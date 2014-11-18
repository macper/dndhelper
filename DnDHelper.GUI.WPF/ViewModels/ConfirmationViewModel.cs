using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class ConfirmationViewModel : ViewModelBase
    {
        public string ConfirmationMessage { get; set; }
        public Action ActionWhenConfirmed { get; set; }

        public ConfirmationViewModel(string confirmationMessage, Action actionWhenConfirmed)
        {
            ConfirmationMessage = confirmationMessage;
            ActionWhenConfirmed = actionWhenConfirmed;
        }
    }
}
