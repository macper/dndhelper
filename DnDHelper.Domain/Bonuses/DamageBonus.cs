using System;
using System.Collections.Generic;

namespace DnDHelper.Domain.Bonuses
{
    public class DamageBonus : BaseBonus
    {
        public Damage Amount { get; set; }

        public DamageBonus()
        {
            Amount = new Damage();
        }

        public DamageBonus(string source, Damage amount)
            : base(source, 0)
        {
            Amount = amount;
        }

        public DamageBonus(string source, DamageBone amountBone) : base(source, 0)
        {
            Amount = new Damage() { Elements = new List<DamageBone>(new []{amountBone})};
        }

        public override void Evaluate(Character character)
        {
           character.CurrentStats.DamageBonus.AddElement(Amount); 
        }

        public override string Name
        {
            get { return "Premia do obra¿eñ"; }
        }

        public override string Description
        {
            get { return string.Format("{0}", Amount);}
        }

        public override bool IsPositive
        {
            get { return true; }
        }
    }

}