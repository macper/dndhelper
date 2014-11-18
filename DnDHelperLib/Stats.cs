using System;
using System.Collections.Generic;

namespace DnDHelper
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
        public int AC { get; set; }

        public int Attack { get; set; }
        public Throw Throws { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Atut> Atutes { get; set; }

        // TODO : wywalić
        public string AttackSkill { get; set; }
        public int WillThrow { get; set; }
        public int ReflexThrow { get; set; }
        public int StrongThrow { get; set; }

        public void MoveObsolete(Character character)
        {
            try
            {
                if (AttackSkill.Contains("\\"))
                {
                    Attack = int.Parse(AttackSkill.Substring(0, AttackSkill.IndexOf("\\")));
                }
                else
                {
                    Attack = int.Parse(AttackSkill);
                }
            }
            catch
            {
                

            }
            Throws = new Throw() {EnduranceThrow = StrongThrow, ReflexThrow = ReflexThrow, WillThrow = WillThrow};
            Skills.AddRange(character.Skills);
            Atutes.AddRange(character.Atutes);
        }

        public void IncreaseMainSkill(BaseAttribute skill, int value)
        {
            switch (skill)
            {
                case BaseAttribute.Charisma:
                    Charisma += value;
                    return;

                case BaseAttribute.Constitution:
                    Constitution += value;
                    return;

                case BaseAttribute.Dexterity:
                    Dexterity += value;
                    return;

                case BaseAttribute.Inteligence:
                    Inteligence += value;
                    return;

                case BaseAttribute.Strength:
                    Strength += value;
                    return;

                case BaseAttribute.Wisdom:
                    Wisdom += value;
                    return;
            }
        }

        public Stats()
        {
            Strength = Dexterity = Constitution = Wisdom = Inteligence = 10;
            Throws = new Throw();
            Skills = new List<Skill>();
            Atutes = new List<Atut>();
        }
    }
}