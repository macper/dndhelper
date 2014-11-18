using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using DnDHelper.Domain;
using System.Threading.Tasks;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class TechViewModel : ViewModelBase
    {

        public ICommand Synchronize { get; private set; }
        public ICommand Refresh { get; private set; }
        public ICommand Populate { get; private set; }

        public ObservableCollection<RepositoryChange> Repositories { get; private set; }

        private bool _processing;
        public bool Processing
        {
            get { return _processing; }
            set { _processing = value; Dispatcher.CurrentDispatcher.BeginInvoke(new Action<string>(PropertyHasChanged), "Processing"); }
        }

        private Lazy<IAppAPI> _api = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);

        public DateTime LastSyncTime
        {
            get
            {
                return new DateTime(ChangeTracker.Instance.LastSync);
            }
        }

        public TechViewModel()
        {
            RefreshRepoList();

            Refresh = new Command((o) =>
            {
                RefreshRepoList();
            }, this, () => !Processing, "Processing");

            Synchronize = new Command(async (o) =>
                    {
                        Processing = true;
                        var result = await SynchronizeAsync();

                        Processing = false;
                        _api.Value.HandleOperationResult(result);
                        RefreshRepoList();
                    }, this, () => !Processing, "Processing");

            Populate = new Command((o) =>
            {
                _api.Value.RedirectToViewModel(new ConfirmationViewModel("Czy na pewno ? To podmieni całą zawartość repo na serwerze!", async () =>
                {
                    Processing = true;
                    var result = await PopulateAsync();
                    Processing = false;
                    _api.Value.HandleOperationResult(result);
                }));
            }, this, () => !Processing, "Processing");

            Processing = false;
        }

        private void RefreshRepoList()
        {
            Repositories = new ObservableCollection<RepositoryChange>(ChangeTracker.Instance.RepositoryChanges.Where(e => e.EntityChanges.Any(x => x.TimeStamp > ChangeTracker.Instance.LastSync)));
            PropertyHasChanged("Repositories");
            PropertyHasChanged("LastSyncTime");
        }

        private async Task<OperationResult> PopulateAsync()
        {
            var task = new Task<OperationResult>(() =>
            {
                return ServiceContainer.GetInstance<IUpdateClient>().InitialPopulate();
            });

            task.Start();
            return await task;
        }

        private async Task<OperationResult> SynchronizeAsync()
        {
            var task = new Task<OperationResult>(() =>
            {
                return ServiceContainer.GetInstance<IUpdateClient>().Synchronize();
            });

            task.Start();
            return await task;
        }
    }
}
