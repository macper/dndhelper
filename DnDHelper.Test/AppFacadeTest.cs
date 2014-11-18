using System;
using System.Linq;
using DnDHelper.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DnDHelper.Test
{
    [TestClass]
    public class AppFacadeTest
    {
        private AppFacade _appFacade;

        [TestInitialize]
        public void Init()
        {
            var logger = new Mock<ILogger>();
            ServiceContainer.Kernel.Bind<ILogger>().ToConstant(logger.Object);
            ServiceContainer.Kernel.Rebind<IPythonEngine>().ToConstant(new PythonEngine());
            var repo = new SampleDataRepository();
            _appFacade = new AppFacade(repo);
            repo.Init();
            //repo.RegisterCharacters();
        }

        [TestMethod]
        public void SimpleTest()
        {
            var storageMock = new Mock<IQueueManager>();
            
            storageMock.Setup((s) => s.RepositorySaveRequest(It.IsAny<Repository<AppSetting>>()));
            ServiceContainer.Kernel.Rebind<IQueueManager>().ToConstant(storageMock.Object);
            var turnsPassed = 0;
            _appFacade.GameTimer.SubscribeOnTurnChange("Tester", (c) => turnsPassed = c);
            Assert.IsTrue(_appFacade.GameTimer.CurrentTime == new DateTime(1300,1,1));
            _appFacade.GameTimer.AddMinutes(1);
            storageMock.Verify((s) => s.RepositorySaveRequest(It.IsAny<Repository<AppSetting>>()), Times.Exactly(1));
            Assert.IsTrue(turnsPassed == 10);

            _appFacade.AddGroup(new Domain.CharacterGroup() {Name = "Nowa grupa"});
            Assert.IsTrue(_appFacade.RepoGroups.Count() == 2);
            
        }
    }
}