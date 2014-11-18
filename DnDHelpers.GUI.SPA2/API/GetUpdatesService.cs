using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;
using DnDHelpers.GUI.SPA2.API.DTO;
using ServiceStack.ServiceInterface;

namespace DnDHelpers.GUI.SPA2.API
{
    public class GetUpdatesService : Service
    {
        public IUpdateClient UpdateService { get; set; }

        public bool Get(GetUpdatesRequest request)
        {
            throw new NotImplementedException();
            //return UpdateService.DownloadUpdates().Result == OperationResultType.Success;    
        }
    }
}