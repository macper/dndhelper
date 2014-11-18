using System.Collections.Generic;

namespace DnDHelper.Domain
{
    public class AttackInfo
    {
        public List<AttackInfoHeader> AttacksHeaders { get; set; }

        public List<AttackEffectModifier> AttackerModifiers { get; set; }

        public List<AttackEffectModifier> DefenderModifiers { get; set; }

        public AttackInfo()
        {
            AttacksHeaders = new List<AttackInfoHeader>();
            AttackerModifiers = new List<AttackEffectModifier>();
            DefenderModifiers = new List<AttackEffectModifier>();
        }
    }

    public class AttackInfoHeader
    {
        public bool Disabled { get; set; }
        public string Header { get; set; }
        public List<AttackViewModel> Attacks { get; set; }

        public AttackInfoHeader()
        {
            Attacks = new List<AttackViewModel>();
        }
    }

    public class AttackEffectModifier
    {
        public bool Positive { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }
    }
}