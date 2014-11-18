using System;

namespace DnDHelper.Domain
{
    public interface IGameTimer
    {
        DateTime CurrentTime { get; set; }
        void AddDays(int amount);
        void AddHours(int amount);
        void AddMinutes(int amount);
        void AddTurns(int amount);
        void SubscribeOnTurnChange(string subscriber, Action<int> action);
    }
}