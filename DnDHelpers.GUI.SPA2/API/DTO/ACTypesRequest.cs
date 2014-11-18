using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;
using ServiceStack.ServiceHost;

namespace DnDHelpers.GUI.SPA2.API.DTO
{
    [Route("/api/ACTypes")]
    public class ACTypesRequest : IReturn<IEnumerable<EnumDictionaryEntry>>
    {
    }
}