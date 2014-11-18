using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string NotifyText { get; set; }
        public ICommand MenuItemChange { get; set; }
        public ICommand AddExperience { get; set; }

        public MainViewModel()
        {
            MenuItemChange = new Command((o) =>
                                             {
                                                 ServiceContainer.GetInstance<IAppAPI>().ChangePanel((string) o);
                                                 ServiceContainer.GetInstance<IAppAPI>().SetGlobalVariable(GlobalVariables.MainTab, (string)o);
                                             });
            ServiceContainer.GetInstance<IAppAPI>().RegisterGlobalCommand(MenuItemChange, GlobalCommands.ChangeMainTab);

            AddExperience = new Command( ( o ) =>
            {
                ServiceContainer.GetInstance<IAppAPI>().RedirectToViewModel( new AddExperienceViewModel(), 
                    () => ServiceContainer.GetInstance<IAppAPI>().ExecuteGlobalCommand( GlobalCommands.RefreshExperience, null ) );
            } );
        }

        public static class TabNames
        {
            public static readonly string Tech = "Tech";
            public static readonly string General = "General";
            public static readonly string Char = "Char";
            public static readonly string Battle = "Battle";
            public static readonly string Repos = "Repos";
        }
    }
}
