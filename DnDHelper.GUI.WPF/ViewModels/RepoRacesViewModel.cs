using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class RepoRacesViewModel : ViewModelBase
    {
        public ObservableCollection<RaceDefinition> Races { get { return new ObservableCollection<RaceDefinition>(_appFacade.Value.RepoRaces); } }

        public BonusEditorViewModel Bonuses { get; set; }

        private RaceDefinition _selectedRace;
        public RaceDefinition SelectedRace
        {
            get { return _selectedRace; }
            set
            {
                _selectedRace = value;
                Bonuses = new BonusEditorViewModel(_selectedRace.Bonuses, "Rasa");
                PropertyHasChanged("Bonuses");
                PropertyHasChanged("SelectedRace");
                _insertMode = false;
            }
        }

        public ICommand Add { get; private set; }
        public ICommand Commit { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand EditScript { get; private set; }

        private Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);
        private Lazy<IAppAPI> _appAPI = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);

        private bool _insertMode;

        public ObservableCollection<Script> Scripts 
        {
            get
            {
                return new ObservableCollection<Script>( ServiceContainer.GetInstance<RepositorySet>().Get<Script>().Elements.Where(e => e.ScriptContext == ScriptContext.Race));
            }
        }

        public RepoRacesViewModel()
        {
            Add = new Command((o) => {
                SelectedRace = new RaceDefinition()
                {
                    Name = "(nowa rasa)"
                };
                _insertMode = true;
            });
            Commit = new Command((o) => {
                if (_insertMode)
                {
                    var res = _appFacade.Value.AddRace(SelectedRace);
                    if (res.Result == OperationResultType.Error)
                    {
                        _appAPI.Value.ShowNotifyToUser(res.Message);
                        return;
                    }
                }
                else
                {
                    _appFacade.Value.RacesChanged( SelectedRace );
                }
                _appAPI.Value.ShowNotifyToUser("Zapisano pomyślnie");
                PropertyHasChanged("Races");
            });
            Remove = new Command((o) =>
            {
                _appFacade.Value.RemoveRace(SelectedRace);
                PropertyHasChanged("Races");
            }, this, () => SelectedRace != null, "SelectedRace");
            EditScript = new Command( ( o ) => _appAPI.Value.RedirectToViewModel( new ScriptEditorViewModel( ScriptContext.Race ), () => PropertyHasChanged( "Scripts" ) ) );
        }

    }
}
