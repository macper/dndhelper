using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DnDHelper.Domain;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class SkillViewModel : ViewModelBase
    {
        private readonly CharacterTabSkillsModel _parentView;
        public Skill OriginalSkill
        {
            get
            {
                return _character.OriginalStats.Skills.SingleOrDefault(s => s.Name == CurrentSkill.Name) ??
                            ServiceContainer.GetInstance<AppFacade>().RepoSkills.Single(s => s.Name == CurrentSkill.Name).CreateItem();        
            }
        }
        public Skill CurrentSkill { get; private set; }
        private readonly Character _character;

        public int BaseValue
        {
            get { return OriginalSkill.Value; }
            set
            {
                var originalValue = OriginalSkill.Value;
                ServiceContainer.GetInstance<IAppAPI>().HandleOperationResult(_character.Controller.ChangeSecondarySkillOriginal(OriginalSkill.Name, value - originalValue));
                ServiceContainer.GetInstance<AppFacade>().CharacterChange(_character);
                PropertyHasChanged("BaseValue"); 
                PropertyHasChanged("TotalValue");
                _parentView.RefreshView();
            }
        }

        public int TotalValue
        {
            get
            {
                return _character.CurrentStats.Skills.SingleOrDefault(s => s.Name == CurrentSkill.Name) == null
                           ? CurrentSkill.Value
                           : _character.CurrentStats.Skills.SingleOrDefault(s => s.Name == CurrentSkill.Name).Value;
            }
        }

        public IEnumerable<BaseBonus> Bonuses
        {
            get
            {
                return _character.Bonuses.Where(b =>
                    (b.GetType() == typeof(SecondarySkillBonus) && ((SecondarySkillBonus)b).SkillName == OriginalSkill.Name) ||
                    (b.GetType() == typeof(OverallBonus)) ||
                    (b.GetType() == typeof(PanaltyBonus) && OriginalSkill.Definition.PanaltyModifier > 0));
            }
        }

        public SkillViewModel(Character character, Skill owner, CharacterTabSkillsModel parentView)
        {
            _character = character;
            CurrentSkill = owner;
            _parentView = parentView;
        }
    }
}
