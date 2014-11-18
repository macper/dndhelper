using System;
using System.Xml.Serialization;

namespace DnDHelper.Domain.Bonuses
{
    public class AtutBonus : BaseBonus
    {
        public AtutBonus(string source, int value) : base(source, value)
        {
        }

        public string AtutName { get; set; }
        public string AdditionalInfo { get; set; }

        public AtutBonus()
        {
        }

        public override void Evaluate(Character character)
        {
            var atDef = ServiceContainer.GetInstance<RepositorySet>().Get<AtutDefinition>().GetElementByName(AtutName);
             if (atDef == null)
                 throw new NotImplementedException("Brak atutu: " + AtutName);

            var at = atDef.CreateItem();
            at.AdditionalInfo = AdditionalInfo;
            character.CurrentStats.Atutes.Add(at);
        }

        public override string Name
        {
            get { return "Dodany atut"; }
        }

        public override string Description
        {
            get { return string.Format("{0}", AtutName);}
        }

        public override bool IsPositive
        {
            get { return true; }
        }
    }
}