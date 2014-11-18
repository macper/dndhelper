using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Configuration;
using System.Xml.Serialization;
using System.Reflection;
using System.IO;

namespace DnDHelper
{
    public static class Rules
    {
        // TODO : wywalić wszystko, żadnych memberów i propertisów !

        private static Dictionary<string, Skill> _skillsDefinition;
        public static Dictionary<string, Skill> SkillsDefinition
        {
            get
            {
                return _skillsDefinition;
            }
        }

        public static AtutesDictionary AtutesDictionary { get; set; }
        public static ClassesDefinition ClassTable { get; set; }

        private static Dictionary<string, Race> _raceTable;
        public static Dictionary<string, Race> RaceTable
        {
            get
            {
                return _raceTable;
            }
        }

        public static List<SpellDefinition> Spells { get; set; }

        public static void LoadDefinitions(string sectionName)
        {
            RulesConfigSection rulesConf = (RulesConfigSection)ConfigurationManager.GetSection(sectionName);
            _skillsDefinition = new Dictionary<string, Skill>();
            XDocument xSkills = XDocument.Load(rulesConf.SkillsPath);
            var skills = from xml in xSkills.Descendants("Skill") select xml;
            foreach (var skill in skills)
            {
                Skill s = new Skill();
                s.Name = skill.Attribute("Name").Value;
                s.Description = skill.Element("Description").Value;
                s.BonusProperty = (BaseAttribute)Enum.Parse(typeof(BaseAttribute), skill.Element("BonusProperty").Value);
                _skillsDefinition.Add(s.Name, s);
            }

            AtutesDictionary = new AtutesDictionary(rulesConf.AtutesPath);
            using (FileStream fs = new FileStream(rulesConf.ClassesPath, FileMode.Open))
            {
                XmlSerializer serialier = new XmlSerializer(typeof(DnDHelper.ClassesDefinition));
                ClassTable = (DnDHelper.ClassesDefinition)serialier.Deserialize(fs);
            }

            using (FileStream fs = new FileStream(rulesConf.SpellsPath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<SpellDefinition>));
                Spells = (List<SpellDefinition>)serializer.Deserialize(fs);
            }
        }

        public static int GetStandardBonus(int value)
        {
            return (int)((value - 10) / 2);
        }

        public static int GetBaseAttack(SkillRate skillRate, int level)
        {
            switch (skillRate)
            {
                case SkillRate.High:
                    return level;

                case SkillRate.Medium:
                    return (int) Math.Ceiling(level*0.75);

                case SkillRate.Low:
                    return (int)(level * 0.5);

                default:throw new NotImplementedException();
            }
        }
       
        #region Rzuty

        public static Dictionary<int, Throw> GetThrowTable(bool highEndurance, bool highReflex, bool highWill)
        {
            Dictionary<int, Throw> dict = new Dictionary<int, Throw>();
            Throw currentThrow = new Throw();
            if (highEndurance) currentThrow.EnduranceThrow = 2;
            if (highReflex) currentThrow.ReflexThrow = 2;
            if (highWill) currentThrow.WillThrow = 2;
            int level = 1;
            while (level <= 20)
            {
                if (level % 2 == 0)
                {
                    if (highEndurance) currentThrow.EnduranceThrow++;
                    if (highReflex) currentThrow.ReflexThrow++;
                    if (highWill) currentThrow.WillThrow++;
                }
                if (level % 3 == 0)
                {
                    if (!highEndurance) currentThrow.EnduranceThrow++;
                    if (!highReflex) currentThrow.ReflexThrow++;
                    if (!highWill) currentThrow.WillThrow++;
                }
                dict[level] = new Throw() { EnduranceThrow = currentThrow.EnduranceThrow, ReflexThrow = currentThrow.ReflexThrow, WillThrow = currentThrow.WillThrow };
                level++;
            }
            return dict;
        }

        #endregion
   
    }



    public enum BaseAttribute { Strength, Dexterity, Constitution, Wisdom, Inteligence, Charisma };

    public enum SkillRate { Low, Medium, High };
   

    
        
}
