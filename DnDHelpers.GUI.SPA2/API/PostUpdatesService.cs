using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;
using DnDHelpers.GUI.SPA2.API.DTO;
using ServiceStack.ServiceInterface;

namespace DnDHelpers.GUI.SPA2.API
{
    public class PostUpdatesService : Service
    {
        public IUpdateClient UpdateService { get; set; }

        public SyncUpdatesResponse Post(SyncUpdatesRequest request)
        {
            var res = UpdateService.Synchronize();

            return new SyncUpdatesResponse
            {
                Success = res.Result == OperationResultType.Success,
                Message = res.Message
            };
        }
    }
}