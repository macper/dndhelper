using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace DnDHelpers.GUI.SPA2.API.DTO
{
    [Route("/api/items/{Name}/{Type}", "GET")]
    [Route("/api/items", "GET")]
    public class ItemsRequest : IReturn<IEnumerable<ItemInfo>>
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}