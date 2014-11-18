using System;
using System.Globalization;
using System.Windows.Data;

namespace DnDHelper.GUI.WPF
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString(CultureInfo.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = value as string;
            if (input == null)
                return null;
            return DateTime.Parse(input, CultureInfo.CurrentCulture);
        }
    }
}