using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class CopyEffectViewModel : ViewModelBase
    {
        public Effect EffectToCopy { get; set; }

        public BattleCharacter Source { get; set; }

        public ObservableCollection<BattleCharacter> Targets { get; set; }

        public BattleCharacter SelectedTarget
        {
            get { return _selectedTarget; }
            set { _selectedTarget = value; PropertyHasChanged("SelectedTarget"); }
        }

        private BattleCharacter _selectedTarget;

        public ICommand Confirm { get; set; }

        public CopyEffectViewModel(Effect effectToCopy, BattleCharacter source, ObservableCollection<BattleCharacter> targets)
        {
            EffectToCopy = effectToCopy;
            Source = source;
            Targets = targets;
        
            Confirm = new Command((o) =>
            {
                SelectedTarget.Character.Controller.AddEffect(DarkTemplar.DeepClone(EffectToCopy, true));
                ServiceContainer.GetInstance<AppFacade>().CharacterChange(SelectedTarget.Character);
                CommandHasExecuted("Confirm", OperationResult.Success());
            });
            
        }

    }
}
