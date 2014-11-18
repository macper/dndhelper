using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;

namespace DnDHelper.Domain
{
    [Serializable]
    public class Stats
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Inteligence { get; set; }
        public int Charisma { get; set; }
        public int HP { get; set; }
        public AC AC { get; set; }

        public AttackSkill Attack { get; set; }
        public int Initiative { get; set; }
        public int Speed { get; set; }
        public Throw Throws { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Atut> Atutes { get; set; }
        public Damage DamageBonus { get; set; }
        public int Panalty { get; set; }

        public Stats()
        {
            Strength = Dexterity = Constitution = Wisdom = Inteligence = Charisma = 10;
            Throws = new Throw();
            Skills = new List<Skill>();
            Atutes = new List<Atut>();
            AC = new AC();
            Attack = new AttackSkill();
            DamageBonus = new Damage();
            Speed = 9;
        }
    }
}