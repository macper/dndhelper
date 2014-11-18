using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using DnDHelper.Domain;
using DnDHelper.GUI.WPF.Panels;
using DnDHelper.GUI.WPF.PopUps;
using DnDHelper.GUI.WPF.ViewModels;
using DnDHelper.Infrastructure;
using DnDHelper.UpdateServiceAPI;

namespace DnDHelper.GUI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IAppAPI
    {
        private AppFacade _appEngine;
        private Dictionary<Type, ViewModelHandler> _viewModelHandlers;
        private Dictionary<string, KeyValuePair<ICommand, Func<bool>>> _commands;
        private Dictionary<string, object> _globalVariables;

        private MainWindow GetMainWindow()
        {
            return (WPF.MainWindow)MainWindow;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ServiceContainer.Kernel.Bind<IAppAPI>().ToConstant(this);
            log4net.Config.XmlConfigurator.Configure();
            ServiceContainer.Kernel.Bind<ILogger>().To(typeof(CompositeLogger));
            ServiceContainer.Kernel.Bind<IRepositorySerializer>().To(typeof(XmlRepositorySerializer));
            ServiceContainer.Kernel.Bind<IEntitySerializer>().To( typeof( XmlEntitySerializer ) );
            ServiceContainer.Kernel.Bind<IRepositoryStorage>().To(typeof( XmlRepositoryStorage ));
            ServiceContainer.Kernel.Bind<IGenericFilePathProvider>().To(typeof (SimplePathProvider));
            ServiceContainer.Kernel.Bind<IUpdateService>().To(typeof (UpdateServiceProxy));
            ServiceContainer.Kernel.Bind<BriefServiceQueueManager>().ToConstant(
                new BriefServiceQueueManager(int.Parse(ConfigurationManager.AppSettings["BriefServiceInterval"]),
                    new FakeBriefService()));
            
            PrepareViewModels();
            _commands = new Dictionary<string, KeyValuePair<ICommand, Func<bool>>>();
            _globalVariables = new Dictionary<string, object>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            ServiceContainer.GetInstance<IAppStateController>().IsRunning = false;
        }

        public void ChangePanel(string name)
        {
            GetMainWindow().ChangePanel(name);
        }

        public void Log(string message, string logger, MessageType type)
        {
            Dispatcher.Invoke(new Action(() => GetMainWindow().GetPanel<Panels.TechPanel>().Log(message, logger, type)));
        }

        public void InitAppEngine()
        {
            ServiceContainer.Kernel.Bind<IPythonEngine>().ToConstant(new PythonEngine());
            _appEngine = new AppFacade(RepositorySet.CreateDefault());
            _appEngine.InitBriefService();

        }

        public void RedirectToViewModel(ViewModelBase viewModel, Action onSuccess)
        {
            if (!_viewModelHandlers.ContainsKey(viewModel.GetType()))
            {
                MessageBox.Show("Nie zarejestrowano handlera dla viewModelu: " + viewModel.GetType(), "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _viewModelHandlers[viewModel.GetType()].Handle(viewModel, onSuccess);
        }

        public void HandleOperationResult(OperationResult result, Action onSuccess)
        {
            if (result.Result == OperationResultType.Error)
            {
                MessageBox.Show("Wystąpił błąd: " + result.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (onSuccess == null)
                return;
            onSuccess();
        }

        public void GetItemFromRepo(ItemPosition position, Action<Item> onSuccess)
        {
            var pop = new PopUps.RepoItems(new RepoItemsViewModel()
                                               {
                                                   SelectMode = true,
                                                   SearchType = Rules.GetDefaultType(position)
                                               });
            if (pop.ShowDialog() == true)
            {
                if (pop.Item == null)
                {
                    MessageBox.Show("Nie wybrano przedmiotu", "Bład", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                onSuccess(pop.Item);
            }
        }

        public void ShowItem(Item item)
        {
            var model = new RepoItemsViewModel()
            {
                SearchName = item.Name
            };
            model.SelectedItem = item.Definition;
            var pop = new PopUps.RepoItems(model);

            pop.Show();
        }


        public void RegisterGlobalCommand(ICommand command, string name, Func<bool> canExecuted)
        {
            _commands[name] = new KeyValuePair<ICommand, Func<bool>>(command, canExecuted);
        }


        public void ExecuteGlobalCommand(string name, object param)
        {
            if (!_commands.ContainsKey(name))
                return;
            if (_commands[name].Value == null || _commands[name].Value())
                _commands[name].Key.Execute(param);
        }

        public void SetGlobalVariable<T>(string name, T value)
        {
            _globalVariables[name] = value;
        }

        public void ShowNotifyToUser(string message)
        {
            MessageBox.Show(message, "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public T GetGlobalVariable<T>(string name) where T:class
        {
            if (!_globalVariables.ContainsKey(name))
                return null;
            return (T)_globalVariables[name];
        }

        public T GetVariableFromUser<T>(string message)
        {
            ExpectedVariableTypes type = ExpectedVariableTypes.String;
            if (typeof(T) == typeof(int))
            {
                type = ExpectedVariableTypes.Int;
            }
            else if (typeof(T) == typeof(double))
            {
                type = ExpectedVariableTypes.Double;
            }
            var pop = new PopUps.GetVariable(message, type);
            if (pop.ShowDialog() == true)
            {
                return (T) pop.Variable;
            }
            return default(T);
        }


        public void PrepareViewModels()
        {
            _viewModelHandlers = new Dictionary<Type, ViewModelHandler>();
            _viewModelHandlers.Add(typeof(MoveCharacterViewModel), new ViewModelHandler((m) =>
            {
                var model = m as MoveCharacterViewModel;
                var pop = new PopUps.MoveCharacter(model);
                return pop.ShowDialog() == true;
            }));

            _viewModelHandlers.Add(typeof(AddGroupViewModel), new ViewModelHandler((m) =>
            {
                var pop = new PopUps.AddGroup((AddGroupViewModel)m);
                return pop.ShowDialog() == true;
            }));

            _viewModelHandlers.Add(typeof(RemoveGroupViewModel), new ViewModelHandler((m) =>
            {
                var model = m as RemoveGroupViewModel;
                var success = false;
                model.CommandExecuted += (s, e) => success = e.Result.Result == OperationResultType.Success;
                if (MessageBox.Show(RemoveGroupViewModel.ConfirmationMessage, "Czy na pewno ?", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    model.Commit.Execute(null);
                }
                return success;
            }));

            _viewModelHandlers.Add(typeof(RemoveCharacterViewModel), new ViewModelHandler((m) =>
            {
                var model = m as RemoveCharacterViewModel;
                var success = false;
                model.CommandExecuted += (s, e) => success = e.Result.Result == OperationResultType.Success;
                if (MessageBox.Show("Czy na pewno chcesz usunąć tę postać?", "Czy na pewno ?", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    model.Commit.Execute(null);
                }
                return success;
            }));

            _viewModelHandlers.Add(typeof(AddClassViewModel), new ViewModelHandler((m) =>
            {
                var pop = new PopUps.AddClass((AddClassViewModel)m);
                return pop.ShowDialog() == true;
            }));

            _viewModelHandlers.Add(typeof(ChangeACModel), new ViewModelHandler((m) =>
            {
                var pop = new PopUps.ChangeAC((ChangeACModel)m);
                return pop.ShowDialog() == true;
            }));

            _viewModelHandlers.Add(typeof(RepoItemsViewModel), new ViewModelHandler((m) =>
            {
                GetMainWindow().GetPanel<ReposPanel>().SetPanel("Items", m);
                return true;
            }));

            _viewModelHandlers.Add(typeof(RepoSpellsViewModel), new ViewModelHandler((m) =>
            {
                var model = m as RepoSpellsViewModel;
                if (!model.SelectMode)
                {
                    GetMainWindow().GetPanel<ReposPanel>().SetPanel("Spells", m);
                }
                else
                {
                    var pop = new PopUps.RepoSpells(model);
                    return pop.ShowDialog() == true;
                }
                return true;
            }));

            _viewModelHandlers.Add(typeof(RepoEffectsViewModel), new ViewModelHandler((m) =>
            {
                var model = m as RepoEffectsViewModel;
                if (!model.SelectMode)
                {
                    GetMainWindow().GetPanel<ReposPanel>().SetPanel("Effects", m);
                }
                else
                {
                    var pop = new PopUps.RepoEffects(model);
                    return pop.ShowDialog() == true;
                }
                return true;
            }));

            _viewModelHandlers.Add(typeof(RepoSkillsViewModel), new ViewModelHandler((m) =>
            {
                GetMainWindow().GetPanel<ReposPanel>().SetPanel(ReposViewModel.TabNames.Skills, m);
                return true;
            }));

            _viewModelHandlers.Add(typeof(RepoAtutesViewModel), new ViewModelHandler((m) =>
            {
                var model = m as RepoAtutesViewModel;
                if (!model.SelectMode)
                {
                    GetMainWindow().GetPanel<ReposPanel>().SetPanel(ReposViewModel.TabNames.Atutes, m);
                }
                else
                {
                    var pop = new PopUps.RepoAtuts(model);
                    return pop.ShowDialog() == true;
                }
                return true;
            }));


            _viewModelHandlers.Add(typeof(ConfirmationViewModel), new ViewModelHandler((m) =>
            {
                var model = m as ConfirmationViewModel;
                if (MessageBox.Show(model.ConfirmationMessage, "Potwierdzenie", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    model.ActionWhenConfirmed();
                }
                return true;
            }));

            _viewModelHandlers.Add(typeof(AddBonusViewModel), new ViewModelHandler((m) =>
            {
                var pop = new PopUps.AddEditBonus((AddBonusViewModel)m);
                return pop.ShowDialog() == true;
            }));

            _viewModelHandlers.Add(typeof(AddAttackViewModel), new ViewModelHandler((m) =>
            {
                var pop = new PopUps.AddAttack((AddAttackViewModel)m);
                return pop.ShowDialog() == true;
            }));

            _viewModelHandlers.Add(typeof(ScriptEditorViewModel), new ViewModelHandler((m) =>
            {
                var pop = new PopUps.ScriptEditor((ScriptEditorViewModel)m);
                return pop.ShowDialog() == true;
            }));

            _viewModelHandlers.Add(typeof(AddBattleMemberViewModel), new ViewModelHandler((m) =>
            {
                var pop = new PopUps.AddBattleMember((AddBattleMemberViewModel)m);
                return pop.ShowDialog() == true;
            }));

            _viewModelHandlers.Add(typeof(GlobalEffectsViewModel), new ViewModelHandler((m) =>
            {
                var pop = new PopUps.GlobalEffects((GlobalEffectsViewModel)m);
                return pop.ShowDialog() == true;
            }));

            _viewModelHandlers.Add(typeof(RepoRacesViewModel), new ViewModelHandler((m) =>
            {
                GetMainWindow().GetPanel<ReposPanel>().SetPanel(ReposViewModel.TabNames.Races, m);
                return true;
            }));

            _viewModelHandlers.Add( typeof( RepoClassesViewModel ), new ViewModelHandler( ( m ) =>
            {
                GetMainWindow().GetPanel<ReposPanel>().SetPanel( ReposViewModel.TabNames.Classes, m );
                return true;
            } ) );

            _viewModelHandlers.Add(typeof(CopyEffectViewModel), new ViewModelHandler((m) =>
            {
                var pop = new PopUps.CopyEffect((CopyEffectViewModel) m);
                return pop.ShowDialog() == true;
            }));

            _viewModelHandlers.Add(typeof(MoveItemViewModel), new ViewModelHandler((m) =>
            {
                var pop = new PopUps.MoveItem((MoveItemViewModel) m);
                return pop.ShowDialog() == true;
            }));

            _viewModelHandlers.Add( typeof( AddExperienceViewModel ), new ViewModelHandler( ( m ) =>
            {
                var pop = new PopUps.AddExperience( (AddExperienceViewModel)m );
                return pop.ShowDialog() == true;
            } ) );
        }

        
    }
}
