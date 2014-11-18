using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;
using DnDHelper.Domain.Bonuses;
using DnDHelpers.GUI.SPA2.API.DTO;

namespace DnDHelpers.GUI.SPA2.API
{
    public static class DTOFactory
    {
        static List<IDTOMapper> dtoMappers = new List<IDTOMapper>();

        static DTOFactory()
        {
            dtoMappers.Add(new DTOMapper<ItemDefinition, ItemInfo>((item) => new ItemInfo { Id = item.Id.Value, Name = item.Name, Type = EnumsDictionary.ItemTypes.SingleOrDefault(e => (BaseTypes)e.Value == item.BaseType).Name, Price = item.Cost }));
            dtoMappers.Add(new DTOMapper<ItemDefinition, ItemDetails>((item) => new ItemDetails
                    {
                        Name = item.Name,
                        Id = item.Id,
                        IsPrototype = item.IsPrototype,
                        OtherInfo = item.Specials,
                        Price = item.Cost,
                        Prototype = new ItemPrototype { Name = item.PrototypeName },
                        Type = new ItemTypeInfo { TypeName = item.BaseType.ToString(), Description = EnumsDictionary.ItemTypes.Single(s => (BaseTypes)s.Value == item.BaseType).Name },
                        Bonuses = item.Bonuses.Select(b => DTOFactory.GetBonusInfo(b))
                    }));
            dtoMappers.Add(new DTOMapper<ItemDefinition, ItemDetailsResponse>((item) =>
                new ItemDetailsResponse
                {
                    Item = DTOFactory.Create<ItemDetails>(item),
                }));
            dtoMappers.Add(new DTOMapper<ItemDefinition, ItemPrototype>((item) => new ItemPrototype { Name = item.PrototypeName }));
            dtoMappers.Add(new DTOMapper<BaseBonus, BonusInfo>(b => GetBonusInfo(b)));
        }

        public static T Create<T>(object source)
        {
            var mapper = dtoMappers.SingleOrDefault(m => m.CanCreate(source.GetType(), typeof(T)));
            if (mapper == null)
                throw new ApplicationException(string.Format("Nie zdefiniowano mapowania DTO: {0} -> {1}", source.GetType(), typeof(T)));
            return (T)mapper.Create(source);
        }

        public static BonusInfo GetBonusInfo(BaseBonus bonus)
        {
            var bonusInfo = new BonusInfo();
            bonusInfo.Type = bonus.GetType().Name;
            bonusInfo.Prototype = bonus;

            if (bonus is ACBonus)
            {
                bonusInfo.AdditionalRequest = AdditionalDataLoadRequest.ACTypes;
            }
            if (bonus is AppendEffectBonus)
            {
                bonusInfo.AdditionalRequest = AdditionalDataLoadRequest.Effects;
            }
            if (bonus is AtutBonus)
            {
                bonusInfo.AdditionalRequest = AdditionalDataLoadRequest.Atutes;
            }
            if (bonus is DamageBonus)
            {
                bonusInfo.AdditionalRequest = AdditionalDataLoadRequest.Damages;
            }
            if (bonus is MainSkillBonus)
            {
                bonusInfo.AdditionalRequest = AdditionalDataLoadRequest.MainSkills;
            }
            if (bonus is SecondarySkillBonus)
            {
                bonusInfo.AdditionalRequest = AdditionalDataLoadRequest.Skills;
            }
            return bonusInfo;
        }

    }

    internal class DTOMapper<F, T> : IDTOMapper
    {
        private Type _sourceType;
        private Type _destinationType;

        private Func<F, T> _factoryMethod;

        public DTOMapper(Func<F, T> factoryMethod)
        {
            _sourceType = typeof(F);
            _destinationType = typeof(T);
            _factoryMethod = factoryMethod;
        }

        public bool CanCreate(Type source, Type destination)
        {
            return source == _sourceType && destination == _destinationType;
        }

        public object Create(object source)
        {
            return _factoryMethod((F)source);
        }
    }

    interface IDTOMapper
    {
        object Create(object source);
        bool CanCreate(Type source, Type destination);
    }
}