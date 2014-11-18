using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace DnDHelpers.GUI.SPA2.API.DTO
{
    public class UpdateInfoResult
    {
        public bool NeedsUpdate { get; set; }

        public bool UnSendedChanges { get; set; }
    }
}