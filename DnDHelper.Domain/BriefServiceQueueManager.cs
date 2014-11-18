using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace DnDHelper.Domain
{
    public class BriefServiceQueueManager
    {
        private readonly int _updatesInterval;
        private readonly List<CharacterBrief> _characterList;
        private readonly List<EnemyBrief> _enemyList; 
        private readonly BackgroundWorker _worker;
        private DateTime _lastUpdated;
        private static readonly ILogger Logger = ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>(Const.LoggerName, typeof(BriefServiceQueueManager).Name));
        private IBriefService _briefService;

        public BriefServiceQueueManager(int updatesInterval, IBriefService briefService)
        {
            _characterList = new List<CharacterBrief>();
            _enemyList = new List<EnemyBrief>();
            _updatesInterval = updatesInterval;
            _worker = new BackgroundWorker();
            _briefService = briefService;
            _worker.DoWork += (o, e) =>
            {
                while (ServiceContainer.GetInstance<IAppStateController>().IsRunning)
                {
                    if (ServiceContainer.GetInstance<IAppStateController>().IsOffline)
                    {
                        Thread.Sleep(60 * 1000);
                        Logger.Info("Tryb offline - czekam...");
                        continue;
                    }
                    if (_lastUpdated.AddSeconds(_updatesInterval) < DateTime.Now)
                    {
                        try
                        {
                            if (_characterList.Any() || _enemyList.Any())
                            {
                                briefService.UpdateBriefs(new BriefUpdate
                                {
                                    Characters = _characterList,
                                    EnemyCharacters = _enemyList
                                });
                                Logger.Info(string.Format("Wysłano: {0} briefów, {1} enemy briefów", _characterList.Count, _enemyList.Count));
                                _characterList.Clear();
                                _enemyList.Clear();
                            }
                            _lastUpdated = DateTime.Now;
                        }
                        catch (Exception exception)
                        {
                            Logger.Error(exception.Message, exception);
                            Thread.Sleep(60 * 1000);
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            };
        }

        public void Run()
        {
            _worker.RunWorkerAsync();
        }

        public void AddBriefCharacter(Character character, TypeOfChange change)
        {
            _characterList.Add(CharacterBrief.Create(character, change));
        }

        public void AddEnemyBriefCharacter(BattleCharacter character, TypeOfChange change)
        {
            _enemyList.Add(EnemyBrief.Create(character, change));
        }

        public void RemoveAllEnemies()
        {
            _briefService.RemoveEnemies();
            Logger.Info("Usunięto wszystkich EnemyBriefów");
        }
       
    }
}
