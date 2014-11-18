using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class AddBonusViewModel : ViewModelBase
    {
        private string _bonusName;
        public string BonusName
        {
            get { return _bonusName; }
            set 
            { 
                _bonusName = value; 
                PropertyHasChanged("BonusName");
                Bonus = BonusFactory.GetByName(_bonusName);
            }
        }

        private BaseBonus _bonus;
        public  BaseBonus Bonus
        {
            get { return _bonus; }
            set { _bonus = value; PropertyHasChanged("Bonus"); }
        }

        public IEnumerable<EffectDefinition> Effects { get { return ServiceContainer.GetInstance<AppFacade>().RepoEffects.ToArray(); } }
        public IEnumerable<AtutDefinition> Atutes { get { return ServiceContainer.GetInstance<AppFacade>().RepoAtutes.ToArray(); } }
        public IEnumerable<SkillDefinition> Skills { get { return ServiceContainer.GetInstance<AppFacade>().RepoSkills.ToArray(); } }

        public ICommand Commit { get; set; }

        public AddBonusViewModel()
        {
            Commit = new Command((o) => CommandHasExecuted("Commit", OperationResult.Success()));
        }

        public AddBonusViewModel(BaseBonus bonus) : this()
        {
            Bonus = bonus;
        }
    }
}
