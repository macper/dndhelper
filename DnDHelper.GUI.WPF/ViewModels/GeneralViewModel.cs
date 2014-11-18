using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;
using System.Collections.ObjectModel;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class GeneralViewModel : ViewModelBase
    {
        private readonly IGameTimer _timer;
        private readonly AppFacade _appEngine;

        public GeneralViewModel()
        {
            _timer = ServiceContainer.GetInstance<IGameTimer>();
            _appEngine = ServiceContainer.GetInstance<AppFacade>();
            _timer.SubscribeOnTurnChange("GeneralViewModel", (t) => PropertyHasChanged("CurrentTime"));
            AddDay = new Command((o) =>
                                     {
                                         _timer.AddDays(1);
                                         PropertyHasChanged("CurrentTime");
                                     });
            AddHour = new Command((o) =>
            {
                _timer.AddHours(1);
                PropertyHasChanged("CurrentTime");
            });
            AddMinute = new Command((o) =>
            {
                _timer.AddMinutes(1);
                PropertyHasChanged("CurrentTime");
            });
            AddTurn = new Command((o) =>
            {
                _timer.AddTurns(1);
                PropertyHasChanged("CurrentTime");
            });
            SubtractDay = new Command((o) =>
            {
                _timer.AddDays(-1);
                PropertyHasChanged("CurrentTime");
            });
            SubtractHour = new Command((o) =>
            {
                _timer.AddHours(-1);
                PropertyHasChanged("CurrentTime");
            });
            SubtractMinute = new Command((o) =>
            {
                _timer.AddMinutes(-1);
                PropertyHasChanged("CurrentTime");
            });

            ServiceContainer.GetInstance<IAppAPI>().RegisterGlobalCommand(new Command((o) =>
                                                                                          {
                                                                                              _appEngine.SetUpTime();
                                                                                              PropertyHasChanged("CurrentTime");
                                                                                              PropertyHasChanged("Notes");
                                                                                          }), GlobalCommands.RefreshAppSettings);

            Experiences = new ObservableCollection<Experience>( _appEngine.Repositories.Get<Experience>().Elements );

            ServiceContainer.GetInstance<IAppAPI>().RegisterGlobalCommand( new Command( ( o ) =>
            {
                Experiences = new ObservableCollection<Experience>( _appEngine.Repositories.Get<Experience>().Elements );
                PropertyHasChanged("Experiences");
                PropertyHasChanged("ExperienceSum");
            } ), GlobalCommands.RefreshExperience );

            RemoveExperience = new Command( ( o ) =>
            {
                _appEngine.RemoveExperience( SelectedExperience );
                Experiences.Remove( SelectedExperience );
                SelectedExperience = null;
                PropertyHasChanged("ExperienceSum");
            }, this, () => SelectedExperience != null, "SelectedExperience" );

            RemoveAllExperiences = new Command((o) =>
            {
                _appEngine.RemoveAllExperiences();
                Experiences.Clear();
                SelectedExperience = null;
                PropertyHasChanged("ExperienceSum");
            }, this, () => SelectedExperience != null, "SelectedExperience");
        }

        public string CurrentTime
        {
            get { return _timer.CurrentTime.ToString("F", new System.Globalization.CultureInfo("pl")); }
        }

        public string Notes
        {
            get { return _appEngine.Notes; }
            set 
            { 
                _appEngine.Notes = value;
                PropertyHasChanged("Notes");
            }
        }

        public ObservableCollection<Experience> Experiences { get; set; }

        private Experience _selectedExperience;
        public Experience SelectedExperience
        {
            get
            {
                return _selectedExperience;
            }
            set
            {
                _selectedExperience = value; PropertyHasChanged( "SelectedExperience" );
            }
        }

        public int ExperienceSum
        {
            get
            {
                return Experiences.Sum(e => e.Amount);
            }
        }

        public ICommand AddDay { get; set; }
        public ICommand AddHour { get; set; }
        public ICommand AddMinute { get; set; }
        public ICommand AddTurn { get; set; }
        public ICommand SubtractDay { get; set; }
        public ICommand SubtractHour { get; set; }
        public ICommand SubtractMinute { get; set; }

        public ICommand RemoveExperience { get; set; }

        public ICommand RemoveAllExperiences { get; set; }

    }
}
