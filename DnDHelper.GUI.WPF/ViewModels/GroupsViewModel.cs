using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class GroupsViewModel : ViewModelBase
    {
        private readonly AppFacade _appFacade;
        private readonly IAppAPI _appAPI;

        public GroupsViewModel()
        {
            _appFacade = ServiceContainer.GetInstance<AppFacade>();
            _appAPI = ServiceContainer.GetInstance<IAppAPI>();

            Characters = new ObservableCollection<Character>( _appFacade.AllCharacters );

            AddGroup = new Command( ( o ) =>
                                       {
                                           var model = new AddGroupViewModel();
                                           _appAPI.RedirectToViewModel( model, () => PropertyHasChanged( "Groups" ) );
                                       } );
            RemoveGroup = new Command( ( o ) => _appAPI.RedirectToViewModel( new RemoveGroupViewModel( CurrentGroup ), () =>
                                            {
                                                PropertyHasChanged( "Groups" );
                                                CurrentCharacter = null;
                                                CurrentGroup = null;
                                            } ), this, () => CurrentGroup != null, "CurrentGroup" );
            AddCharacter = new Command( ( o ) =>
                                           {
                                               CurrentCharacter = new Character() { Name = "Nowa postać" };
                                               _appAPI.HandleOperationResult( _appFacade.AddCharacter( CurrentCharacter, CurrentGroup ), () =>
                                                {
                                                    CurrentGroup = CurrentGroup;
                                                    PropertyHasChanged( "Characters" );
                                                } );
                                           }, this, () => CurrentGroup != null, "CurrentGroup" );
            RemoveCharacter = new Command( ( o ) => _appAPI.RedirectToViewModel( new RemoveCharacterViewModel( CurrentCharacter ), () =>
                                            {
                                                CurrentCharacter = null;
                                                CurrentGroup = CurrentGroup;
                                                PropertyHasChanged( "Characters" );
                                            } ), this, () => CurrentCharacter != null, "CurrentCharacter" );
            CopyCharacter = new Command( ( o ) => _appAPI.HandleOperationResult( _appFacade.CopyCharacter( CurrentCharacter ), () => { CurrentGroup = CurrentGroup; PropertyHasChanged( "Characters" ); } ), this, () => CurrentCharacter != null, "CurrentCharacter" );
            MoveCharacter = new Command( ( o ) =>
                                            {
                                                var model = new MoveCharacterViewModel( _appFacade.RepoGroups, CurrentCharacter );
                                                _appAPI.RedirectToViewModel( model, () => { CurrentGroup = CurrentGroup; PropertyHasChanged( "Characters" ); } );
                                            }, this, () => CurrentCharacter != null, "CurrentCharacter" );

            _appAPI.RegisterGlobalCommand( new Command( ( o ) => PropertyHasChanged( "Groups" ) ), GlobalCommands.RefreshGroups );
            _appAPI.RegisterGlobalCommand( new Command( ( o ) => CharacterViewModel = new CharacterViewModel( (BattleCharacter)o ) ), GlobalCommands.SetCharacter );

            Races = _appFacade.RepoRaces.Select( r => r.Name ).ToList();
            Races.Insert( 0, "" );

            Classes = _appFacade.RepoClasses.Select( r => r.Name ).ToList();
            Classes.Insert( 0, "" );
        }

        public ObservableCollection<CharacterGroup> Groups { get { return new ObservableCollection<CharacterGroup>( _appFacade.RepoGroups ); } }
        public ObservableCollection<Character> Characters { get; set; }

        private CharacterGroup _currentGroup;
        public CharacterGroup CurrentGroup
        {
            get { return _currentGroup; }
            set
            {
                _currentGroup = value;
                PropertyHasChanged( "CurrentGroup" );
                FillList();
            }
        }

        private Character _character;
        public Character CurrentCharacter { get { return _character; } set { _character = value; PropertyHasChanged( "CurrentCharacter" ); CharacterViewModel = new CharacterViewModel( value ); } }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                FillList();
            }
        }

        private string _race;
        public string Race
        {
            get
            {
                return _race;
            }
            set
            {
                _race = value;
                FillList();
            }
        }

        private string _class;
        public string Class
        {
            get
            {
                return _class;
            }
            set
            {
                _class = value;
                FillList();
            }
        }

        private int _level;
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                FillList();
            }
        }

        private bool _filterByGroup;
        public bool FilterByGroup
        {
            get
            {
                return _filterByGroup;
            }
            set
            {
                _filterByGroup = value;
                FillList();
            }
        }

        public ICommand AddGroup { get; set; }
        public ICommand RemoveGroup { get; set; }
        public ICommand AddCharacter { get; set; }
        public ICommand RemoveCharacter { get; set; }
        public ICommand CopyCharacter { get; set; }
        public ICommand MoveCharacter { get; set; }

        private CharacterViewModel _characterViewModel;
        public CharacterViewModel CharacterViewModel
        {
            get { return _characterViewModel; }
            set
            {
                _characterViewModel = value;
                PropertyHasChanged( "CharacterViewModel" );
            }
        }

        private void FillList()
        {
            Characters = new ObservableCollection<Character>( _appFacade.AllCharacters
                .Where( c =>
                    ( string.IsNullOrEmpty( Name ) || c.Name.ToLower().StartsWith( Name.ToLower() ) ) &&
                    ( string.IsNullOrEmpty( Race ) || c.Race.Name == Race ) &&
                    ( string.IsNullOrEmpty( Class ) || c.Class.Any( k => k.Name == Class ) ) &&
                    ( Level == 0 || c.Class.Sum( k => k.Level ) == Level ) &&
                    ( FilterByGroup == false || c.GroupName == CurrentGroup.Name )
                    ).OrderBy( x => x.Name ) );
            PropertyHasChanged( "Characters" );
        }

        public IList<string> Classes { get; set; }

        public IList<string> Races { get; set; }
    }
}
