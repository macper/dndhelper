using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF
{
    public class EffectsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var effects = value as IEnumerable<Effect>;
            if (effects == null || !effects.Any())
            {
                return "";
            }
            return string.Join(",", effects.Select(e => string.Format("{0}({1})", e.InstanceName, e.Duration)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
