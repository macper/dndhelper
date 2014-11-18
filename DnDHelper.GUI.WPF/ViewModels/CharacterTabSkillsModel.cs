using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class CharacterTabSkillsModel : CharacterTabBaseModel
    {
        public ObservableCollection<Skill> Skills
        {
            get { return new ObservableCollection<Skill>(Character.GetActualSkills());}
        }
        public ObservableCollection<Atut> Atutes { get { return new ObservableCollection<Atut>(Character.OriginalStats.Atutes.Union(Character.CurrentStats.Atutes.Where(a => !Character.OriginalStats.Atutes.Any(o => o.Name == a.Name)))); } }

        private Skill _selectedSkill;
        public Skill SelectedSkill { get { return _selectedSkill; } 
            set 
            { 
                _selectedSkill = value; 
                PropertyHasChanged("SelectedSkill");
                    if (value == null)
                        return;
                CurrentModel = new SkillViewModel(Character, SelectedSkill, this);
            } 
        }

        private Atut _selectedAtut;
        public Atut SelectedAtut 
        { 
            get { return _selectedAtut; } 
            set 
            { 
                _selectedAtut = value; 
                PropertyHasChanged("SelectedAtut"); 
                if (value == null)
                    return;
                CurrentModel = SelectedAtut;
            } 
        }

        private object _currentModel;
        public object CurrentModel { get { return _currentModel; } set { _currentModel = value; PropertyHasChanged("CurrentModel"); } }

        public ICommand AddAtut { get; private set; }
        public ICommand RemoveAtut { get; private set; }

        private readonly Lazy<IAppAPI> _api = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);
        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);
        
        public int SkillPointsLeft
        {
            get { return Character.SkillPointsLeft.SecondarySkills; }
            set
            {
                Character.SkillPointsLeft.SecondarySkills = value;
                PropertyHasChanged("SkillPointsLeft");
            }

        }

        public int AtutesPointsLeft
        {
            get { return Character.SkillPointsLeft.Atutes; }
            set
            {
                Character.SkillPointsLeft.Atutes = value;
                PropertyHasChanged("AtutesPointsLeft");
            }
        }

        public CharacterTabSkillsModel(Character character)
            : base(character)
        {
            AddAtut = new Command((o) => _api.Value.RedirectToViewModel(new RepoAtutesViewModel(true, Character), () => { PropertyHasChanged("Atutes"); AtutesPointsLeft--; _appFacade.Value.CharacterChange(Character); }), this, () => AtutesPointsLeft > 0, "AtutesPointsLeft");
            RemoveAtut = new Command((o) => _api.Value.HandleOperationResult(Character.Controller.RemoveAtuteOriginal(SelectedAtut), () => { PropertyHasChanged("Atutes"); _appFacade.Value.CharacterChange(Character); }), this, () => SelectedAtut != null, "SelectedAtut");
            RefreshView();
        }

        public void RefreshView()
        {
            PropertyHasChanged("Skills");
            PropertyHasChanged("SkillPointsLeft");
            PropertyHasChanged("AtutesPointsLeft");
        }
    }
}
