using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DnDHelper.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DnDHelper.GUI.WPF.ViewModels;
using Moq;

namespace DnDHelper.GUI.WPF.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class GeneralTabTest
    {
        protected AppFacade _appFacade;
        protected Mock<RepositorySet> _repoMock;
        protected Mock<IAppAPI> _apiMock;
        protected Mock<IQueueManager> _queueMock;

        public GeneralTabTest()
        {
            _repoMock = new Mock<RepositorySet>();
            _apiMock = new Mock<IAppAPI>();

            ServiceContainer.Kernel.Bind<ILogger>().ToConstant(new Mock<ILogger>().Object);
            ServiceContainer.Kernel.Bind<IRepositoryStorage>().ToConstant(new Mock<IRepositoryStorage>().Object);
            _repoMock.Setup(m => m.Get<AppSetting>()).Returns(() => new Repository<AppSetting>() { Elements = new List<AppSetting>(new [] { new AppSetting() { Name = Const.AppSettings.Time, Value = new DateTime(1000,1,1).ToString() }})});
            _repoMock.Setup(m => m.Get<CharacterGroup>()).Returns(() => new Repository<CharacterGroup>());
            _appFacade = new AppFacade(_repoMock.Object);
            ServiceContainer.Kernel.Bind<IAppAPI>().ToConstant(_apiMock.Object);
            _queueMock = new Mock<IQueueManager>();
            ServiceContainer.Kernel.Rebind<IQueueManager>().ToConstant(_queueMock.Object);
        }

        [TestMethod]
        public void TimeTest()
        {
            var model = new GeneralViewModel();
            var currentTime = _appFacade.GameTimer.CurrentTime;
            model.AddDay.Execute(null);
            Assert.IsTrue(DateTime.Parse(model.CurrentTime) == currentTime.AddDays(1));
            model.AddHour.Execute(null);
            Assert.IsTrue(DateTime.Parse(model.CurrentTime) == currentTime.AddDays(1).AddHours(1));
            model.AddMinute.Execute(null);
            Assert.IsTrue(DateTime.Parse(model.CurrentTime) == currentTime.AddDays(1).AddHours(1).AddMinutes(1));
            
        }
    }
}
