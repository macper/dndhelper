using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace DnDHelpers.GUI.SPA2.API.DTO.Spells
{
    [Route("/api/spells/{Name}/{Type}/{Level}", "GET")]
    [Route("/api/spells", "GET")]
    public class SpellsInfoRequest
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Level { get; set; }
    }
}