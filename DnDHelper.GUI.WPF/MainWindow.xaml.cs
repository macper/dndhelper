using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DnDHelper.Domain;
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, UserControl> _panels;
        private Lazy<IAppAPI> _appApi = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);
        private static readonly ILogger Logger = ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>(Const.LoggerName, typeof(MainWindow).Name));

        public MainWindow()
        {
            InitializeComponent();
            _panels = new Dictionary<string, UserControl> 
            { 
                { PanelNames.Tech, new Panels.TechPanel() },
                { PanelNames.General, new Panels.GeneralPanel() },
                { PanelNames.Groups, new Panels.GroupsPanel() },
                { PanelNames.Repos, new Panels.ReposPanel()},
                { PanelNames.Battle, new Panels.Battle()}
            };
            try
            {
                ServiceContainer.GetInstance<IAppAPI>().InitAppEngine();
            }
            catch (Exception exception)
            {
                ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>(Domain.Const.LoggerName, "Application")).Error(exception.Message, exception);
            }

            DataContext = new MainViewModel();
            _panels[PanelNames.General].DataContext = new GeneralViewModel();
            _panels[PanelNames.Groups].DataContext = new GroupsViewModel();
            _panels[PanelNames.Battle].DataContext = new BattleViewModel();
            _panels[PanelNames.Tech].DataContext = new TechViewModel();

            // SuspiciousEntitiesKillThemAll();
        }

        public T GetPanel<T>() where T:UserControl
        {
            var type = typeof (T);
            if (!_panels.Values.Any(t => t.GetType() == type))
            {
                MessageBox.Show("Nie znaleziono panelu " + type, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return (T) _panels.Values.Single(s => s.GetType() == type);
        }

        public void ChangePanel(string name)
        {
            if (!_panels.ContainsKey(name))
            {
                MessageBox.Show("Nie znaleziono panelu " + name, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            PanelHolder.Children.Clear();
            PanelHolder.Children.Add(_panels[name]);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                    {
                        _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeCharacterTab, CharacterViewModel.TabNames.Stats);
                        _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeRepoTab, ReposViewModel.TabNames.Items);
                    }
                    break;

                case Key.D2:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                    {
                        _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeCharacterTab, CharacterViewModel.TabNames.Items);
                        _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeRepoTab, ReposViewModel.TabNames.Spells);
                    }
                    break;

                case Key.D3:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                    {
                        _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeCharacterTab, CharacterViewModel.TabNames.Spells);
                        _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeRepoTab, ReposViewModel.TabNames.Effects);
                    }
                    break;

                case Key.D4:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                    {
                        _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeCharacterTab, CharacterViewModel.TabNames.Effects);
                        _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeRepoTab, ReposViewModel.TabNames.Skills);
                    }
                    break;

                case Key.D5:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                    {
                        _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeCharacterTab, CharacterViewModel.TabNames.Skills);
                        _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeRepoTab, ReposViewModel.TabNames.Atutes);
                    }
                    break;

                case Key.F1:
                    _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeMainTab, "Tech");
                    break;

                case Key.F2:
                    _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeMainTab, "General");
                    break;

                case Key.F3:
                    _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeMainTab, "Char");
                    break;

                case Key.F4:
                    _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeMainTab, "Battle");
                    break;

                case Key.F5:
                    _appApi.Value.ExecuteGlobalCommand(GlobalCommands.ChangeMainTab, "Repos");
                    break;

                case Key.N:
                    _appApi.Value.ExecuteGlobalCommand(GlobalCommands.NextBattleMember, null);
                    break;
            }
        }

        private void SuspiciousEntitiesKillThemAll()
        {
            var repo = ServiceContainer.GetInstance<RepositorySet>();
            
            foreach (var r in repo.GetAll())
            {
                var items = r.GetElements();
                foreach (var item in items)
                {
                    var anotherItemsWithTheSameID = items.Where(e => e.Id == item.Id);
                    if (anotherItemsWithTheSameID.Count() > 1)
                    {
                        Logger.Error(string.Format("Items with the same ID: {0} : {1}", item.Id, string.Join(",", anotherItemsWithTheSameID.Select(s => s.Name).ToArray())), null);
                        foreach (var toChange in anotherItemsWithTheSameID.Where(e => e.Name != item.Name))
                        {
                            toChange.Id = Guid.NewGuid();
                            r.Commit(toChange, true);
                        }
                    }

                    var anotherItemsWithTheSameNAme = items.Where(e => e.Name == item.Name);
                    if (anotherItemsWithTheSameNAme.Count() > 1)
                    {
                        Logger.Error(string.Format("Items with the same name: {0} : {1}", item.Name, string.Join(",", anotherItemsWithTheSameID.Select(s => s.Name).ToArray())), null);
                    }
                        
                }
            }
            
        }

        //private void CopyCharacterGroups()
        //{
        //    var repo = ServiceContainer.GetInstance<RepositorySet>();
        //    var chG = repo.Get<CharacterGroup>();
        //    var ch = repo.Get<Character>();
            
        //    ch.Elements.Clear();

        //    foreach (var g in chG.Elements)
        //    {
        //        foreach (var c in g.Members)
        //        {
        //            c.GroupName = g.Name;
        //            ch.Elements.Add(c);
        //        }
                
        //    }

        //    ch.Commit(null, false);
        //}
    }

    public static class PanelNames
    {
        public static readonly string Tech = "Tech";
        public static readonly string General = "General";
        public static readonly string Groups = "Char";
        public static readonly string Battle = "Battle";
        public static readonly string Repos = "Repos";
    }
}
