using System;
using System.Collections.Generic;

namespace DnDHelper.Domain
{
    public class GameTimer : IGameTimer
    {
        private DateTime _currentTime;
        public DateTime CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                var repo = ServiceContainer.GetInstance<RepositorySet>().Get<AppSetting>();
                var el = repo.GetElementByName( Const.AppSettings.Time );
                el.Value = value.ToString();
                repo.Commit(el, true);
            }
        }
        private static readonly ILogger Logger = ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>(Const.LoggerName, typeof(GameTimer).Name));
        private event EventHandler<TurnChangeEventArgs> TurnChange;

        public GameTimer()
        {
        }

        public GameTimer(DateTime currentTime)
        {
            _currentTime = currentTime;
        }

        public void AddDays(int amount)
        {
            AddHours(amount * 24);
        }

        public void AddHours(int amount)
        {
            AddMinutes(amount * 60);
        }

        public void AddMinutes(int amount)
        {
            AddTurns(amount * (60 / Rules.TurnLengthInSeconds));
        }

        public void AddTurns(int amount)
        {
            CurrentTime = CurrentTime.AddSeconds(Rules.TurnLengthInSeconds*amount);
            Logger.Debug("Turns added: " + amount);
            if (amount > 0)
            {
                TurnChange(this, new TurnChangeEventArgs() { TurnsCount = amount});
            }
        }

        public void SubscribeOnTurnChange(string subscriber, Action<int> action)
        {
            TurnChange += (s, e) => action(e.TurnsCount);
            Logger.Info("SubscribeOnTurnChange : " + subscriber);
        }
    }

    public class TurnChangeEventArgs : EventArgs
    {
        public int TurnsCount { get; set; }
    }
}