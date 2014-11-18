using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain.Bonuses;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class BonusEditorViewModel : ViewModelBase
    {
        public ObservableCollection<BaseBonus> Bonuses { get; set; }
        private List<BaseBonus> _originalBonuses;
        private string _bonusSource;
        public string BonusSource
        {
            get { return _bonusSource; }
            set { _bonusSource = value; }
        }
        private BaseBonus _selectedBonus;
        public BaseBonus SelectedBonus 
        { 
            get { return _selectedBonus; }
            set { _selectedBonus = value; PropertyHasChanged("SelectedBonus"); }
        }
        public ICommand AddBonus { get; set; }
        public ICommand EditBonus { get; set; }
        public ICommand RemoveBonus { get; set; }

        public event EventHandler<ChangeBonusEventArgs> ChangeOccured;

        private void OnChangeOccured(bool edit)
        {
            if (ChangeOccured != null)
                ChangeOccured(this, new ChangeBonusEventArgs() { EditMode = edit});

            if (edit)
            {
                var notAdded = Bonuses.Where(b => !Bonuses.Contains(b));
                Bonuses = new ObservableCollection<BaseBonus>(Bonuses);
                foreach (var baseBonuse in notAdded)
                {
                    Bonuses.Add(baseBonuse);
                }
            }
            _originalBonuses.Clear();
            foreach (var baseBonuse in Bonuses)
            {
                _originalBonuses.Add(baseBonuse);
                baseBonuse.Source = _bonusSource;
            }
            PropertyHasChanged("Bonuses");
        }

        public BonusEditorViewModel(List<BaseBonus> bonuses, string bonusSource)
        {
            _originalBonuses = bonuses;
            _bonusSource = bonusSource;
            Bonuses = new ObservableCollection<BaseBonus>(bonuses);
            AddBonus = new Command((o) =>
                                       {
                                           var model = new AddBonusViewModel();
                                           ServiceContainer.GetInstance<IAppAPI>().RedirectToViewModel(model, () =>
                                                                                                                  {
                                                                                                                      Bonuses.Add(model.Bonus); OnChangeOccured(false);
                                                                                                                  });
                                       });
            EditBonus = new Command((o) => ServiceContainer.GetInstance<IAppAPI>().RedirectToViewModel(new AddBonusViewModel(SelectedBonus), () => OnChangeOccured(true)), this, () => SelectedBonus != null, "SelectedBonus");
            RemoveBonus = new Command((o) => { Bonuses.Remove(SelectedBonus); OnChangeOccured(false); }, this, () => SelectedBonus != null, "SelectedBonus");
        }
    }

    public class ChangeBonusEventArgs : EventArgs
    {
        public bool EditMode { get; set; }
    }
}
