using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DnDHelper.Domain
{
    public class AppFacade : IAppStateController
    {
        public RepositorySet Repositories { get; private set; }
        public IGameTimer GameTimer { get; private set; }
        public string Notes
        {
            get { return Repositories.Get<AppSetting>().GetElementByName( Const.AppSettings.Notes ).Value; }
            set
            {
                var el = Repositories.Get<AppSetting>().GetElementByName( Const.AppSettings.Notes );
                el.Value = value;
                Repositories.Get<AppSetting>().Commit( el, true );
            }
        }
        private static readonly ILogger Logger = ServiceContainer.GetInstance<ILogger>( new KeyValuePair<string, object>( Const.LoggerName, typeof( AppFacade ).Name ) );
        public IEnumerable<Character> AllCharacters
        {
            get { return Repositories.Get<Character>().Elements.OrderBy( c => c.Name ); }
        }

        public IEnumerable<CharacterGroup> RepoGroups { get { return Repositories.Get<CharacterGroup>().Elements.OrderBy( c => c.Name ); } }
        public IEnumerable<ItemDefinition> RepoItems { get { return Repositories.Get<ItemDefinition>().Elements.OrderBy( i => i.Name ); } }
        public IEnumerable<SpellDefinition> RepoSpells { get { return Repositories.Get<SpellDefinition>().Elements.OrderBy( s => s.Level ); } }
        public IEnumerable<EffectDefinition> RepoEffects { get { return Repositories.Get<EffectDefinition>().Elements.OrderBy( e => e.Name ); } }
        public IEnumerable<AtutDefinition> RepoAtutes { get { return Repositories.Get<AtutDefinition>().Elements.OrderBy( s => s.Name ); } }
        public IEnumerable<SkillDefinition> RepoSkills { get { return Repositories.Get<SkillDefinition>().Elements.OrderBy( s => s.Name ); } }
        public IEnumerable<ClassDefinition> RepoClasses { get { return Repositories.Get<ClassDefinition>().Elements.OrderBy( s => s.Name ); } }
        public IEnumerable<RaceDefinition> RepoRaces { get { return Repositories.Get<RaceDefinition>().Elements.OrderBy( s => s.Name ); } }
        public IEnumerable<Script> RepoScripts { get { return Repositories.Get<Script>().Elements.OrderBy( s => s.Name ); } }

        public event EventHandler<CharacterEventArgs> CharacterHasChanged;

        public bool IsOffline { get; set; }

        private IEnumerable<string> _briefGroups;
        private IList<Character> _briefCharacters;

        public AppFacade( RepositorySet repositorySet )
        {
            IsRunning = true;
            Repositories = repositorySet;
            RegisterTypes();
            SubscribeEvents();
        }

        public void InitBriefService()
        {
            var configVal = ConfigurationManager.AppSettings["BriefGroups"];
            if( configVal == null )
            {
                Logger.Error( "Brak wpisu konfiguracyjnego - BriefGroups", new ConfigurationException( "Brak wpisu konfiguracyjnego - BriefGroups" ) );
                return;
            }
            _briefGroups = configVal.Split( ',' );
            _briefCharacters = new List<Character>();
            //RepoGroups.Where( r => _briefGroups.Contains( r.Name ) ).ToList().ForEach( g => g.Members.ForEach( m => _briefCharacters.Add( m ) ) );
            CharacterHasChanged += ( s, e ) =>
            {
                if( _briefCharacters.Any( c => c.Id == e.Character.Id ) )
                {
                    ServiceContainer.GetInstance<BriefServiceQueueManager>().AddBriefCharacter( e.Character, TypeOfChange.Update );
                }
                var battleCh = Battle.Instance.GetBattleCharacter( e.Character );
                if( battleCh != null && battleCh.SendNotify )
                {
                    ServiceContainer.GetInstance<BriefServiceQueueManager>().AddEnemyBriefCharacter( battleCh, TypeOfChange.Update );
                }
            };
            var queueManager = ServiceContainer.GetInstance<BriefServiceQueueManager>();
            foreach( var group in _briefGroups )
            {
                var gr = RepoGroups.SingleOrDefault( s => s.Name == group );
                if( gr != null )
                {
                    // gr.Members.ForEach( c => queueManager.AddBriefCharacter( c, TypeOfChange.Insert ) );
                }
            }
            queueManager.Run();
        }

        public OperationResult AddGroup( CharacterGroup group )
        {
            if( RepoGroups.Any( g => g.Name == group.Name ) )
            {
                return OperationResult.Error( "Istnieje ju¿ grupa o takiej nazwie" );
            }
            Repositories.Get<CharacterGroup>().Elements.Add( group );
            Repositories.Get<CharacterGroup>().Commit( group, true );
            Logger.Info( "Dodano now¹ grupê o nazwie: " + group.Name );
            return OperationResult.Success();
        }

        public OperationResult RemoveGroup( CharacterGroup group )
        {
            if( !RepoGroups.Contains( group ) )
            {
                return OperationResult.Error( "Brak takiej grupy" );
            }
            Repositories.Get<CharacterGroup>().Elements.Remove( group );
            Repositories.Get<CharacterGroup>().Commit( group, false );
            Logger.Info( "Usuniêto grupê o nazwie: " + group.Name );
            return OperationResult.Success();
        }

        public OperationResult AddCharacter( Character character, CharacterGroup characterGroup )
        {
            if( characterGroup.Characters.Any( m => m.Name == character.Name ) )
            {
                return OperationResult.Error( "W tej grupie istnieje ju¿ cz³onek o takiej nazwie" );
            }
            character.GroupName = characterGroup.Name;
            var characterRepo = Repositories.Get<Character>();
            characterRepo.Elements.Add( character );
            characterRepo.Commit( character, true );
            Logger.Info( string.Format( "Dodano postaæ: {0} do grupy {1}", character.Name, characterGroup.Name ) );
            return OperationResult.Success();
        }

        public OperationResult RemoveCharacter( Character character )
        {
            var characterRepo = Repositories.Get<Character>();
            characterRepo.Elements.Remove( character );
            characterRepo.Commit( character, false );
            Logger.Info( "Usuniêto postaæ " + character.Name );
            return OperationResult.Success();
        }

        public OperationResult CopyCharacter( Character character )
        {
            var characterRepo = Repositories.Get<Character>();
            var copy = DarkTemplar.DeepClone( character, true );
            characterRepo.Elements.Add( copy );
            copy.Name += "(kopia)";
            copy.Id = Guid.NewGuid();
            Logger.Info( "Wykonano kopiê postaci: " + character );
            characterRepo.Commit( character, true );
            return OperationResult.Success();
        }

        public OperationResult MoveCharacter( Character character, CharacterGroup newGroup )
        {
            var oldGroup = character.GroupName;
            var characterRepo = Repositories.Get<Character>();
            character.GroupName = newGroup.Name;
            Logger.Info( string.Format( "Przeniesiono postaæ {0} z grupy {1} do grupy {2}", character.Name, oldGroup, newGroup.Name ) );
            Repositories.Get<Character>().Commit( character, true );
            return OperationResult.Success();
        }

        public void CharacterChange( Character character )
        {
            Repositories.Get<Character>().Commit( character, true );
            if( CharacterHasChanged != null )
                CharacterHasChanged( this, new CharacterEventArgs { Character = character } );
        }

        public OperationResult AddSpell( SpellDefinition spell )
        {
            if( RepoSpells.Any( s => s.Name == spell.Name ) )
            {
                return OperationResult.Error( "Istnieje ju¿ czar o takiej nazwie" );
            }
            Repositories.Get<SpellDefinition>().Elements.Add( spell );
            Repositories.Get<SpellDefinition>().Commit( spell, true );
            Logger.Info( "Dodano definicjê czaru: " + spell.Name );
            return OperationResult.Success();
        }

        public OperationResult RemoveSpell( SpellDefinition spell )
        {
            Repositories.Get<SpellDefinition>().Elements.Remove( spell );
            Repositories.Get<SpellDefinition>().Commit( spell, false );
            Logger.Info( "Usuniêto definicjê czaru: " + spell.Name );
            return OperationResult.Success();
        }

        public IEnumerable<ItemDefinition> GetItemPrototypes()
        {
            return Repositories.Get<ItemDefinition>().Elements.Where( i => i.IsPrototype );
        }

        public IEnumerable<ItemDefinition> GetItemsByType( BaseTypes type )
        {
            return RepoItems.Where( i => i.BaseType == type ).OrderBy( n => n.Name );
        }

        public static IEnumerable<string> GetDamageTypes()
        {
            return new[] { DamageTypes.Physical, DamageTypes.Acid, DamageTypes.DivineEnergy, DamageTypes.Fire, DamageTypes.Ice, DamageTypes.NegativeEnergy };
        }

        public static IEnumerable<string> GetACTypes()
        {
            return new[] { ACBonusTypes.Armor, ACBonusTypes.Dexterity, ACBonusTypes.MagicShield, ACBonusTypes.NaturalArmor, ACBonusTypes.Other };
        }

        public OperationResult AddEffect( EffectDefinition effect )
        {
            if( RepoEffects.Any( r => r.Name == effect.Name ) )
            {
                return OperationResult.Error( "Istnieje ju¿ efekt o takiej nazwie" );
            }
            Repositories.Get<EffectDefinition>().Elements.Add( effect );
            Repositories.Get<EffectDefinition>().Commit( effect, true );
            Logger.Info( "Dodano efekt: " + effect.Name );
            return OperationResult.Success();
        }

        public OperationResult AddRace( RaceDefinition race )
        {
            if( RepoRaces.Any( r => r.Name == race.Name ) )
            {
                return OperationResult.Error( "Istnieje ju¿ rasa o takiej nazwie" );
            }
            Repositories.Get<RaceDefinition>().Elements.Add( race );
            Repositories.Get<RaceDefinition>().Commit( race, true );
            Logger.Info( "Dodano rase: " + race.Name );
            return OperationResult.Success();
        }

        public OperationResult RemoveEffect( EffectDefinition effect )
        {
            Repositories.Get<EffectDefinition>().Elements.Remove( effect );
            Repositories.Get<EffectDefinition>().Commit( effect, false );
            Logger.Info( "Usuniêto efekt: " + effect.Name );
            return OperationResult.Success();
        }

        public OperationResult RemoveRace( RaceDefinition race )
        {
            Repositories.Get<RaceDefinition>().Elements.Remove( race );
            Repositories.Get<RaceDefinition>().Commit( race, false );
            Logger.Info( "Usuniêto rase: " + race.Name );
            return OperationResult.Success();
        }

        public OperationResult AddItem( ItemDefinition item )
        {
            var repo = Repositories.Get<ItemDefinition>();
            if( repo.Elements.Any( e => e.Name == item.Name ) )
                return OperationResult.Error( "Istnieje ju¿ przedmiot o takiej nazwie" );

            repo.Elements.Add( item );
            repo.Commit( item, true );
            Logger.Info( "Dodano przedmiot: " + item.Name );
            return OperationResult.Success();
        }

        public OperationResult RemoveItem( ItemDefinition item )
        {
            Repositories.Get<ItemDefinition>().Elements.Remove( item );
            Repositories.Get<ItemDefinition>().Commit( item, false );
            Logger.Info( "Usuniêto przedmiot: " + item.Name );
            return OperationResult.Success();
        }

        public OperationResult RemoveSkill( SkillDefinition skill )
        {
            Repositories.Get<SkillDefinition>().Elements.Remove( skill );
            Repositories.Get<SkillDefinition>().Commit( skill, false );
            Logger.Info( "Usuniêto skilla + " + skill.Name );
            return OperationResult.Success();
        }

        public OperationResult AtutesChanged( AtutDefinition atutDefinition )
        {
            Repositories.Get<AtutDefinition>().Commit( atutDefinition, true );
            return OperationResult.Success();
        }

        public OperationResult AddAtut( AtutDefinition atutDefinition )
        {
            var repo = Repositories.Get<AtutDefinition>();
            if( repo.Elements.Any( r => r.Name == atutDefinition.Name ) )
            {
                return OperationResult.Error( "Istnieje ju¿ atut o takiej nazwie" );
            }
            repo.Elements.Add( atutDefinition );
            Repositories.Get<AtutDefinition>().Commit( atutDefinition, true );
            return OperationResult.Success();
        }

        public OperationResult RemoveAtut( AtutDefinition atutDefinition )
        {
            Repositories.Get<AtutDefinition>().Elements.Remove( atutDefinition );
            Repositories.Get<AtutDefinition>().Commit( atutDefinition, false );
            return OperationResult.Success();
        }

        public ItemDefinition GetItemCopy( ItemDefinition item )
        {
            var copy = DarkTemplar.DeepClone( item, true );
            copy.Name += ( "(kopia)" );
            copy.Id = Guid.NewGuid();
            return copy;
        }

        public OperationResult ItemsChange( ItemDefinition item )
        {
            Repositories.Get<ItemDefinition>().Commit( item, true );
            return OperationResult.Success();
        }

        public OperationResult SpellChange( SpellDefinition spell )
        {
            Repositories.Get<SpellDefinition>().Commit( spell, true );
            return OperationResult.Success();
        }

        public OperationResult SkillsChanged( SkillDefinition skill )
        {
            Repositories.Get<SkillDefinition>().Commit( skill, true );
            return OperationResult.Success();
        }

        public OperationResult AddSkill( SkillDefinition skill )
        {
            var repo = Repositories.Get<SkillDefinition>();
            if( repo.Elements.Any( r => r.Name == skill.Name ) )
                return OperationResult.Error( "Isnieje ju¿ umiejêtnoœæ o takiej nazwie" );

            repo.Elements.Add( skill );
            return SkillsChanged( skill );
        }

        public OperationResult CalculateInitial( Character character )
        {
            try
            {
                var calculator = ServiceContainer.GetInstance<CharacterCalculator>();
                calculator.CalculateInitialStats( character );
                calculator.Calculate( character );
                character.Life = character.CurrentStats.HP;
            }
            catch( Exception exception )
            {
                return OperationResult.Error( exception.Message );
            }
            return OperationResult.Success();
        }

        public OperationResult Calculate( Character character )
        {
            try
            {
                var calculator = ServiceContainer.GetInstance<CharacterCalculator>();
                calculator.Calculate( character );
            }
            catch( Exception exception )
            {
                return OperationResult.Error( exception.Message );
            }
            return OperationResult.Success();
        }

        public OperationResult AddClass(ClassDefinition classDefinition)
        {
            var repo = Repositories.Get<ClassDefinition>();
            if( repo.Elements.Any( e => e.Name == classDefinition.Name ) )
                return OperationResult.Error( "Istnieje ju¿ klasa o takiej nazwie" );

            repo.Elements.Add( classDefinition );
            repo.Commit( classDefinition, true );

            return OperationResult.Success();
        }

        public void ClassChanged(ClassDefinition classDefinition)
        {
            Repositories.Get<ClassDefinition>().Commit( classDefinition, true );
        }

        public void RemoveClass(ClassDefinition classDefinition)
        {
            var repo = Repositories.Get<ClassDefinition>();
            repo.Elements.Remove( classDefinition );

            repo.Commit( classDefinition, false );
        }

        public void EffectsChanged( EffectDefinition effect )
        {
            Repositories.Get<EffectDefinition>().Commit( effect, true );
        }

        private void RegisterTypes()
        {
            Logger.Info( "Rejestracja typów" );
            try
            {
                RegisterType<AppFacade>( () => ServiceContainer.Kernel.Bind<AppFacade>().ToConstant( this ) );
                RegisterType<IAppStateController>( () => ServiceContainer.Kernel.Bind<IAppStateController>().ToConstant( this ) );
                RegisterType<RepositorySet>( () => ServiceContainer.Kernel.Bind<RepositorySet>().ToConstant( Repositories ) );
                RegisterType<IQueueManager>( () => ServiceContainer.Kernel.Bind<IQueueManager>().ToConstant( new AdvancedQueueManager() ) );
                RegisterType<ICharacterCalculator>( () => ServiceContainer.Kernel.Bind<ICharacterCalculator>().ToConstant( new CharacterCalculator( Repositories ) ) );
                GameTimer = new GameTimer( DateTime.Parse( Repositories.Get<AppSetting>().GetElementByName( Const.AppSettings.Time ).Value ) );
                RegisterType<IGameTimer>( () => ServiceContainer.Kernel.Bind<IGameTimer>().ToConstant( GameTimer ) );
                RegisterType<IUpdateClient>( () => ServiceContainer.Kernel.Bind<IUpdateClient>().ToConstant( new UpdateClient() ) );

            }
            catch( Exception exception )
            {
                Logger.Fatal( "B³¹d podczas rejestracji typu! " + exception.Message, exception );
                throw;
            }
            Logger.Info( "Typy zarejestrowano" );
        }

        public void SetUpTime()
        {
            GameTimer.CurrentTime = DateTime.Parse( Repositories.Get<AppSetting>().GetElementByName( Const.AppSettings.Time ).Value );
        }

        public static void ExecuteAndLog( Action action, string logEntry )
        {
            action();
            Logger.Info( logEntry );
        }

        private static void RegisterType<T>( Action action )
        {
            ExecuteAndLog( action, string.Format( "Zarejestrowano: " + typeof( T ) ) );
        }

        private void SubscribeEvents()
        {
            var timer = ServiceContainer.GetInstance<IGameTimer>();
            timer.SubscribeOnTurnChange( "AppFacade.SubscribeEvents", ( amount ) =>
                                                  {
                                                      foreach( var character in AllCharacters.Where( c => c.Effects.Count > 0 && c.Effects.Any( e => e.Duration != null ) ) )
                                                      {
                                                          character.Effects.ForEach( e => e.Duration -= amount );
                                                          var toDelete = character.Effects.Where( e => e.Duration <= 0 ).ToArray();
                                                          for( var i = 0; i < toDelete.Length; i++ )
                                                          {
                                                              character.Controller.RemoveEffect( toDelete[i] );
                                                          }
                                                          CharacterChange( character );
                                                      }

                                                  } );
        }

        public bool IsRunning { get; set; }

        public OperationResult RacesChanged( RaceDefinition race )
        {
            Repositories.Get<RaceDefinition>().Commit( race, true );
            return OperationResult.Success();
        }

        public OperationResult AddExperience( Experience experience )
        {
            var repo = Repositories.Get<Experience>();
            repo.Elements.Add( experience );
            repo.Commit( experience, true );
            return OperationResult.Success();
        }

        public OperationResult RemoveExperience( Experience experience )
        {
            var repo = Repositories.Get<Experience>();
            repo.Elements.Remove( experience );
            repo.Commit( experience, false );
            return OperationResult.Success();
        }

        public OperationResult RemoveAllExperiences()
        {
            var repo = Repositories.Get<Experience>();
            repo.Elements.Clear();
            repo.Commit(null, false);
            return OperationResult.Success();
        }
    }
}