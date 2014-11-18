using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class AddAttackViewModel : ViewModelBase
    {
        public CustomAttack Attack { get; private set; }
        private readonly Character _owner;
        private bool _editMode;

        public string Name
        {
            get { return Attack.Name; }
            set 
            { 
                Attack.Name = value;
                PropertyHasChanged("Name");
            }
        }

        public BonusEditorViewModel BonusModel { get; private set; }
        public ICommand Commit { get; private set; }

        public AddAttackViewModel(Character owner, CustomAttack attack, bool editMode)
        {
            Attack = attack;
            _editMode = editMode;
            BonusModel = new BonusEditorViewModel(attack.Bonuses, "Atak");
            _owner = owner;
            Commit = new Command((o) =>
                                     {
                                         if (!_editMode)
                                         {
                                             var result = _owner.Controller.AddCustomAttack(attack);
                                             if (result.Result == OperationResultType.Error)
                                             {
                                                 ServiceContainer.GetInstance<IAppAPI>().HandleOperationResult(result);
                                                 return;
                                             }
                                         }
                                         else
                                         {
                                             _owner.Controller.Recalculate();
                                         }
                                         CommandHasExecuted("Commit", OperationResult.Success());
                                     });
        }
    }
}
