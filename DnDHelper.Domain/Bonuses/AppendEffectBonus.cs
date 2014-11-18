using System;

namespace DnDHelper.Domain.Bonuses
{
    public class AppendEffectBonus : BaseBonus
    {
        public string EffectName { get; set; }

        public AppendEffectBonus()
        {
        }

        public AppendEffectBonus(string source, string name) : base(source, 0)
        {
            EffectName = name;
        }

        public override void Evaluate(Character character)
        {
            var effectRepo = ServiceContainer.GetInstance<RepositorySet>().Get<EffectDefinition>();
            var effectDef = effectRepo.GetElementByName(EffectName);
            if (effectDef == null)
                throw new NotImplementedException("Brak efektu: " + EffectName);
            var effect = effectDef.CreateItem();
            effect.Calculated = true;
            character.Effects.Add(effect);
        }

        public override string Name
        {
            get { return "Dodaje efekt"; }
        }

        public override string Description
        {
            get { return string.Format("{0}", EffectName);}
        }

        public override bool IsPositive
        {
            get { return true; }
        }
    }
}