using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.Domain
{
    [Serializable]
    [XmlInclude(typeof(BaseEntity))]
    public class Character : BaseEntity, IComparable
    {
        // Stałe
        public int Level { get { return Class.Sum(c => c.Level); } }
        public Race Race { get; set; }
        public List<Class> Class { get; set; }
        public int Experience { get; set; }
        public string Appearance { get; set; }
        public Stats OriginalStats { get; set; }
        public List<Effect> Effects { get; set; }
        public List<EquipItem> MainItems { get; set; }
        public string OtherItems { get; set; }
        public int Gold { get; set; }
        public string ImagePath { get; set; }
        public List<KilledCreature> Kills { get; set; }
        public SkillPointsLeft SkillPointsLeft { get; set; }
        public int Life { get; set; }
        public List<CustomAttack> CustomAttacks { get; set; }
        public string GroupName { get; set; }

        // Wyliczalne:
        public Stats CurrentStats { get; set; }
        public List<Attack> Attacks { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        [IgnoreDataMember]
        public IEnumerable<SpellDefinition> KnownSpells
        {
            get
            {
                var repo = ServiceContainer.GetInstance<RepositorySet>().Get<SpellDefinition>();
                return KnownSpellsNames.Select(repo.GetElementByName).ToArray();
            }
        }

        public List<string> KnownSpellsNames { get; set; }
        public List<Spell> Spells { get; set; }        
        public List<SpellCasting> AvailableCastings { get; set; }

        public List<BaseBonus> InitialBonuses { get; set; }

        [XmlIgnore]
        [IgnoreDataMember]
        public List<BaseBonus> Bonuses { get; set; }

        private static ILogger Logger = ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>(Const.LoggerName, typeof(Character).Name));

        [XmlIgnore]
        [IgnoreDataMember]
        public CharacterController Controller { get; private set; }

        public Character()
        {
            Attacks = new List<Attack>();
            CurrentStats = new Stats();
            OriginalStats = new Stats();
            Effects = new List<Effect>();
            Spells = new List<Spell>();
            Kills = new List<KilledCreature>();
            MainItems = new List<EquipItem>();
            SkillPointsLeft = new SkillPointsLeft();
            KnownSpellsNames = new List<string>();
            InitialBonuses = new List<BaseBonus>();
            Bonuses = new List<BaseBonus>();
            Controller = new CharacterController(this);
            Class = new List<Class>();
            Life = OriginalStats.HP;
            CustomAttacks = new List<CustomAttack>();
        }

        public Item GetItemByPosition(ItemPosition position)
        {
            var ei = MainItems.FirstOrDefault(i => i.Position == position);
            if (ei == null)
                return null;
            return ei.Item;
        }
        
        public string Description
        {
            get
            {
                string strEffects ="";
                foreach (Effect ef in Effects)
                {
                    strEffects += string.Format("{0} ({1}) ", ef.Name, ef.Duration.ToString());
                }

                return string.Format("PŻ:{0} Efekty: {1}", CurrentStats.HP.ToString(), strEffects);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}{2} poz.{3})", Name, Race, Class, Level);
        }

        public IEnumerable<Skill> GetActualSkills()
        {
            var skills = new List<Skill>(CurrentStats.Skills);
            foreach (var skillDefinition in ServiceContainer.GetInstance<AppFacade>().RepoSkills.Where(sd => !skills.Any(s => s.Name == sd.Name)))
            {
                var defaultSkill = skillDefinition.CreateItem();
                skills.Add(defaultSkill);
                ServiceContainer.GetInstance<ICharacterCalculator>().ApplySkillBonus(this, defaultSkill);
            }
            return skills.OrderBy(s => s.Name);
        }
        
        #region IComparable Members

        public int CompareTo(object obj)
        {
            return ((Character)obj).CurrentStats.Initiative - CurrentStats.Initiative;
        }

        #endregion
    }


    public class AtutComparer : IComparer<Atut>
    {
        #region IComparer<Atut> Members

        public int Compare(Atut x, Atut y)
        {
            return string.Compare(x.Name, y.Name);
        }

        #endregion
    }
}
