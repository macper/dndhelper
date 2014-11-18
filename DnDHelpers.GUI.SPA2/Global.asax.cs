using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DnDHelper.Domain;
using DnDHelper.Infrastructure;
using DnDHelper.UpdateServiceAPI;
using DnDHelpers.GUI.SPA2.API;
using DnDHelpers.GUI.SPA2.Auth;
using log4net;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;


namespace DnDHelpers.GUI.SPA2
{
    public class Global : System.Web.HttpApplication
    {
        private static ILog appLogger = log4net.LogManager.GetLogger("Application");
        public class DnDServiceHost : AppHostBase
        {
            public DnDServiceHost() : base("DnDService", typeof(UpdateInfoService).Assembly)
            {
                CatchAllHandlers.Add((httpMethod, pathInfo, filePath) =>
                {
                    if (pathInfo.EndsWith("js", StringComparison.InvariantCultureIgnoreCase) 
                        || pathInfo.EndsWith("css", StringComparison.InvariantCultureIgnoreCase) 
                        || pathInfo.EndsWith("html", StringComparison.InvariantCultureIgnoreCase)
                        || pathInfo.Contains("glyphicons-halflings-regular")
                        || pathInfo.EndsWith("metadata"))
                        return null;

                    if (pathInfo != "/index.html" && pathInfo != "/")
                    {
                        return new ServiceStack.WebHost.Endpoints.Support.RedirectHttpHandler() { AbsoluteUrl = "/" };
                    }
                    return null;
                });
            }


            public override void Configure(Funq.Container container)
            {
                container.Register<IUpdateClient>((c) => ServiceContainer.GetInstance<IUpdateClient>());
                container.Register<RepositorySet>(ServiceContainer.GetInstance<RepositorySet>());

                Plugins.Add(new AuthFeature(() => new AuthUserSession(), new IAuthProvider[] { new BasicAuthProvider() }));

                container.Register<ICacheClient>(new MemoryCacheClient());

                var userRepo = new MacAuthProvider();
                container.Register<IUserAuthRepository>(userRepo);
                ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.JsonDateHandler.TimestampOffset;
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            try
            {
                RegisterServices();
                new DnDServiceHost().Init();
                appLogger.Info("Inicjalizacja ServiceStack - zakończona");
            }
            catch (Exception exc)
            {
                appLogger.Error("Błąd podczas inicjalizacji", exc);
            }
        }

        private static void RegisterServices()
        {
            ServiceContainer.Kernel.Bind<IRepositoryStorage>().To(typeof(XmlRepositoryStorage));
            ServiceContainer.Kernel.Bind<IRepositorySerializer>().To(typeof(XmlRepositorySerializer));
            ServiceContainer.Kernel.Bind<IEntitySerializer>().To(typeof(XmlEntitySerializer));
            ServiceContainer.Kernel.Bind<IUpdateService>().To(typeof(UpdateServiceProxy));
            ServiceContainer.Kernel.Bind<IUpdateClient>().To(typeof(UpdateClient));
            ServiceContainer.Kernel.Bind<IQueueManager>().To(typeof(SimpleQueueManager));
            
            ServiceContainer.Kernel.Bind<ILogger>().To(typeof(Log4NetLogger));
            ServiceContainer.Kernel.Bind<IGenericFilePathProvider>().To(typeof(WebPathProvider));
            var python = new PythonEngine();
            ServiceContainer.Kernel.Bind<IPythonEngine>().ToConstant(python);
            var repo = new RepositorySet();
            repo.Register<RaceDefinition>();
            repo.Register<ClassDefinition>();
            repo.Register<EffectDefinition>();
            repo.Register<SpellDefinition>();
            repo.Register<SkillDefinition>();
            repo.Register<AtutDefinition>();
            repo.Register<ItemDefinition>();
            ServiceContainer.Kernel.Bind<RepositorySet>().ToConstant(repo);

        }
    }
}