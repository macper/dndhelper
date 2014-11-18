using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Xml.Serialization;

namespace DnDHelper
{
    [Serializable]
    public class Character : IComparable
    {
        // Stałe
        public string Name { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public string Class { get; set; }
        public int Experience { get; set; }
        public string Appearance { get; set; }
        public Stats OriginalStats { get; set; }
        public List<Effect> Effects { get; set; }
        public int Gold { get; set; }
        public string ImagePath { get; set; }
        public List<KilledCreature> Kills { get; set; }

        // Wyliczalne:
        public Stats CurrentStats { get; set; }
        public int Initiative { get; set; }
        public int BaseInitiative { get; set; }
        public List<Attack> Attacks { get; set; }
        
        [System.Xml.Serialization.XmlIgnore]
        public List<SpellDefinition> KnownSpells { get; set; }
        public List<Spell> Spells { get; set; }        
        public List<SpellCasting> AvailableCastings { get; set; }
        public string Speed { get; set; }

        public bool IsActiveMember { get; set; }
        public bool IsAlive { get { return CurrentStats.HP > 0; } }

        
        public List<string> KnownSpellsS { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Atut> Atutes { get; set; }

        public Item RightHand { get; set; }
        public Item LeftHand { get; set; }
        public Item Torso { get; set; }
        public Item SecondWeapon { get; set; }
        public Item Neclease { get; set; }
        public Item RightRing { get; set; }
        public Item LeftRing { get; set; }
        public Item Belt { get; set; }
        public Item Boots { get; set; }
        public Item Cloak { get; set; }
        public Item Gloves { get; set; }
        public Item Helmet { get; set; }
        public List<Item> Arrows { get; set; }
        public List<Item> Staffes { get; set; }
        public List<Item> Potions { get; set; }
        public List<Item> Others { get; set; }

        public Character()
        {
            Attacks = new List<Attack>();
            CurrentStats = new Stats();
            OriginalStats = new Stats();
            Effects = new List<Effect>();
            Spells = new List<Spell>();
            Kills = new List<KilledCreature>();
            Potions = new List<Item>();
            Staffes = new List<Item>();
            Arrows = new List<Item>();
            Others = new List<Item>();
            Skills = new List<Skill>();
            KnownSpellsS = new List<string>();
            Atutes = new List<Atut>();
            KnownSpells = new List<SpellDefinition>();
        }

        public void Deserialize()
        {
            foreach (string spellDef in KnownSpellsS)
            {
                SpellDefinition sd = Rules.Spells.Single(s => s.Name == spellDef);
                if (sd != null)
                {
                    KnownSpells.Add(sd);
                }
            }

            foreach (var atut in Atutes)
            {
                atut.Name = Rules.AtutesDictionary[atut.ID].Name;
                atut.Requirements = Rules.AtutesDictionary[atut.ID].Requirements;
                atut.Description = Rules.AtutesDictionary[atut.ID].Description;
                atut.AdditionalInfo = Rules.AtutesDictionary[atut.ID].AdditionalInfo;
            }

            foreach (var skill in Skills)
            {
                skill.BonusProperty = Rules.SkillsDefinition[skill.Name].BonusProperty;
                skill.Description = Rules.SkillsDefinition[skill.Name].Description;
            }
        }

        public void Serialize()
        {
            foreach (var spellDefinition in KnownSpells)
            {
                KnownSpellsS.Add(spellDefinition.Name);
            }
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

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return ((Character)obj).Initiative - Initiative;
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
