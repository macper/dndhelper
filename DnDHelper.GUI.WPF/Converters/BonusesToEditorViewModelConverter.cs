using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using DnDHelper.Domain.Bonuses;
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF
{
    public class BonusesToEditorViewModelConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BonusEditorViewModel((List<BaseBonus>)value, (string) parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
