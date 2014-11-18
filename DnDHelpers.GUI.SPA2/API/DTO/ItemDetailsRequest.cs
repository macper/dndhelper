using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace DnDHelpers.GUI.SPA2.API.DTO
{
    [Route("/api/item/{Id}", "GET")]
    public class ItemDetailsRequest : IReturn<ItemDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}