using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DnDHelper.Domain;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class CharacterTabBonusesModel : CharacterTabBaseModel
    {
        public ObservableCollection<BaseBonus> OriginalBonuses { get { return new ObservableCollection<BaseBonus>(Character.InitialBonuses.OrderBy(b => b.Name)); } }
        public ObservableCollection<BaseBonus> CurrentBonuses { get { return new ObservableCollection<BaseBonus>(Character.Bonuses.OrderBy(b => b.Name)); } }

        public CharacterTabBonusesModel(Character character) : base(character)
        {
        }
    }
}
