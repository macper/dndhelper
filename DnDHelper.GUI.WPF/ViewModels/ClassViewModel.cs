using DnDHelper.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class ClassViewModel : ViewModelBase
    {
        private ClassDefinition _class;
        private Lazy<AppFacade> _appFacade = new Lazy<AppFacade>( () => ServiceContainer.GetInstance<AppFacade>() );
        private Lazy<IAppAPI> _appApi = new Lazy<IAppAPI>( () => ServiceContainer.GetInstance<IAppAPI>() );

        public string Name
        {
            get
            {
                return _class.Name;
            }
            set
            {
                _class.Name = value;
                PropertyHasChanged( "Name" );
            }
        }

        public int PW
        {
            get
            {
                return _class.PW;
            }
            set
            {
                _class.PW = value;
                PropertyHasChanged( "PW" );
            }
        }

        public SkillRate AttackSkill
        {
            get
            {
                return _class.AttackSkill;
            }
            set
            {
                _class.AttackSkill = value;
                PropertyHasChanged( "AttackSkill" );
            }
        }

        public bool HighReflexThrow
        {
            get
            {
                return _class.HighReflexThrow;
            }
            set
            {
                _class.HighReflexThrow = value;
                PropertyHasChanged( "HighReflexThrow" );
            }
        }

        public bool HighWillThrow
        {
            get
            {
                return _class.HighWillThrow;
            }
            set
            {
                _class.HighWillThrow = value;
                PropertyHasChanged( "HighWillThrow" );
            }
        }

        public bool HighEnduranceThrow
        {
            get
            {
                return _class.HighEnduranceThrow;
            }
            set
            {
                _class.HighEnduranceThrow = value;
                PropertyHasChanged( "HighEnduranceThrow" );
            }
        }

        public int SkillPoints
        {
            get
            {
                return _class.SkillsPoints;
            }
            set
            {
                _class.SkillsPoints = value;
                PropertyHasChanged( "SkillPoints" );
            }
        }

        public string Script
        {
            get
            {
                return _class.Script;
            }
            set
            {
                _class.Script = value;
                PropertyHasChanged( "Script" );
            }
        }

        public int SkillPreferenceCountAlreadySet
        {
            get
            {
                var sum = SkillsPreference.Sum(s => s.Wage);
                return sum;
            }
        }

        public ObservableCollection<Script> Scripts
        {
            get
            {
                return new ObservableCollection<Script>( ServiceContainer.GetInstance<RepositorySet>().Get<Script>().Elements.Where( e => e.ScriptContext == ScriptContext.Class ) );
            }
        }

        public ObservableCollection<SkillPreference> SkillsPreference { get; private set; }

        private int _selectedSpellCastLevel;
        public int SelectedSpellCastLevel
        {
            get
            {
                return _selectedSpellCastLevel;
            }
            set
            {
                _selectedSpellCastLevel = value;
                PropertyHasChanged( "SelectedSpellCastLevel" );
            }
        }

        public ObservableCollection<SpellCasting> SpellCastings { get; set; }

        public ObservableCollection<int> SpellCastingLevels { get; private set; }

        private int _selectedSpellCastingLevel;
        public int SelectedSpellCastingLevel
        {
            get
            {
                return _selectedSpellCastingLevel;
            }
            set
            {
                _selectedSpellCastingLevel = value;
                PropertyHasChanged( "SelectedSpellCastingLevel" );
                if( _class.SpellsPerLevel.Count < _selectedSpellCastingLevel )
                {
                    _class.SpellsPerLevel.Add( new List<SpellCasting>() );
                }
                SpellCastings = new ObservableCollection<SpellCasting>(_class.SpellsPerLevel[_selectedSpellCastingLevel - 1]);
                PropertyHasChanged( "SpellCastings" );
                
            }
        }

        public IEnumerable<EnumDictionaryEntry<SkillRate>> AttackSkillRates { get; private set; }
        public IEnumerable<int> SpellLevels { get; private set; }



        public ICommand Commit { get; private set; }

        public ICommand EditScript { get; private set; }

        public ICommand AddSpellCastingLevel { get; private set; }

        public ICommand AddSpellCasting { get; private set; }

        public ICommand RemoveSpellCasting { get; private set; }

        public ICommand CopyPreviousSpellCasting { get; private set; }

        public ICommand RefreshSkillPreference { get; private set; }
        public ClassViewModel(ClassDefinition @class)
        {
            _class = @class;
            AttackSkillRates = EnumsDictionary.SkillRates;
            SpellLevels = Enumerable.Range( 1, 9 );


            SpellCastingLevels = new ObservableCollection<int>();
            for( var l = 0; l < _class.SpellsPerLevel.Count; l++ )
            {
                SpellCastingLevels.Add( l + 1 );
            }

            SkillsPreference = new ObservableCollection<SkillPreference>();

            foreach( var skill in _appFacade.Value.RepoSkills )
            {
                var exising = _class.SkillsPreference.SingleOrDefault( s => s.Name == skill.Name );
                if( exising != null )
                {
                    SkillsPreference.Add( exising );
                    continue;
                }
                SkillsPreference.Add( new SkillPreference
                {
                    Name = skill.Name,
                    Wage = 0
                } );
            }


            Commit = new Command( ( o ) =>
            {
                _class.SkillsPreference.Clear();
                foreach( var pref in SkillsPreference )
                {
                    if( pref.Wage > 0 )
                    {
                        _class.SkillsPreference.Add( pref );
                    }
                }
                if( _class.Id.HasValue )
                {
                    _appFacade.Value.ClassChanged( _class );
                    _appApi.Value.ShowNotifyToUser( "Zmiany zapisano pomyślnie" );
                    CommandHasExecuted( "Commit", OperationResult.Success() );
                    return;
                }
                _appApi.Value.HandleOperationResult( _appFacade.Value.AddClass( _class ), () =>
                {
                    _appApi.Value.ShowNotifyToUser( "Pomyślnie dodano klasę" );
                    CommandHasExecuted( "Commit", OperationResult.Success() );
                } );
                
            } );

            EditScript = new Command( ( o ) =>
            {
                _appApi.Value.RedirectToViewModel( new ScriptEditorViewModel( ScriptContext.Class ), () =>
                {
                    PropertyHasChanged( "Scripts" );
                } );
            } );

            AddSpellCastingLevel = new Command( ( o ) =>
            {
                var last = 0;
                if( SpellCastingLevels != null && SpellCastingLevels.Any() )
                {
                    last = SpellCastingLevels.Max();
                }
                
                SpellCastingLevels.Add( last + 1 );
                PropertyHasChanged( "SpellCastingLevels" );
            } );

            AddSpellCasting = new Command( ( o ) =>
            {
                var sc = new SpellCasting
                {
                    Level = 1,
                    Type = SpellType.Mage,
                    Count = 1
                };
                SpellCastings.Add( sc );
                _class.SpellsPerLevel[SelectedSpellCastingLevel - 1].Add( sc );
                PropertyHasChanged( "SpellCastings" );
            } );

            RemoveSpellCasting = new Command( ( o ) =>
            {
                var sc = (SpellCasting)o;
                SpellCastings.Remove( sc );
                _class.SpellsPerLevel[SelectedSpellCastingLevel].Remove( sc );
                PropertyHasChanged( "SpellCastings" );
            } );

            CopyPreviousSpellCasting = new Command( ( o ) =>
            {
                var previous = _class.SpellsPerLevel[SelectedSpellCastingLevel - 1];
                var copy = DarkTemplar.DeepClone<List<SpellCasting>>( previous, true );
                _class.SpellsPerLevel[SelectedSpellCastingLevel] = copy;
                SpellCastings = new ObservableCollection<SpellCasting>( copy );
                PropertyHasChanged( "SpellCastings" );
            }, this, () => SelectedSpellCastingLevel > 1, "SelectedSpellCastingLevel" );

            RefreshSkillPreference = new Command((o) =>
            {
                PropertyHasChanged("SkillPreferenceCountAlreadySet");
            });
        }
    }
}
