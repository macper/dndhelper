using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DnDHelper
{
    public class RulesConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("SkillsPath")]
        public string SkillsPath
        {
            get
            {
                return (string)this["SkillsPath"];
            }
            set
            {
                this["SkillsPath"] = value;
            }
        }

        [ConfigurationProperty("AtutesPath")]
        public string AtutesPath
        {
            get
            {
                return (string)this["AtutesPath"];
            }
            set
            {
                this["AtutesPath"] = value;
            }
        }

        [ConfigurationProperty("ClassesPath")]
        public string ClassesPath
        {
            get
            {
                return (string)this["ClassesPath"];
            }
            set
            {
                this["ClassesPath"] = value;
            }
        }

        [ConfigurationProperty("RacesPath")]
        public string RacesPath
        {
            get { return (string) this["RacesPath"]; }
            set { this["RacesPath"] = value; }
        }

        [ConfigurationProperty("SpellsPath")]
        public string SpellsPath
        {
            get
            {
                return (string)this["SpellsPath"];
            }
            set
            {
                this["SpellsPath"] = value;
            }
        }
    }
}
