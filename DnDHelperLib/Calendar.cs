using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper
{
    public class Calendar
    {
        public List<CalendarEntry> Entries { get; set; }

        public Calendar()
        {
            Entries = new List<CalendarEntry>();
        }

        public void AddEntry(CalendarEntry entry)
        {
            Entries.Add(entry);
            Entries.Sort(new CalendarComparer());
        }


    }

    public class CalendarEntry
    {
        public DateTime Time { get; set; }
        public string Name { get; set; }

        public CalendarEntry(DateTime time, string name)
        {
            Time = time;
            Name = name;
        }

        public CalendarEntry()
        {
        }
    }

    public class CalendarComparer : IComparer<CalendarEntry>
    {

        #region IComparer<CalendarEntry> Members

        public int Compare(CalendarEntry x, CalendarEntry y)
        {
            return x.Time.CompareTo(y.Time);
        }

        #endregion
    }
}
