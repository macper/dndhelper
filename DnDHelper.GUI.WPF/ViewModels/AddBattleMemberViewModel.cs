using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class AddBattleMemberViewModel : ViewModelBase
    {
        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);
        private readonly Lazy<IAppAPI> _appApi = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);

        public IEnumerable<CharacterGroup> Groups { get { return _appFacade.Value.RepoGroups.Where(g => string.IsNullOrEmpty(GroupSearchName) || g.Name.ToLower().StartsWith(GroupSearchName.ToLower())).ToArray(); } }
        public IEnumerable<Character> Characters 
        { 
            get
            {
                if (SelectedGroup == null)
                    return null;
                return SelectedGroup.Characters.Where(m => string.IsNullOrEmpty(CharacterSearchName) || m.Name.ToLower().StartsWith(CharacterSearchName.ToLower()));
            } 
        }

        public ObservableCollection<BattleCharacter> Members
        {
            get { return new ObservableCollection<BattleCharacter>(Battle.Instance.Members); }
        }

        public CharacterGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set { _selectedGroup = value; PropertyHasChanged("SelectedGroup"); PropertyHasChanged("Characters"); }
        }

        public Character SelectedCharacter
        {
            get { return _selectedCharacter; }
            set { _selectedCharacter = value; PropertyHasChanged("SelectedCharacter"); }
        }

        public BattleCharacter SelectedMember
        {
            get { return _selectedMember; }
            set { _selectedMember = value; PropertyHasChanged("SelectedMember"); }
        }

        public string GroupSearchName
        {
            get { return _groupSearchName; }
            set { _groupSearchName = value; PropertyHasChanged("GroupSearchName"); PropertyHasChanged("Groups"); }
        }

        public string CharacterSearchName
        {
            get { return _characterSearchName; }
            set { _characterSearchName = value; PropertyHasChanged("CharacterSearchName"); PropertyHasChanged("Characters"); }
        }

        private string _nameSuffix;
        public string NameSuffix
        {
            get
            {
                return _nameSuffix;
            }
            set
            {
                _nameSuffix = value;
                PropertyHasChanged( "NameSuffix" );
            }
        }

        private CharacterGroup _selectedGroup;
        private Character _selectedCharacter;
        private BattleCharacter _selectedMember;

        private string _groupSearchName;
        private string _characterSearchName;

        public int Initiative { get; set; }

        public ICommand AddMember { get; private set; }
        public ICommand AddMemberCopy { get; private set; }
        public ICommand RemoveMember { get; private set; }
        public ICommand Commit { get; private set; }
        


        public AddBattleMemberViewModel()
        {
            AddMember = new Command((o) =>
                                        {
                                            if (Initiative <=0)
                                            {
                                                _appApi.Value.HandleOperationResult(OperationResult.Error("Nieprawidłowa inicjatywa"));
                                                return;
                                            }
                                            if (Battle.Instance.Members.Any(m => m.Character == SelectedCharacter))
                                            {
                                                _appApi.Value.HandleOperationResult(OperationResult.Error("Istnieje już taki member!"));
                                                return;
                                            }
                                            Battle.Instance.AddMember(SelectedCharacter, Initiative, NameSuffix);
                                            PropertyHasChanged("Members");
                                        }, this, () => SelectedCharacter != null, "SelectedCharacter");
            AddMemberCopy = new Command((o) =>
                                            {
                                                if (Initiative <= 0)
                                                {
                                                    _appApi.Value.HandleOperationResult(OperationResult.Error("Nieprawidłowa inicjatywa"));
                                                    return;
                                                }
                                                Battle.Instance.AddMemberCopy(SelectedCharacter, Initiative, NameSuffix);
                                                PropertyHasChanged("Members");
                                            }, this, () => SelectedCharacter != null, "SelectedCharacter");
            RemoveMember = new Command((o) =>
                                           {
                                               Battle.Instance.Members.Remove(SelectedMember);
                                               PropertyHasChanged("Members");
                                           }, this, () => SelectedMember != null, "SelectedMember");
            Commit = new Command((o) => CommandHasExecuted("Commit", OperationResult.Success()));
        }
    }
}
