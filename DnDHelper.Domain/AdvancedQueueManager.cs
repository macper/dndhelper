using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace DnDHelper.Domain
{
    class AdvancedQueueManager : IQueueManager
    {
        private readonly Dictionary<string, Action> _updateQueue = new Dictionary<string, Action>();
        private readonly int _updatesInterval;
        private BackgroundWorker _worker;
        private DateTime _lastUpdated;
        private static ILogger Logger = ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>(Const.LoggerName, typeof(AdvancedQueueManager).Name));

        public AdvancedQueueManager(int updatesInterval)
        {
            _updatesInterval = updatesInterval;
            SetupWorker();
        }

        public AdvancedQueueManager()
        {
            int interval;
            if (ConfigurationManager.AppSettings[Const.ConfigurationSettings.UpdatesInterval] == null || !int.TryParse(ConfigurationManager.AppSettings[Const.ConfigurationSettings.UpdatesInterval], out interval))
            {
                interval = 20;
            }
            _updatesInterval = interval;
            SetupWorker();
        }

        private void SetupWorker()
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += (o, e) =>
            {
                while (ServiceContainer.GetInstance<IAppStateController>().IsRunning)
                {
                    if (_lastUpdated.AddSeconds(_updatesInterval) < DateTime.Now)
                    {
                        try
                        {
                            foreach (var value in _updateQueue)
                            {
                                Logger.Info("Zapisano repozytorium: " + value.Key);
                                value.Value();
                            }
                            _updateQueue.Clear();

                            ChangeTracker.Instance.SaveChanges();

                            _lastUpdated = DateTime.Now;
                        }
                        catch (Exception exception)
                        {
                            Logger.Error(exception.Message, exception);
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                Logger.Info("Kończę pracę");
            };
            _worker.RunWorkerAsync();
        }

        public void RepositorySaveRequest<T>(Repository<T> repository) where T : BaseEntity
        {
            if (_updateQueue.ContainsKey(repository.Name))
            {
                return;
            }
            _updateQueue.Add(repository.Name, () => ServiceContainer.GetInstance<IRepositoryStorage>().SaveRepository(repository));
            Logger.Debug("Zakolejkowano do zapisania: " + repository.Name);
        }
    }
}