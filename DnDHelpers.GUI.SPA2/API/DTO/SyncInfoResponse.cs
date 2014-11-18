using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelpers.GUI.SPA2.API.DTO
{
    public class SyncInfoResponse
    {
        public string LastSyncTime { get; set; }

        public IEnumerable<RepositoryInfo> Changes { get; set; }
    }
}