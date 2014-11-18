using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class Command : ICommand
    {
        private readonly Action<object> _actionToExecute;
        private bool _canBeExecuted;

        public Command(Action<object> actionToExecute, bool canBeExecuted = true)
        {
            _actionToExecute = actionToExecute;
            _canBeExecuted = canBeExecuted;
        }

        public Command(Action<object> actionToExecute, ViewModelBase viewModel, Func<bool> canBeExecuted, string propertyWatched)
        {
            _actionToExecute = actionToExecute;
            viewModel.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == propertyWatched)
                {
                    _canBeExecuted = canBeExecuted();
                    Dispatcher.CurrentDispatcher.BeginInvoke(new Action(OnCanExecuteChange));
                }
            };
        }

        private void OnCanExecuteChange()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, null);
        }

        public void Execute(object parameter)
        {
            _actionToExecute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canBeExecuted;
        }

        public event EventHandler CanExecuteChanged;
    }
}