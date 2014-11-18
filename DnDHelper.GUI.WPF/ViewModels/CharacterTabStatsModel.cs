using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DnDHelper.Domain;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class CharacterTabStatsModel : CharacterTabBaseModel
    {
        private readonly AppFacade _appFacade;
        private readonly IAppAPI _appAPI;

        #region Zakładka górna

        public string Name
        {
            get { return this.Character.Name; }
            set
            {
                Character.Name = value;
                PropertyHasChanged("Name");
                _appFacade.CharacterChange(Character);
            }
        }

        public RaceDefinition Race
        {
            get { return Character.Race != null ? Character.Race.Definition: null; }
            set
            {
                Character.Race = value.CreateItem();
                PropertyHasChanged("Race");
                _appFacade.CharacterChange(Character);
            }
        }

        public ClassDefinition Class
        {
            get { return Character.Class.Count  != 0 ? Character.Class.FirstOrDefault().Definition : null; }
            set
            {
                Character.Class.Clear();
                var @class = value.CreateItem();
                @class.Level = Level;
                Character.Class.Add(@class);
                PropertyHasChanged("Class");
                _appFacade.CharacterChange(Character);
            }
        }

        private Class _currentClass;
        public Class CurrentClass
        {
            get { return _currentClass; }
            set
            {
                _currentClass = value;
                PropertyHasChanged("CurrentClass");
            }
        }

        public ObservableCollection<Class> CharacterClasses { get; set; }

        public IEnumerable<RaceDefinition> Races
        {
            get { return _appFacade.RepoRaces; }
        }

        public IEnumerable<ClassDefinition> Classes
        {
            get { return _appFacade.RepoClasses; }
        }

        public int Level
        {
            get { return Character.Level; }
            set
            {
                if (!Character.Class.Any())
                {
                    _appAPI.HandleOperationResult(OperationResult.Error("Wymagane jest wybranie klasy"));
                    return;
                }
                    
                Character.Class.FirstOrDefault().Level = value;
                _appFacade.CharacterChange(Character);
            }
        }

        public int Experience
        {
            get { return Character.Experience; }
            set { Character.Experience = value; PropertyHasChanged("Experience"); _appFacade.CharacterChange(Character); }
        }

        public bool IsMultiClass
        {
            get { return Character.Class.Count > 1; }
        }

        public int BattleInitiative
        {
            get { return BattleCharacter == null ? 0 : BattleCharacter.Initiative; }
            set
            {
                BattleCharacter.Initiative = value;
                PropertyHasChanged("BattleInitiative");
                PropertyHasChanged("TotalInitiative");
                _appAPI.ExecuteGlobalCommand(GlobalCommands.RefreshBattleMembers, null);
            }
        }

        public int TotalInitiative
        {
            get { return BattleCharacter == null ? 0 : BattleCharacter.TotalInitiative; }
        }

        public ICommand AddClass { get; set; }
        public ICommand RemoveClass { get; set; }
        public ICommand CalculateInitial { get; set; }

        #endregion

        #region Cechy główne

        public int BaseStrength
        {
            get { return Character.OriginalStats.Strength; }
            set
            {
                ChangeMainSkill(BaseAttribute.Strength, "Strength", Character.OriginalStats.Strength, value);
            }
        }

        

        public int CurrentStrength
        {
            get { return Character.CurrentStats.Strength; }
        }

        public int StrengthBonus
        {
            get { return Rules.GetStandardBonus(Character.CurrentStats.Strength); }
        }

        public int BaseDexterity
        {
            get { return Character.OriginalStats.Dexterity; }
            set
            {
                ChangeMainSkill(BaseAttribute.Dexterity, "Dexterity", Character.OriginalStats.Dexterity, value);
            }
        }

        public IEnumerable<BaseBonus> StrengthBonuses
        {
            get
            {
                var val = Character.Bonuses.Where(b => b.GetType() == typeof(MainSkillBonus) && ((MainSkillBonus)b).Attribute == BaseAttribute.Strength).ToArray();
                return Character.Bonuses.Where(b => b.GetType() == typeof(MainSkillBonus) && ((MainSkillBonus)b).Attribute == BaseAttribute.Strength).ToArray();
            }
        }

        public int CurrentDexterity
        {
            get { return Character.CurrentStats.Dexterity; }
        }

        public int DexterityBonus
        {
            get { return Rules.GetStandardBonus(Character.CurrentStats.Dexterity); }
        }

        public IEnumerable<BaseBonus> DexterityBonuses
        {
            get { return Character.Bonuses.Where(b => b.GetType() == typeof(MainSkillBonus) && ((MainSkillBonus)b).Attribute == BaseAttribute.Dexterity).ToArray(); }
        }

        public int BaseConstitution
        {
            get { return Character.OriginalStats.Constitution; }
            set
            {
                ChangeMainSkill(BaseAttribute.Constitution, "Constitution", Character.OriginalStats.Constitution, value);
            }
        }

        public int CurrentConstitution
        {
            get { return Character.CurrentStats.Constitution; }
        }

        public int ConstitutionBonus
        {
            get { return Rules.GetStandardBonus(Character.CurrentStats.Constitution); }
        }

        public IEnumerable<BaseBonus> ConstitutionBonuses
        {
            get { return Character.Bonuses.Where(b => b.GetType() == typeof(MainSkillBonus) && ((MainSkillBonus)b).Attribute == BaseAttribute.Constitution).ToArray(); }
        }

        public int BaseIntelligence
        {
            get { return Character.OriginalStats.Inteligence; }
            set
            {
                ChangeMainSkill(BaseAttribute.Inteligence, "Intelligence", Character.OriginalStats.Inteligence, value);
            }
        }

        public int CurrentIntelligence
        {
            get { return Character.CurrentStats.Inteligence; }
        }

        public int IntelligenceBonus
        {
            get { return Rules.GetStandardBonus(Character.CurrentStats.Inteligence); }
        }

        public IEnumerable<BaseBonus> IntelligenceBonuses
        {
            get { return Character.Bonuses.Where(b => b.GetType() == typeof(MainSkillBonus) && ((MainSkillBonus)b).Attribute == BaseAttribute.Inteligence).ToArray(); }
        }

        public int BaseWisdom
        {
            get { return Character.OriginalStats.Wisdom; }
            set
            {
                ChangeMainSkill(BaseAttribute.Wisdom, "Wisdom", Character.OriginalStats.Wisdom, value);
            }
        }

        public int CurrentWisdom
        {
            get { return Character.CurrentStats.Wisdom; }
        }

        public int WisdomBonus
        {
            get { return Rules.GetStandardBonus(Character.CurrentStats.Wisdom); }
        }

        public IEnumerable<BaseBonus> WisdomBonuses
        {
            get { return Character.Bonuses.Where(b => b.GetType() == typeof(MainSkillBonus) && ((MainSkillBonus)b).Attribute == BaseAttribute.Wisdom).ToArray(); }
        }

        public int BaseCharisma
        {
            get { return Character.OriginalStats.Charisma; }
            set
            {
                ChangeMainSkill(BaseAttribute.Charisma, "Charisma", Character.OriginalStats.Charisma, value);
            }
        }

        public int CurrentCharisma
        {
            get { return Character.CurrentStats.Charisma; }
        }

        public int CharismaBonus
        {
            get { return Rules.GetStandardBonus(Character.CurrentStats.Charisma); }
        }

        public IEnumerable<BaseBonus> CharismaBonuses
        {
            get { return Character.Bonuses.Where(b => b.GetType() == typeof(MainSkillBonus) && ((MainSkillBonus)b).Attribute == BaseAttribute.Charisma).ToArray(); }
        }

        public int AvailableMainSkillPoints
        {
            get { return Character.SkillPointsLeft.MainSkills; }
            set 
            { 
                Character.SkillPointsLeft.MainSkills = value;
                PropertyHasChanged("AvailableMainSkillPoints");
            }
        }

        private void SubtractMainPointsLeft(int value)
        {
            AvailableMainSkillPoints -= value;
        }

        public void CalculatedPropertyChanged()
        {
            PropertyHasChanged("BaseStrength");
            PropertyHasChanged("BaseDexterity");
            PropertyHasChanged("BaseConstitution");
            PropertyHasChanged("BaseIntelligence");
            PropertyHasChanged("BaseWisdom");
            PropertyHasChanged("BaseCharisma");
            PropertyHasChanged("CurrentStrength");
            PropertyHasChanged("CurrentDexterity");
            PropertyHasChanged("CurrentConstitution");
            PropertyHasChanged("CurrentIntelligence");
            PropertyHasChanged("CurrentWisdom");
            PropertyHasChanged("CurrentCharisma");
            PropertyHasChanged("StrengthBonus");
            PropertyHasChanged("DexterityBonus");
            PropertyHasChanged("ConstitutionBonus");
            PropertyHasChanged("IntelligenceBonus");
            PropertyHasChanged("WisdomBonus");
            PropertyHasChanged("CharismaBonus");
            PropertyHasChanged("AvailableMainSkillPoints");
            PropertyHasChanged("StrengthBonuses");
            PropertyHasChanged("BaseWillThrow");
            PropertyHasChanged("BaseReflexThrow");
            PropertyHasChanged("BaseEnduranceThrow");
            PropertyHasChanged("CurrentWillThrow");
            PropertyHasChanged("CurrentReflexThrow");
            PropertyHasChanged("CurrentEnduranceThrow");
            PropertyHasChanged("ReflexThrowBonuses");
            PropertyHasChanged("WillThrowBonuses");
            PropertyHasChanged("EnduranceThrowBonuses");
            PropertyHasChanged("CurrentHP");
            PropertyHasChanged("HPBonuses");
            PropertyHasChanged("AC");
            PropertyHasChanged("ACBonuses");
            PropertyHasChanged("BaseAttackMelee");
            PropertyHasChanged("BaseAttackRange");
            PropertyHasChanged("CurrentAttackMelee");
            PropertyHasChanged("CurrentAttackRange");
            PropertyHasChanged("AttackMeleeBonuses");
            PropertyHasChanged("AttackMeleeRange");
            PropertyHasChanged("BaseAttacksCount");
            PropertyHasChanged("BaseInitiative");
            PropertyHasChanged("CurrentAttacksCount");
            PropertyHasChanged("CurrentInitiative");
            PropertyHasChanged("InitiativeBonuses");
            PropertyHasChanged("CurrentSpeed");
            PropertyHasChanged("SpeedBonuses");
            PropertyHasChanged("Attacks");
            PropertyHasChanged("Life");
            _appFacade.CharacterChange(Character);
        }

        private void ChangeMainSkill(BaseAttribute skill, string skillName, int skillValue, int value)
        {
            var val = value - skillValue;
            Character.Controller.IncreaseMainSkillOriginal(skill, val);
            PropertyHasChanged("Base" + skillName);
            SubtractMainPointsLeft(val);
            CalculatedPropertyChanged();
        }

        #endregion

        #region Rzuty

        public int BaseWillThrow
        {
            get { return Character.OriginalStats.Throws.WillThrow; }
            set
            {
                Character.Controller.ChangeThrowOriginal(value, ThrowType.Will);
                PropertyHasChanged("BaseWillThrow");
                CalculatedPropertyChanged();
            }
        }

        public int CurrentWillThrow
        {
            get { return Character.CurrentStats.Throws.WillThrow; }
        }

        public IEnumerable<BaseBonus> WillThrowBonuses
        {
            get { return GetBonuses(typeof(ThrowBonus)).Where(b => ((ThrowBonus)b).IsThrowType(ThrowType.Will)).Union(GetBonuses(typeof(OverallBonus))); }
        }

        public int BaseReflexThrow
        {
            get { return Character.OriginalStats.Throws.ReflexThrow; }
            set
            {
                Character.Controller.ChangeThrowOriginal(value, ThrowType.Reflex);
                PropertyHasChanged("BaseReflexThrow");
                CalculatedPropertyChanged();
            }
        }

        public int CurrentReflexThrow
        {
            get { return Character.CurrentStats.Throws.ReflexThrow; }
        }

        public IEnumerable<BaseBonus> ReflexThrowBonuses
        {
            get { return GetBonuses(typeof(ThrowBonus)).Where(b => ((ThrowBonus)b).IsThrowType(ThrowType.Reflex)).Union(GetBonuses(typeof(OverallBonus))); }
        }

        public int BaseEnduranceThrow
        {
            get { return Character.OriginalStats.Throws.EnduranceThrow; }
            set
            {
                Character.Controller.ChangeThrowOriginal(value, ThrowType.Endurance);
                PropertyHasChanged("BaseEnduranceThrow");
                CalculatedPropertyChanged();
            }
        }

        public int CurrentEnduranceThrow
        {
            get { return Character.CurrentStats.Throws.EnduranceThrow; }
        }

        public IEnumerable<BaseBonus> EnduranceThrowBonuses
        {
            get { return GetBonuses(typeof(ThrowBonus)).Where(b => ((ThrowBonus)b).IsThrowType(ThrowType.Endurance)).Union(GetBonuses(typeof(OverallBonus))); }
        }

        #endregion

        #region Wyliczalne

        public int BaseHP
        {
            get { return Character.OriginalStats.HP; }
            set
            {
                Character.Controller.ChangeHPOriginal(value - Character.OriginalStats.HP);
                PropertyHasChanged("BaseHP");
                CalculatedPropertyChanged();
            }
        }

        public int CurrentHP
        {
            get { return Character.CurrentStats.HP; }
        }

        public IEnumerable<HPBonus> HPBonuses { get { return GetBonuses<HPBonus>(); } }

        public bool AllowLifeOverflow { get; set; }

        public int Life
        {
            get { return Character.Life; }
            set
            {
                var diff = value - Character.Life;
                if (diff > 0)
                {
                    Character.Controller.Heal(diff, AllowLifeOverflow); 
                }
                else
                {
                    Character.Controller.DoDamage(-1*diff);
                }
                PropertyHasChanged("Life");
                CalculatedPropertyChanged();
            }
        }

        public int AC
        {
            get { return Character.CurrentStats.AC.Total; }
        }

        public IEnumerable<ACBonus> ACBonuses { get { return GetBonuses<ACBonus>(); } }

        public ICommand ChangeAC { get; set; }

        public int BaseAttackMelee
        {
            get { return Character.OriginalStats.Attack.Melee; }
            set
            {
                Character.Controller.ChangeAttackSkillOriginal(value - Character.OriginalStats.Attack.Melee, true);
                PropertyHasChanged("BaseAttackMelee");
                CalculatedPropertyChanged();
            }
        }

        public int BaseAttackRange
        {
            get { return Character.OriginalStats.Attack.Range; }
            set
            {
                Character.Controller.ChangeAttackSkillOriginal(value - Character.OriginalStats.Attack.Range, false);
                PropertyHasChanged("BaseAttackRange");
                CalculatedPropertyChanged();
            }
        }

        public int CurrentAttackMelee
        {
            get { return Character.CurrentStats.Attack.Melee; }
        }

        public int CurrentAttackRange
        {
            get { return Character.CurrentStats.Attack.Range; }
        }

        public IEnumerable<BaseBonus> AttackMeleeBonuses { get { return GetBonuses(typeof(AttackBonus)).Where(b => ((AttackBonus)b).Melee).Union(GetBonuses(typeof(OverallBonus))); } }
        public IEnumerable<BaseBonus> AttackRangeBonuses { get { return GetBonuses(typeof(AttackBonus)).Where(b => !((AttackBonus)b).Melee).Union(GetBonuses(typeof(OverallBonus))); } }

        public int CurrentAttacksCount
        {
            get { return Character.CurrentStats.Attack.NumberOfAttacks; }
        }

        public int BaseAttacksCount
        {
            get { return Character.OriginalStats.Attack.NumberOfAttacks; }
            set
            {
                Character.Controller.ChangeAttacksCountOriginal(value - Character.OriginalStats.Attack.NumberOfAttacks);
                PropertyHasChanged("BaseAttacksCount");
                CalculatedPropertyChanged();
            }
        }

        public int BaseInitiative
        {
            get { return Character.OriginalStats.Initiative; }
            set
            {
                Character.Controller.ChangeInitiativeOriginal(value - Character.OriginalStats.Initiative);
                PropertyHasChanged("BaseInitiative");
                CalculatedPropertyChanged();
            }
        }

        public int CurrentInitiative
        {
            get { return Character.CurrentStats.Initiative; }
        }

        public IEnumerable<InitiativeBonus> InitiativeBonuses { get { return GetBonuses<InitiativeBonus>(); } }

        public int BaseSpeed
        {
            get { return Character.OriginalStats.Speed; }
            set
            {
                Character.Controller.ChangeSpeedOriginal(value - Character.OriginalStats.Speed);
                PropertyHasChanged("BaseSpeed");
                CalculatedPropertyChanged();
            }
        }

        public int CurrentSpeed
        {
            get { return Character.CurrentStats.Speed; }
        }

        public IEnumerable<SpeedBonus> SpeedBonuses { get { return GetBonuses<SpeedBonus>(); } }

        #endregion

        #region Ataki

        public ObservableCollection<Attack> Attacks { get { return new ObservableCollection<Attack>(Character.Attacks); }}
        private Attack _selectedAttack;
        public Attack SelectedAttack { get { return _selectedAttack; } set { _selectedAttack = value; PropertyHasChanged("SelectedAttack"); } }

        public ICommand AddAttack { get; private set; }
        public ICommand RemoveAttack { get; private set; }
        public ICommand EditAttack { get; private set; }

        #endregion

        public CharacterTabStatsModel(Character character)
            : base(character)
        {
            _appFacade = ServiceContainer.GetInstance<AppFacade>();
            _appAPI = ServiceContainer.GetInstance<IAppAPI>();

            CharacterClasses = new ObservableCollection<Class>(character.Class);
            AddClass = new Command((o) => _appAPI.RedirectToViewModel(new AddClassViewModel(Character, Classes), () =>
            {
                CharacterClasses = new ObservableCollection<Class>(Character.Class);
                PropertyHasChanged("CharacterClasses");
                PropertyHasChanged("Level");
                PropertyHasChanged("IsMultiClass");
            }));

            RemoveClass = new Command((o) =>
            {
                Character.Class.Remove(CurrentClass);
                _appFacade.CharacterChange(Character);
                CharacterClasses = new ObservableCollection<Class>(Character.Class);
                PropertyHasChanged("CharacterClasses");
                PropertyHasChanged("Level");
                PropertyHasChanged("IsMultiClass");
            }, this, () => CurrentClass != null, "CurrentClass");

            CalculateInitial = new Command((o) => _appAPI.HandleOperationResult(_appFacade.CalculateInitial(Character), CalculatedPropertyChanged));

            ChangeAC = new Command((o) => _appAPI.RedirectToViewModel(new ChangeACModel(Character), () =>
            {
                PropertyHasChanged("AC");
                CalculatedPropertyChanged();
            }));

            AddAttack = new Command((o) => _appAPI.RedirectToViewModel(new AddAttackViewModel(Character, new CustomAttack() { Bonuses = new List<BaseBonus>(new[] { new DamageBonus(null, new DamageBone(1, 1)) }) }, false), () => { PropertyHasChanged("Attacks"); _appFacade.CharacterChange(Character); }));
            EditAttack = new Command((o) => _appAPI.RedirectToViewModel(new AddAttackViewModel(Character, Character.CustomAttacks.Single(a => a.Name == SelectedAttack.Name), true), () => { PropertyHasChanged("Attacks"); _appFacade.CharacterChange(Character); }), this, () => SelectedAttack != null && SelectedAttack.Custom, "SelectedAttack");
            RemoveAttack = new Command((o) =>
                                           {
                                              _appAPI.HandleOperationResult(Character.Controller.RemoveCustomAttack(Character.CustomAttacks.SingleOrDefault(a => a.Name == SelectedAttack.Name))); 
                                              PropertyHasChanged("Attacks");
                                              _appFacade.CharacterChange(Character);
                                           }, this, () => SelectedAttack != null && SelectedAttack.Custom, "SelectedAttack");
        }

        private IEnumerable<T> GetBonuses<T>() where T : BaseBonus
        {
            return Character.Bonuses.Where(b => b.GetType() == typeof(T)).Cast<T>().ToArray();
        }

        private IEnumerable<BaseBonus> GetBonuses(Type type)
        {
            return Character.Bonuses.Where(b => b.GetType() == type);
        }
    }
}
