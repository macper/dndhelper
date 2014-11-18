using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF
{
    public interface IAppAPI
    {
        void ChangePanel(string name);
        void Log(string message, string logger, MessageType type);
        void InitAppEngine();
        void RedirectToViewModel(ViewModels.ViewModelBase viewModel, Action onSuccess = null);
        void HandleOperationResult(OperationResult result, Action onSuccess = null);
        void GetItemFromRepo(ItemPosition position, Action<Item> onSuccess);
        void ShowItem(Item item);
        void RegisterGlobalCommand(ICommand command, string name, Func<bool> canExecuted = null);
        void ExecuteGlobalCommand(string name, object param);
        void SetGlobalVariable<T>(string name, T value);
        void ShowNotifyToUser(string message);
        T GetGlobalVariable<T>(string name) where T : class;
        T GetVariableFromUser<T>(string message);
    }

    public enum MessageType { Debug, Info, Warning, Error }
}
