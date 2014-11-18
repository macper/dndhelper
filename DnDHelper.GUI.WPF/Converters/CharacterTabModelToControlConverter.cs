using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using DnDHelper.GUI.WPF.Panels;
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF
{
    public class CharacterTabModelToControlConverter : IValueConverter
    {
        private static readonly Dictionary<Type, Type> _modelToControlMappings;

        static CharacterTabModelToControlConverter()
        {
            _modelToControlMappings = new Dictionary<Type, Type>
                                          {
                                              {typeof (CharacterTabStatsModel), typeof (CharacterTabStats)},
                                              {typeof (CharacterTabItemsModel), typeof (CharacterTabItems)},
                                              {typeof (CharacterTabSkillsModel), typeof (CharacterTabSkills)},
                                              {typeof (CharacterTabSpellsModel), typeof (CharacterTabSpells)},
                                              {typeof (CharacterTabEffectsModel), typeof (CharacterTabEffects)},
                                              {typeof (CharacterTabOthersModel), typeof(CharacterTabOthers)},
                                              {typeof(CharacterTabBonusesModel), typeof(CharacterTabBonuses)}
                                          };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var model = value as CharacterTabBaseModel;
            if (model == null)
                return null;

            if (!_modelToControlMappings.ContainsKey(model.GetType()))
            {
               throw new KeyNotFoundException("Nie zarejestrowano typu:" + model.GetType());
            }
            var control = (UserControl)Activator.CreateInstance(_modelToControlMappings[model.GetType()]);
            control.DataContext = model;
            return control;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
