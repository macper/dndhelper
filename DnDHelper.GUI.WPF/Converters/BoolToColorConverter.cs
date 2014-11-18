using System;
using System.Globalization;
using System.Windows.Data;
using System.Drawing;

namespace DnDHelper.GUI.WPF
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Color.DarkGreen.Name : Color.Red.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}