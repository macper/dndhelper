using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<CommandExecutedEventArgs> CommandExecuted;

        protected void PropertyHasChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        protected void CommandHasExecuted(string name, OperationResult result)
        {
            if (CommandExecuted != null)
            {
                CommandExecuted(this, new CommandExecutedEventArgs(name, result));
            }
        }
    }

    public class CommandExecutedEventArgs : EventArgs
    {
        public string CommandName { get; private set; }
        public OperationResult Result { get; private set; }

        public CommandExecutedEventArgs(string commandName, OperationResult result)
        {
            CommandName = commandName;
            Result = result;
        }
    }
}
