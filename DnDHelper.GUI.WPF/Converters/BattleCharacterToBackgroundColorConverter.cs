using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF
{
    class BattleCharacterToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var character = value as BattleCharacter;
            if (character == null)
                return null;
            if (character.IsActive)
            {
                return !character.Live ? Color.Red.Name : Color.Yellow.Name;
            }

            if (!character.Live)
                return Color.LightSalmon.Name;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
