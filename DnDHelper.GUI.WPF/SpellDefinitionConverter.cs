using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF
{
    public class SpellDefinitionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SpellShool)
                return SpellDefinition.ConvertToString((SpellShool)value);
            else if (value is SpellRange)
                return SpellDefinition.ConvertToString((SpellRange) value);
            throw new NotImplementedException("Nieznany typ");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
