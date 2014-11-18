using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DnDHelper.Domain;
using DnDHelper.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DnDHelper.Test
{
    public abstract class BaseTest
    {
        protected static bool isInitialized;

        protected BaseTest()
        {
            Init();
        }

        public static void Init()
        {
            var logger = new Mock<ILogger>();
            ServiceContainer.Kernel.Rebind<ILogger>().ToConstant(logger.Object);
            var python = new PythonEngine();
            ServiceContainer.Kernel.Rebind<IRepositoryStorage>().To(typeof(XmlRepositoryStorage));
            ServiceContainer.Kernel.Rebind<IGenericFilePathProvider>().To(typeof (SimplePathProvider));
            ServiceContainer.Kernel.Rebind<IPythonEngine>().ToConstant(python);
            var repo = new SampleDataRepository();
            repo.Init();
            ServiceContainer.Kernel.Rebind<RepositorySet>().ToConstant(repo);
            ServiceContainer.Kernel.Rebind<IGameTimer>().ToConstant(new GameTimer(new DateTime(1300, 1, 1)));
            ServiceContainer.Kernel.Rebind<ICharacterCalculator>().ToConstant(new CharacterCalculator(repo));
            ServiceContainer.Kernel.Rebind<IQueueManager>().ToConstant(new SimpleQueueManager());
            //repo.RegisterCharacters();
        }
    }
}
