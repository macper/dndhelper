using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using DnDHelper.BriefService;
using DnDHelper.Domain;
using log4net;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;

namespace DnDHelpers.GUI.SPA2.API
{
    public class BriefsService : Service
    {
        private string _briefServiceUrl;
        private static ILog Logger = LogManager.GetLogger(typeof (BriefsService));

        public BriefsService()
        {
            _briefServiceUrl = ConfigurationManager.AppSettings["BriefServiceUrl"];
        }

        public BriefInfoResponse Post(BriefInfoRequest request)
        {
            Logger.Debug("BriefInfoRequest:");
            Logger.Debug(JsonSerializer.SerializeToString(request));
            var resp = new JsonServiceClient(_briefServiceUrl).Post<BriefInfoResponse>(request);
            Logger.Debug("Response:");
            Logger.Debug(JsonSerializer.SerializeToString(resp));
            return resp;
        }
    }
}