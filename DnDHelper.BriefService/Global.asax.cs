using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ServiceStack.WebHost.Endpoints;

namespace DnDHelper.BriefService
{
    public class Global : System.Web.HttpApplication
    {
        public class BriefService : AppHostBase
        {
            public BriefService()
                : base("BriefService", typeof(BriefService).Assembly)
            {
               
            }

            public override void Configure(Funq.Container container)
            {
                ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.JsonDateHandler.TimestampOffset;
                log4net.Config.XmlConfigurator.Configure();
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            new BriefService().Init();
        }

    }
}