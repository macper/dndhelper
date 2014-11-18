using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DnDHelper.Domain.Bonuses
{
    [XmlRoot(Namespace = "DnDHelper.Domain.Bonuses")]
    [XmlInclude(typeof(MainSkillBonus))]
    [XmlInclude(typeof(SecondarySkillBonus))]
    [XmlInclude(typeof(ACBonus))]
    [XmlInclude(typeof(AttackBonus))]
    [XmlInclude(typeof(ThrowBonus))]
    [XmlInclude(typeof(AtutBonus))]
    [XmlInclude(typeof(DamageBonus))]
    [XmlInclude(typeof(RangeBonus))]
    [XmlInclude(typeof(MaxDexterityBonus))]
    [XmlInclude(typeof(PanaltyBonus))]
    [XmlInclude(typeof(AppendEffectBonus))]
    [XmlInclude(typeof(SpeedBonus))]
    [XmlInclude(typeof(HPBonus))]
    [XmlInclude(typeof(ResistanceBonus))]
    [XmlInclude(typeof(NumberOfAttacksBonus))]
    [XmlInclude(typeof(OverallBonus))]
    [XmlInclude(typeof(WeaponBonus))]
    [XmlInclude(typeof(InitiativeBonus))]
    public abstract class BaseBonus
    {
        public string Source { get; set; }
        public int Value { get; set; }

        protected BaseBonus()
        {
        }

        protected BaseBonus(string source, int value)
        {
            Source = source;
            Value = value;
        }

        public override string ToString()
        {
            return Name + " : " + Description + string.Format(" ({0})", Source);
        }

        public virtual string Description
        {
            get { return string.Format("{0}{1}", Value > 0 ? "+" : "", Value); }
        }

        public string ShortDescription
        {
            get { return string.Format("{0} ({1})", Description, Source); }
        }

        public virtual string Name
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual bool IsPositive
        {
            get { return Value >= 0; }
        }

        public abstract void Evaluate(Character character);
    }
}
