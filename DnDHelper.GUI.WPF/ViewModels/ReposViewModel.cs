using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class ReposViewModel : ViewModelBase
    {
        public ICommand ChangePanel { get; private set; }
        private Lazy<Dictionary<string, ViewModelBase>> _mappings = new Lazy<Dictionary<string, ViewModelBase>>(() => 
            new Dictionary<string, ViewModelBase>
                            {
                                { TabNames.Items, new RepoItemsViewModel() },
                                { TabNames.Spells, new RepoSpellsViewModel() },
                                { TabNames.Effects, new RepoEffectsViewModel()},
                                { TabNames.Skills, new RepoSkillsViewModel()},
                                { TabNames.Atutes, new RepoAtutesViewModel()},
                                { TabNames.Races, new RepoRacesViewModel()},
                                { TabNames.Classes, new RepoClassesViewModel()}
                            });

        public ReposViewModel()
        {
            var appAPI = ServiceContainer.GetInstance<IAppAPI>();

            ChangePanel = new Command((o) =>
                                          {
                                              var key = o as string;
                                              if (key == null)
                                              {
                                                  appAPI.HandleOperationResult(OperationResult.Error("Nieprawidłowy parametr"));
                                                  return;
                                              }
                                              if (!_mappings.Value.ContainsKey(key))
                                              {
                                                  appAPI.HandleOperationResult(OperationResult.Error("Brak takiego panelu: " + key));
                                                  return;
                                              }
                                              appAPI.RedirectToViewModel(_mappings.Value[key]);
                                          });

            appAPI.RegisterGlobalCommand(ChangePanel, GlobalCommands.ChangeRepoTab, () => appAPI.GetGlobalVariable<string>(GlobalVariables.MainTab) == MainViewModel.TabNames.Repos);
        }

        public static class TabNames
        {
            public static readonly string Items = "Items";
            public static readonly string Spells = "Spells";
            public static readonly string Effects = "Effects";
            public static readonly string Skills = "Skills";
            public static readonly string Atutes = "Atutes";
            public static readonly string Races = "Races";
            public static readonly string Classes = "Classes";
        }
    }
}
