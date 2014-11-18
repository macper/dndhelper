using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper.Domain
{
    public static class DescriptionsDictionary 
    {
        public static string GetDurationDescription(int duration)
        {
            if (duration < 60 / Rules.TurnLengthInSeconds)
            {
                return string.Format("{0} tur", duration);
            }
            if (duration < 3600 / Rules.TurnLengthInSeconds)
            {
                var wholeMins = TurnsToMinutes(duration);
                var turns = duration - MinutesToTurns(wholeMins);
                return string.Format("{0} minut {1} tur", wholeMins, turns);
            }
            var wholeHours = TurnsToHours(duration);
            var remaining = duration - HoursToTurns(wholeHours);
            return string.Format("{0} godzin {1} minut", wholeHours, TurnsToMinutes(remaining));
        }

        public static int TurnsToMinutes(int turns)
        {
            return (int)(turns * Rules.TurnLengthInSeconds) / 60;
        }

        public static int MinutesToTurns(int minutes)
        {
            return (minutes * 60 / Rules.TurnLengthInSeconds);
        }

        public static int TurnsToHours(int turns)
        {
            return (int) (turns*Rules.TurnLengthInSeconds) / 3600;
        }

        public static int HoursToTurns(int hours)
        {
            return (hours*3600 / Rules.TurnLengthInSeconds);
        }
    }
}
