using DnDHelper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class AddExperienceViewModel : ViewModelBase
    {
        private Experience _experience;
        private AppFacade _appFacade;
        private IAppAPI _appApi;

        public string Name
        {
            get
            {
                return _experience.Name;
            }
            set
            {
                _experience.Name = value;
            }
        }

        public int Amount
        {
            get
            {
                return _experience.Amount;
            }
            set
            {
                _experience.Amount = value;
                PropertyHasChanged( "Amount" );
            }
        }

        public ICommand AddExp { get; private set; }
        public ICommand Commit { get; private set; }

        public AddExperienceViewModel()
        {
            _experience = new Experience();
            _appFacade = ServiceContainer.GetInstance<AppFacade>();
            _appApi = ServiceContainer.GetInstance<IAppAPI>();
            _experience.Time = _appFacade.GameTimer.CurrentTime;

            AddExp = new Command( ( o ) =>
            {
                Amount += int.Parse((string)o);
            } );

            Commit = new Command( ( o ) =>
            {
                if( string.IsNullOrEmpty( _experience.Name ) )
                {
                    _appApi.ShowNotifyToUser( "Nazwa jest obowiązkowa" );
                    return;
                }

                var result = _appFacade.AddExperience( _experience );
                CommandHasExecuted( "Commit", result );
            } );
        }
    }
}
