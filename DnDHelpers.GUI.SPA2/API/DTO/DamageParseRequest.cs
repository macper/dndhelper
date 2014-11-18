using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace DnDHelpers.GUI.SPA2.API.DTO
{
    [Route("/api/dictionary/parsedDamage")]
    public class DamageParseRequest
    {
        public string Value { get; set; }
    }
}