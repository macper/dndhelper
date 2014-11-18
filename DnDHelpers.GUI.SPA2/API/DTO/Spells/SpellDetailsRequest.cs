using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelpers.GUI.SPA2.API.DTO.Spells
{
    [Route( "/api/spell/{Id}", "GET" )]
    public class SpellDetailsRequest
    {
        public Guid Id { get; set; }
    }
}