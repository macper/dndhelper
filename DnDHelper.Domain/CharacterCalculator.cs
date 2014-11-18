using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.Domain
{
    public class CharacterCalculator : ICharacterCalculator
    {
        private readonly RepositorySet _repository;
        private static readonly ILogger Logger = ServiceContainer.GetInstance<ILogger>( new KeyValuePair<string, object>( Const.LoggerName, typeof( CharacterCalculator ).Name ) );
        private readonly IPythonEngine _python;
        private List<BaseBonus> _alreadyEvaluated;

        public CharacterCalculator( RepositorySet repository )
        {
            _repository = repository;
            _python = ServiceContainer.GetInstance<IPythonEngine>();
        }

        public void CalculateInitialStats( Character character )
        {
            DarkTemplar.AssertNotNull( character.Race, "Race - " + character.Name );
            DarkTemplar.AssertNotNull( character.Class, "Class - " + character.Name );

            Logger.Info( string.Format( "CalculateStats" ) );
            character.Controller.DisableRecalculation = true;
            character.InitialBonuses.Clear();
            character.SkillPointsLeft.SecondarySkills = 0;
            character.OriginalStats.Skills = new List<Skill>();
            character.OriginalStats.Throws = new Throw();
            character.OriginalStats.HP = 0;

            SetCommonParams( character );
            GetClassBonuses( character );
            GetRaceBonuses( character );
            character.Controller.DisableRecalculation = false;
            Logger.Info( "CalculateStats - complete" );
        }

        public void Calculate( Character character )
        {
            try
            {
                character.Controller.DisableRecalculation = true;
                character.Bonuses.Clear();
                character.Attacks.Clear();
                character.Effects.RemoveAll( a => a.Calculated );
                character.CurrentStats = DarkTemplar.DeepClone( character.OriginalStats );
                _alreadyEvaluated = new List<BaseBonus>();
                character.Bonuses.AddRange( character.InitialBonuses );
                Evaluate( character );
                GetItemsBonuses( character );
                Evaluate( character );
                GetAtutesBonuses( character );
                Evaluate( character );
                GetEffectBonuses( character );
                Evaluate( character );
                GetMainSkillsBonus( character );
                Evaluate( character );
                GetAttacks( character );
                GetCustomAttacks( character );
                character.Controller.DisableRecalculation = false;
            }
            catch( Exception exception )
            {
                Logger.Error( exception.Message, exception );
            }
            finally
            {
                character.Controller.DisableRecalculation = false;
            }
        }

        public void ApplySkillBonus( Character character, Skill skill )
        {
            switch( skill.Definition.BonusProperty )
            {
                case BaseAttribute.Charisma:
                    skill.Value += Rules.GetStandardBonus( character.CurrentStats.Charisma );
                    break;

                case BaseAttribute.Constitution:
                    skill.Value += Rules.GetStandardBonus( character.CurrentStats.Constitution );
                    break;

                case BaseAttribute.Dexterity:
                    skill.Value += Rules.GetStandardBonus( character.CurrentStats.Dexterity );
                    break;

                case BaseAttribute.Inteligence:
                    skill.Value += Rules.GetStandardBonus( character.CurrentStats.Inteligence );
                    break;

                case BaseAttribute.Strength:
                    skill.Value += Rules.GetStandardBonus( character.CurrentStats.Strength );
                    break;

                case BaseAttribute.Wisdom:
                    skill.Value += Rules.GetStandardBonus( character.CurrentStats.Wisdom );
                    break;
            }
            if( skill.Definition.PanaltyModifier > 0 )
            {
                skill.Value -= ( skill.Definition.PanaltyModifier * character.CurrentStats.Panalty );
            }
        }

        private void GetMainSkillsBonus( Character character )
        {
            var controller = character.Controller;
            var strengthBonus = Rules.GetStandardBonus( character.CurrentStats.Strength );
            if( strengthBonus != 0 )
            {
                controller.AddBonus( new AttackBonus( CommonSources.Strength, strengthBonus ) { Melee = true } );
                var weapon = character.MainItems.SingleOrDefault( w => w.Position == ItemPosition.RightHand );
                if( weapon != null && !weapon.Item.Definition.IsRangedWeapon )
                {
                    var dmgModifier = strengthBonus;
                    if( weapon.Item.Definition.BaseType == BaseTypes.TwoHandedWeapon )
                    {
                        dmgModifier = (int)( dmgModifier * 1.5 );
                    }
                    controller.AddBonus( new DamageBonus( CommonSources.Strength, new DamageBone() { Count = dmgModifier, MaxValue = 1, DamageType = DamageTypes.Physical } ) );
                }
                if( character.CustomAttacks.Any() )
                {
                    var dmgModifier = strengthBonus;
                    controller.AddBonus( new DamageBonus( CommonSources.Strength, new DamageBone() { Count = dmgModifier, MaxValue = 1, DamageType = DamageTypes.Physical } ) );
                }

                foreach( var skill in character.CurrentStats.Skills.Where( s => s.Definition.BonusProperty == BaseAttribute.Strength ) )
                {
                    controller.AddBonus( new SecondarySkillBonus( CommonSources.Strength, strengthBonus ) { SkillName = skill.Name } );
                }
            }
            var dexterityBonus = Rules.GetStandardBonus( character.CurrentStats.Dexterity );
            if( dexterityBonus != 0 )
            {
                controller.AddBonus( new AttackBonus( CommonSources.Dexterity, dexterityBonus ) { Melee = false } );
                controller.AddBonus( new ACBonus( CommonSources.Dexterity, ACBonusTypes.Dexterity, dexterityBonus ) );
                controller.AddBonus( new ThrowBonus( CommonSources.Dexterity, new Throw() { ReflexThrow = dexterityBonus } ) );
                controller.AddBonus( new InitiativeBonus( CommonSources.Dexterity, dexterityBonus ) );
                foreach( var skill in character.CurrentStats.Skills.Where( s => s.Definition.BonusProperty == BaseAttribute.Dexterity ) )
                {
                    controller.AddBonus( new SecondarySkillBonus( CommonSources.Dexterity, dexterityBonus ) { SkillName = skill.Name } );
                }
            }
            var constitutionBonus = Rules.GetStandardBonus( character.CurrentStats.Constitution );
            if( constitutionBonus != 0 )
            {
                var diff = character.CurrentStats.Constitution - character.OriginalStats.Constitution;
                if( diff != 0 )
                {
                    controller.AddBonus( new HPBonus( CommonSources.Constitution, diff / 2 * character.Level ) );
                }
                controller.AddBonus( new ThrowBonus( CommonSources.Constitution, new Throw() { EnduranceThrow = constitutionBonus } ) );
                foreach( var skill in character.CurrentStats.Skills.Where( s => s.Definition.BonusProperty == BaseAttribute.Constitution ) )
                {
                    controller.AddBonus( new SecondarySkillBonus( CommonSources.Constitution, constitutionBonus ) { SkillName = skill.Name } );
                }
            }
            var inteligenceBonus = Rules.GetStandardBonus( character.CurrentStats.Inteligence );
            if( inteligenceBonus != 0 )
            {
                // TODO : co z punktami umiejêtnoiœæi jako bonus do INT ?
                foreach( var skill in character.CurrentStats.Skills.Where( s => s.Definition.BonusProperty == BaseAttribute.Inteligence ) )
                {
                    controller.AddBonus( new SecondarySkillBonus( CommonSources.Inteligence, inteligenceBonus ) { SkillName = skill.Name } );
                }
            }
            var wisdomBonus = Rules.GetStandardBonus( character.CurrentStats.Wisdom );
            if( wisdomBonus != 0 )
            {
                controller.AddBonus( new ThrowBonus( CommonSources.Wisdom, new Throw() { WillThrow = wisdomBonus } ) );
                foreach( var skill in character.CurrentStats.Skills.Where( s => s.Definition.BonusProperty == BaseAttribute.Wisdom ) )
                {
                    controller.AddBonus( new SecondarySkillBonus( CommonSources.Wisdom, wisdomBonus ) { SkillName = skill.Name } );
                }
            }
            var charismaBonus = Rules.GetStandardBonus( character.CurrentStats.Charisma );
            {
                foreach( var skill in character.CurrentStats.Skills.Where( s => s.Definition.BonusProperty == BaseAttribute.Charisma ) )
                {
                    controller.AddBonus( new SecondarySkillBonus( CommonSources.Charisma, charismaBonus ) { SkillName = skill.Name } );
                }
            }
        }

        private void Evaluate( Character character )
        {
            foreach( var baseBonuse in character.Bonuses.Where( b => !_alreadyEvaluated.Contains( b ) ) )
            {
                baseBonuse.Evaluate( character );
                _alreadyEvaluated.Add( baseBonuse );
            }
        }

        private void GetEffectBonuses( Character character )
        {
            foreach( var effect in character.Effects )
            {
                character.Bonuses.AddRange( effect.Bonuses );
                DarkTemplar.AssertNotNull( effect.Definition, "Effect.Definition - " + effect.Name );

                if( effect.Definition.Script != null )
                {
                    _python.GetMethod<Action<Character, RepositorySet, Effect>>( ScriptContext.Effect, effect.Definition.Script )( character, _repository, effect );
                }
            }
        }

        private void GetAtutesBonuses( Character character )
        {
            foreach( var atute in character.CurrentStats.Atutes )
            {
                DarkTemplar.AssertNotNull( atute.Definition, "Atute.Definition - " + atute.Name );
                
                if( atute.Definition.Script != null )
                {
                    _python.GetMethod<Action<Character, RepositorySet, Atut>>( ScriptContext.Atut, atute.Definition.Script )( character, _repository, atute );
                }
            }
        }

        private void GetItemsBonuses( Character character )
        {
            foreach( var item in character.MainItems )
            {
                if( !Rules.IsBonusPosition( item.Position ) )
                    continue;

                DarkTemplar.AssertNotNull( item.Item, "EquipItem.Item " );
                DarkTemplar.AssertNotNull( item.Item.Definition, "Item.Definition - " + item.Item.Name );

                if( item.Position == ItemPosition.RightHand || ( item.Position == ItemPosition.LeftHand && item.Item.Definition.BaseType == BaseTypes.OneHandWeapon ) )
                {
                    character.Bonuses.AddRange( item.Item.Definition.Bonuses.Where( b => !new[] { typeof( DamageBonus ), typeof( AttackBonus ), typeof( RangeBonus ) }.Contains( b.GetType() ) ) );
                    continue;
                }

                character.Bonuses.AddRange( item.Item.Definition.Bonuses );
                if( item.Item.Definition.Script != null )
                {
                    _python.GetMethod<Action<Character, RepositorySet, Item>>( ScriptContext.Item, item.Item.Definition.Script )( character, _repository, item.Item );
                }
            }
        }

        private static void GetRaceBonuses( Character character )
        {
            Logger.Debug( "GetRaceBonuses - start" );
            var race = character.Race.Definition;
            DarkTemplar.AssertNotNull( race, "Race.Definition - " + character.Race.Name );

            character.OriginalStats.Speed = race.BaseSpeed;
            race.Bonuses.ForEach( b => character.Controller.AddBonusOriginal( b ) );
            Logger.Debug( "GetRaceBonuses - complete" );

        }

        private void GetClassBonuses( Character character )
        {
            Logger.Debug( "GetClassBonuses - start" );
            var baseAttack = 0;
            foreach( var @class in character.Class )
            {
                Logger.Debug( string.Format( "{0}: klasa {1} poziom {2}", character.Name, @class.Name, @class.Level ) );
                character.OriginalStats.HP += @class.Definition.PW + ( @class.Definition.PW / 2 * ( @class.Level - 1 ) ) + ( @class.Level * Rules.GetStandardBonus( character.OriginalStats.Constitution ) );
                baseAttack += Rules.GetBaseAttack( @class.Definition.AttackSkill, @class.Level );
                character.OriginalStats.Throws.Add( Rules.GetThrowTable( @class.Definition.HighEnduranceThrow, @class.Definition.HighReflexThrow, @class.Definition.HighWillThrow )[@class.Level] );
                character.SkillPointsLeft.SecondarySkills += 4 * ( @class.Definition.SkillsPoints );
                character.SkillPointsLeft.SecondarySkills += @class.Level * ( @class.Definition.SkillsPoints );
                var pythonEngine = ServiceContainer.GetInstance<IPythonEngine>();
                if( !string.IsNullOrEmpty( @class.Definition.Script ) )
                {
                    pythonEngine.GetMethod<Action<Character, RepositorySet>>( ScriptContext.Class, @class.Definition.Script )( character, _repository );
                }

                var baseAmount = character.SkillPointsLeft.SecondarySkills;

                var counter = 0;
                var probDictionary = new Dictionary<int, SkillPreference>();
                foreach( var skillPreference in @class.Definition.SkillsPreference )
                {
                    counter += skillPreference.Wage;
                    probDictionary.Add( counter, skillPreference );
                    if( counter > 100 )
                    {
                        Logger.Error( "SkillPreference counter < 100 !!", null );
                    }
                }

                for ( var l = 0; l < @class.Level; l++ )
                {
                    var skillPoints = @class.Definition.SkillsPoints + ( Rules.GetStandardBonus( character.CurrentStats.Inteligence ) );
                    if( skillPoints < 1 )
                    {
                        skillPoints = 1;
                    }

                    var rnd = new Random();

                    for( var k = 0; k < skillPoints; k++ )
                    {
                        var r = rnd.Next( 0, 100 );
                        foreach (var key in probDictionary.Keys)
                        {
                            if ( r <= key)
                            {
                                character.Controller.ChangeSecondarySkillOriginal(probDictionary[key].Name, 1);
                                break;
                            }
                        }
                    }
                    if( character.SkillPointsLeft.SecondarySkills <= 0 )
                        break;
                }

                if( @class.Definition.SpellsPerLevel != null && @class.Definition.SpellsPerLevel.Any() )
                {
                    character.AvailableCastings = new List<SpellCasting>();
                    foreach( var casting in @class.Definition.SpellsPerLevel[@class.Level] )
                    {
                        character.AvailableCastings.Add( casting );
                    }
                }
            }
            character.OriginalStats.Attack.Melee = baseAttack;
            character.OriginalStats.Attack.Range = baseAttack;
            character.OriginalStats.Attack.NumberOfAttacks = 1 + ( ( baseAttack - 1 ) / 5 );
            Logger.Debug( "GetClassBonuses - complete" );
        }

        private void SetCommonParams( Character character )
        {
            Logger.Debug( "GetCommonBonuses - start" );
            character.SkillPointsLeft.MainSkills = 10 + (int)Math.Floor( (double)character.Level / 4 );
            character.SkillPointsLeft.Atutes = 1 + (int)Math.Floor( (double)character.Level / 3 );
            Logger.Debug( "GetCommonBonuses - complete" );
        }

        private void GetAttacks( Character character )
        {
            if( !character.CurrentStats.Attack.Defined )
            {
                return;
            }

            var rightHandWeapon = character.MainItems.FirstOrDefault( i => i.Position == ItemPosition.RightHand );
            if( rightHandWeapon == null )
                return;
            var rightHandDmg = rightHandWeapon.Item.Definition.Bonuses.Where( b => b.GetType() == typeof( DamageBonus ) ).Cast<DamageBonus>();
            if( rightHandDmg.Count() == 0 )
                throw new ApplicationException( "Brak DamageBonus dla g³ównej broni!" );
            var rightHandAttack = rightHandWeapon.Item.Definition.Bonuses.SingleOrDefault( b => b.GetType() == typeof( AttackBonus ) ) as AttackBonus;
            var rightHandRange = rightHandWeapon.Item.Definition.Bonuses.SingleOrDefault( b => b.GetType() == typeof( RangeBonus ) ) as RangeBonus;

            if( rightHandRange != null )
            {
                GetAttacksForRangedWeapon( character, rightHandWeapon, rightHandDmg, rightHandAttack, rightHandRange );
                return;
            }

            var leftHand = character.MainItems.SingleOrDefault( i => i.Position == ItemPosition.LeftHand );
            if( rightHandWeapon.Item.Definition.BaseType == BaseTypes.OneHandWeapon && leftHand != null && leftHand.Item.Definition.BaseType == BaseTypes.OneHandWeapon )
            {
                GetAttacksForTwoWeapons( character, rightHandWeapon, rightHandDmg, rightHandAttack, leftHand );
            }
            var attack = new Attack
            {
                Name = rightHandWeapon.Item.Name,
                Damage = character.CurrentStats.DamageBonus,
                NumberOfAttacks = character.CurrentStats.Attack.NumberOfAttacks,
                ToHit = character.CurrentStats.Attack.Melee
            };
            attack.Damage.AddElements( rightHandDmg.Select( s => s.Amount ) );
            if( rightHandAttack != null )
            {
                attack.ToHit += rightHandAttack.Value;
            }
            character.Attacks.Add( attack );
            ApplyBonusFromAtutes( character, attack, rightHandWeapon.Item );
        }

        private void ApplyBonusFromAtutes( Character character, Attack attack, Item weapon )
        {
            if( character.CurrentStats.Atutes.Any( a => a.Name == AtutDefinition.CommonAtutes.FineCombat ) )
            {
                if( new[] { ItemPrototypes.ShortSword, ItemPrototypes.Dagger, ItemPrototypes.Scimitar, ItemPrototypes.Rapier }.Contains( weapon.Definition.PrototypeName ) )
                {
                    var dexterityBonus = Rules.GetStandardBonus( character.CurrentStats.Dexterity );
                    var strengthBonus = Rules.GetStandardBonus( character.CurrentStats.Strength );
                    if( dexterityBonus > strengthBonus )
                    {
                        attack.ToHit += ( dexterityBonus - strengthBonus );
                    }
                }
            }
            var weaponBonuses = character.Bonuses.Where( b => b.GetType() == typeof( WeaponBonus ) ).Cast<WeaponBonus>();
            var thisWeapon = weaponBonuses.Where( w => w.Item == weapon );
            if( !thisWeapon.Any() )
                return;

            foreach( var bonus in thisWeapon.Select( t => t.Bonus ) )
            {
                var dmgBonus = bonus as DamageBonus;
                if( dmgBonus != null )
                {
                    attack.Damage.AddElement( dmgBonus.Amount );
                    continue;
                }
                var attackBonus = bonus as AttackBonus;
                if( attackBonus != null )
                {
                    attack.ToHit += attackBonus.Value;
                    continue;
                }
                var numberOfAttacks = bonus as NumberOfAttacksBonus;
                if( numberOfAttacks != null )
                {
                    attack.NumberOfAttacks += numberOfAttacks.Value;
                    continue;
                }
            }
        }

        private void GetCustomAttacks( Character character )
        {
            foreach( var customAttack in character.CustomAttacks )
            {
                var ranged = customAttack.Bonuses.Any( b => b.GetType() == typeof( RangeBonus ) );
                var attack = new Attack
                                 {
                                     Name = customAttack.Name,
                                     NumberOfAttacks = character.CurrentStats.Attack.NumberOfAttacks,
                                     ToHit = character.CurrentStats.Attack.Melee,
                                     Damage = new Damage(),
                                     Custom = true
                                 };
                if( ranged )
                {
                    attack.ToHit = character.CurrentStats.Attack.Range;
                    attack.Range = customAttack.Bonuses.Single( b => b.GetType() == typeof( RangeBonus ) ).Value;
                }
                else
                {
                    attack.Damage.AddElement( character.CurrentStats.DamageBonus );
                }

                foreach( var baseBonus in customAttack.Bonuses )
                {
                    var attackBonus = baseBonus as AttackBonus;
                    if( attackBonus != null )
                        attack.ToHit += attackBonus.Value;
                    var damageBonus = baseBonus as DamageBonus;
                    if( damageBonus != null )
                        attack.Damage.AddElement( damageBonus.Amount );
                    var numberOfAttacks = baseBonus as NumberOfAttacksBonus;
                    if( numberOfAttacks != null )
                        attack.NumberOfAttacks += numberOfAttacks.Value;
                }
                character.Attacks.Add( attack );
            }
        }

        private void GetAttacksForTwoWeapons( Character character, EquipItem rightHandWeapon, IEnumerable<DamageBonus> rightHandDmg, AttackBonus rightHandAttack, EquipItem leftHand )
        {
            bool hasAtutTwoWeaponsCombat = character.CurrentStats.Atutes.Any( a => a.Name == AtutDefinition.CommonAtutes.TwoWeaponsCombat );
            var additionalWeaponAttacks = 1;
            var firstWeaponPanalty = -4;
            var secondWeaponPanalty = -8;
            if( hasAtutTwoWeaponsCombat )
            {
                firstWeaponPanalty = -2;
                secondWeaponPanalty = -2;
            }
            if( character.CurrentStats.Atutes.Any( a => a.Name == AtutDefinition.CommonAtutes.AdvancedTwoWeaponsCombat ) )
            {
                additionalWeaponAttacks = 2;
            }
            if( character.CurrentStats.Atutes.Any( a => a.Name == AtutDefinition.CommonAtutes.MasterTwoWeaponsCombat ) )
            {
                additionalWeaponAttacks = 3;
            }
            var rightAttack = new Attack
                                  {
                                      Name = string.Format( "1:Prawa rêka:{0}", rightHandWeapon.Item.Name ),
                                      Damage = new Damage( character.CurrentStats.DamageBonus.Elements ),
                                      NumberOfAttacks = character.CurrentStats.Attack.NumberOfAttacks,
                                      ToHit = character.CurrentStats.Attack.Melee + firstWeaponPanalty
                                  };
            rightAttack.Damage.AddElements( rightHandDmg.Select( s => s.Amount ) );
            if( rightHandAttack != null )
            {
                rightAttack.ToHit += rightHandAttack.Value;
            }
            character.Attacks.Add( rightAttack );

            var leftHandDmg = leftHand.Item.Definition.Bonuses.Where( b => b.GetType() == typeof( DamageBonus ) ).Cast<DamageBonus>();
            if( leftHandDmg.Count() == 0 )
            {
                throw new ApplicationException( "Brak DamageBonus dla drugiej broni!" );
            }
            var leftHandAttack = leftHand.Item.Definition.Bonuses.SingleOrDefault( b => b.GetType() == typeof( AttackBonus ) );
            var leftAttack = new Attack
                                 {
                                     Name = string.Format( "2:Lewa rêka:{0}", leftHand.Item.Name ),
                                     Damage = new Damage( character.CurrentStats.DamageBonus.Elements ),
                                     NumberOfAttacks = additionalWeaponAttacks,
                                     ToHit = character.CurrentStats.Attack.Melee + secondWeaponPanalty
                                 };
            leftAttack.Damage.AddElements( leftHandDmg.Select( s => s.Amount ) );
            if( leftHandAttack != null )
            {
                leftAttack.ToHit += leftHandAttack.Value;
            }
            character.Attacks.Add( leftAttack );
            ApplyBonusFromAtutes( character, rightAttack, rightHandWeapon.Item );
            ApplyBonusFromAtutes( character, leftAttack, leftHand.Item );
        }

        private void GetAttacksForRangedWeapon( Character character, EquipItem rightHandWeapon, IEnumerable<DamageBonus> rightHandDmg, AttackBonus rightHandAttack, RangeBonus rightHandRange )
        {
            var baseDmg = new Damage( character.CurrentStats.DamageBonus.Elements );
            baseDmg.AddElements( rightHandDmg.Select( s => s.Amount ) );
            var baseAttack = character.CurrentStats.Attack.Range;
            if( rightHandAttack != null )
            {
                baseAttack += rightHandAttack.Value;
            }
            var arrows = character.MainItems.Where( i => i.Position == ItemPosition.Arrow );
            if( arrows.Count() == 0 )
            {
                character.Attacks.Add( new Attack
                {
                    Name = rightHandWeapon.Item.Name,
                    Damage = baseDmg,
                    NumberOfAttacks = character.CurrentStats.Attack.NumberOfAttacks,
                    Range = rightHandRange.Value,
                    ToHit = baseAttack
                } );
                return;
            }
            foreach( var arrow in arrows )
            {
                var actualDmg = new Damage( baseDmg.Elements );
                var arrowDmgBonus = arrow.Item.Definition.Bonuses.SingleOrDefault( b => b.GetType() == typeof( DamageBonus ) ) as DamageBonus;
                if( arrowDmgBonus != null )
                {
                    actualDmg.AddElement( arrowDmgBonus.Amount );
                }
                var actualToHit = baseAttack;
                var arrowToHitBonus = arrow.Item.Definition.Bonuses.SingleOrDefault( b => b.GetType() == typeof( AttackBonus ) ) as AttackBonus;
                if( arrowToHitBonus != null )
                {
                    actualToHit += arrowToHitBonus.Value;
                }
                character.Attacks.Add( new Attack
                {
                    Name = string.Format( "{0} - {1}({2})", rightHandWeapon.Item.Name, arrow.Item.Name, arrow.Item.Charges ),
                    Damage = actualDmg,
                    NumberOfAttacks = character.CurrentStats.Attack.NumberOfAttacks,
                    Range = rightHandRange.Value,
                    ToHit = actualToHit,
                    MissileName = arrow.Item.Name
                } );
            }
        }
    }
}