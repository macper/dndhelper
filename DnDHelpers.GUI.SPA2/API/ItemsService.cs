using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;
using DnDHelper.Domain.Bonuses;
using DnDHelpers.GUI.SPA2.API.DTO;
using log4net;
using ServiceStack.ServiceInterface;

namespace DnDHelpers.GUI.SPA2.API
{
    public class ItemsService : Service
    {
        private static ILog Logger = log4net.LogManager.GetLogger("ItemsService");
        public RepositorySet Repository { get; set; }

        public IEnumerable<ItemInfo> Get(ItemsRequest request)
        {
            if (string.IsNullOrEmpty(request.Name) && request.Type == null)
                return Repository.Get<ItemDefinition>().Elements.Select(s => DTOFactory.Create<ItemInfo>(s)).OrderBy(b => b.Name);    

            BaseTypes type;
            if (!Enum.TryParse<BaseTypes>(request.Type, out type))
                type = BaseTypes.None;
                
            return Repository.Get<ItemDefinition>().Elements.Where(e => (string.IsNullOrEmpty(e.Name) || string.IsNullOrEmpty(request.Name) || e.Name.ToLower().StartsWith(request.Name.ToLower())) &&
                (type == BaseTypes.None || e.BaseType == type)).Select(s => DTOFactory.Create<ItemInfo>(s)).OrderBy(i => i.Name);
        }

        public IEnumerable<ItemTypeInfo> Get(ItemTypesRequest request)
        {
            return EnumsDictionary.ItemTypes.Select(s => new ItemTypeInfo { TypeName = s.Value.ToString(), Description = s.Name });
        }

        public ItemDetailsResponse Get(ItemDetailsRequest request)
        {
            ItemDetailsResponse resposne = null;
            if (request.Id == Guid.Empty)
            {
                resposne = DTOFactory.Create<ItemDetailsResponse>(new ItemDefinition { Name = "(nowy przedmiot)"});
            }
            else
            {
                resposne = DTOFactory.Create<ItemDetailsResponse>(Repository.Get<ItemDefinition>().GetElementById(request.Id));    
            }
            var repoProtos = Repository.Get<ItemDefinition>().Elements.Where(i => i.IsPrototype).ToArray();
            resposne.ItemPrototypes = repoProtos.Select(s => new ItemPrototype { Name = s.Name }).ToArray();
            resposne.ItemTypes = EnumsDictionary.ItemTypes.Select(s => new ItemTypeInfo { TypeName = s.Value.ToString(), Description = s.Name }).ToArray();
            return resposne;
        }

        public IEnumerable<ItemPrototype> Get(ItemPrototypesRequest request)
        {
            return Repository.Get<ItemDefinition>().Elements.Where(i => i.IsPrototype).Select(s => DTOFactory.Create<ItemPrototype>(s));
        }

        public IEnumerable<BonusInfo> Get(BonusPrototypesRequest request)
        {
            yield return DTOFactory.GetBonusInfo(new ACBonus("", (string)EnumsDictionary.ACTypes.First().Value, 0));
            yield return DTOFactory.GetBonusInfo(new AttackBonus());
            yield return DTOFactory.GetBonusInfo(new AppendEffectBonus());
            yield return DTOFactory.GetBonusInfo(new AtutBonus());
            yield return DTOFactory.GetBonusInfo(new DamageBonus());
            yield return DTOFactory.GetBonusInfo(new HPBonus());
            yield return DTOFactory.GetBonusInfo(new InitiativeBonus());
            yield return DTOFactory.GetBonusInfo(new MainSkillBonus());
            yield return DTOFactory.GetBonusInfo(new MaxDexterityBonus());
            yield return DTOFactory.GetBonusInfo(new NumberOfAttacksBonus());
            yield return DTOFactory.GetBonusInfo(new OverallBonus());
            yield return DTOFactory.GetBonusInfo(new PanaltyBonus());
            yield return DTOFactory.GetBonusInfo(new RangeBonus());
            yield return DTOFactory.GetBonusInfo(new ResistanceBonus());
            yield return DTOFactory.GetBonusInfo(new SecondarySkillBonus());
            yield return DTOFactory.GetBonusInfo(new SpeedBonus());
            yield return DTOFactory.GetBonusInfo(new ThrowBonus());
        }

        public IEnumerable<EnumDictionaryEntry> Get(ACTypesRequest request)
        {
            return EnumsDictionary.ACTypes;
        }

        public void Post(ItemDetails item)
        {
            try
            {
                var repo = Repository.Get<ItemDefinition>();
                ItemDefinition it = null;
                if (item.Id == null)
                {
                    it = new ItemDefinition();
                    repo.Elements.Add(it);
                }
                else
                {
                    it = repo.GetElementById(item.Id.Value);
                    it.Bonuses.Clear();
                }
                it.Name = item.Name;
                it.BaseType = (BaseTypes)EnumsDictionary.ItemTypes.Single(s => s.Name == item.Type.Description).Value;
                it.Cost = item.Price;
                it.IsPrototype = item.IsPrototype;
                it.PrototypeName = item.Prototype.Name;
                it.Specials = item.OtherInfo;
                it.Bonuses = item.Bonuses.Select(b => b.Prototype).ToList();

                repo.Commit(it, true);
            }
            catch (Exception exc)
            {
                Logger.Error("Błąd podczas zapisywania przedmiotu", exc);
                throw;
            }
        }

        public void Delete(ItemDetails item)
        {
            try
            {
                var repo = Repository.Get<ItemDefinition>();
                var it = repo.SingleOrDefault(s => s.Id == item.Id);

                repo.Elements.Remove(it);
                repo.Commit(it, false);
            }
            catch (Exception exc)
            {
                Logger.Error("Błąd podczas usuwania przedmiotu", exc);
                throw;
            }
        }

        public ItemDetails Get(CopyItemRequest request)
        {
            var source = Repository.Get<ItemDefinition>().GetElementById(request.Id);
            if (source == null)
                throw new ApplicationException("Item o id: " + request.Id + " nie odnaleziony");
            var copy = DarkTemplar.DeepClone<ItemDefinition>(source, true);
            copy.Id = null;
            copy.Name = source.Name + " (kopia)";
            return DTOFactory.Create<ItemDetails>(copy);
        }
    }
}