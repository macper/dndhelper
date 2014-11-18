using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain.Bonuses;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace DnDHelpers.GUI.SPA2.API.DTO
{
    [Route("/api/item", "POST")]
    [Route("/api/item", "DELETE")]
    [Authenticate]
    [RequiredRole("Admin")]
    public class ItemDetails
    {
        public string Name { get; set; }
        public Guid? Id { get; set; }
        public ItemTypeInfo Type { get; set; }

        public ItemPrototype Prototype { get; set; }
        public bool IsPrototype { get; set; }

        public int Price { get; set; }

        public string OtherInfo { get; set; }

        public IEnumerable<BonusInfo> Bonuses { get; set; }
    }

    public class ItemDetailsResponse
    {
        public ItemDetails Item { get; set; }

        public IEnumerable<ItemTypeInfo> ItemTypes { get; set; }

        public IEnumerable<ItemPrototype> ItemPrototypes { get; set; }
    }
}