using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class BonusPreviewViewModel : ViewModelBase
    {
        public IEnumerable<BaseBonus> Bonuses { get; set; }
        public bool NoBonuses { get { return !Bonuses.Any(); } }

        public BonusPreviewViewModel(IEnumerable<BaseBonus> bonuses)
        {
            Bonuses = bonuses;
        }
    }
}
