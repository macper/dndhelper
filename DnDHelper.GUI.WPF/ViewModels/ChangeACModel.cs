using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class ChangeACModel : ViewModelBase
    {
        private readonly Character _host;

        public int TotalAC { get { return _host.CurrentStats.AC.Total; } }

        public int BaseNaturalArmor
        {
            get { return _host.OriginalStats.AC.GetForType(ACBonusTypes.NaturalArmor); }
            set
            {
                _host.Controller.ChangeACOriginal(value - BaseNaturalArmor, ACBonusTypes.NaturalArmor);
                PropertyHasChanged("OriginalNaturalArmor");
                PropertyHasChanged("CurrentNaturalArmor");
                PropertyHasChanged("TotalAC");
            }
        }

        public int BaseArmor
        {
            get { return _host.OriginalStats.AC.GetForType(ACBonusTypes.Armor); }
            set
            {
                _host.Controller.ChangeACOriginal(value - BaseArmor, ACBonusTypes.Armor);
                PropertyHasChanged("OriginalArmor");
                PropertyHasChanged("CurrentArmor");
                PropertyHasChanged("TotalAC");
            }
        }

        public int BaseMagicShield
        {
            get { return _host.OriginalStats.AC.GetForType(ACBonusTypes.MagicShield); }
            set
            {
                _host.Controller.ChangeACOriginal(value - BaseMagicShield, ACBonusTypes.MagicShield);
                PropertyHasChanged("OriginalMagicShield");
                PropertyHasChanged("CurrentMagicShield");
                PropertyHasChanged("TotalAC");
            }
        }

        public int CurrentNaturalArmor { get { return _host.CurrentStats.AC.GetForType(ACBonusTypes.NaturalArmor); }}
        public int CurrentArmor { get { return _host.CurrentStats.AC.GetForType(ACBonusTypes.Armor); } }
        public int CurrentMagicShield { get { return _host.CurrentStats.AC.GetForType(ACBonusTypes.MagicShield); } }
        public int CurrentDexterity { get { return _host.CurrentStats.AC.GetForType(ACBonusTypes.Dexterity); } }

        public ICommand Commit { get; set; }

        public ChangeACModel(Character host)
        {
            _host = host;
            Commit = new Command((o) => CommandHasExecuted("Commit", OperationResult.Success()));
        }
    }
}
