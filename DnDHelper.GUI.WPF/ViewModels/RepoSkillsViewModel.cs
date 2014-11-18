using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class RepoSkillsViewModel : ViewModelBase
    {
        private string _searchName;

        public string SearchName
        {
            get { return _searchName; }
            set { _searchName = value; PropertyHasChanged("SearchName"); FillList(); }
        }

        public ObservableCollection<SkillDefinition> Skills { get; set; }

        private SkillDefinition _selectedSkill;
        public SkillDefinition SelectedSkill
        {
            get
            {
                return _selectedSkill;
            } 
            set
            {
                _selectedSkill = value;
                PropertyHasChanged("SelectedSkill");
            }
        }

        public ICommand AddSkill { get; private set; }
        public ICommand RemoveSkill{ get; private set; }
        public ICommand Commit { get; private set; }

        private readonly Lazy<IAppAPI> _appApi = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);
        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);

        public RepoSkillsViewModel()
        {
            FillList();
            AddSkill = new Command((o) =>
                                        {
                                            SelectedSkill = new SkillDefinition {Name = "(nowa umiejętność)"};
                                        });
            RemoveSkill = new Command((o) =>
                                           {
                                               _appApi.Value.HandleOperationResult(_appFacade.Value.RemoveSkill(SelectedSkill));
                                               PropertyHasChanged("Skills");
                                               FillList();
                                           }, this, () => SelectedSkill != null, "SelectedSkill");
            Commit = new Command((o) =>
                                     {
                                         if (!_appFacade.Value.RepoSkills.Any(s => s.Name == SelectedSkill.Name))
                                         {
                                             _appApi.Value.HandleOperationResult(_appFacade.Value.AddSkill(SelectedSkill));
                                         }
                                         else
                                         {
                                             _appFacade.Value.SkillsChanged( SelectedSkill );
                                         }
                                         _appApi.Value.ShowNotifyToUser("Zapisano pomyślnie");
                                         FillList();
                                     }, this, () => SelectedSkill != null, "SelectedSkill");
        }

        private void FillList()
        {
            var appFacade = ServiceContainer.GetInstance<AppFacade>();
            Skills = new ObservableCollection<SkillDefinition>(appFacade.RepoSkills.Where(e => string.IsNullOrEmpty(_searchName) || e.Name.ToLower().StartsWith(_searchName.ToLower())));
            PropertyHasChanged("Skills");
        }
    }
}
