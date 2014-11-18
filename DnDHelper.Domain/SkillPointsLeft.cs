using System;

namespace DnDHelper.Domain
{
    [Serializable]
    public class SkillPointsLeft
    {
        public int MainSkills { get; set; }
        public int SecondarySkills { get; set; }
        public int Atutes { get; set; }
    }
}