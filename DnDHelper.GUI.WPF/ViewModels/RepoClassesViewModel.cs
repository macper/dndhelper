using DnDHelper.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class RepoClassesViewModel : ViewModelBase
    {
        private ClassDefinition _selectedClass;
        public ClassDefinition SelectedClass
        {
            get
            {
                return _selectedClass;
            }
            set
            {
                _selectedClass = value;
                PropertyHasChanged( "SelectedClass" );
                if( _selectedClass == null )
                {
                    _selectedClassViewModel = null;
                    return;
                }
                _selectedClassViewModel = new ClassViewModel( _selectedClass );
                PropertyHasChanged( "SelectedClassViewModel" );
                _selectedClassViewModel.CommandExecuted += ( o, e ) =>
                {
                    if( e.CommandName == "Commit" )
                    {
                        PropertyHasChanged( "Classes" );
                    }
                };
            }
        }

        private ClassViewModel _selectedClassViewModel;
        public ClassViewModel SelectedClassViewModel
        {
            get
            {
                return _selectedClassViewModel;
            }
            set
            {
                _selectedClassViewModel = value;
                
                PropertyHasChanged( "SelectedClassViewModel" );
            }
        }


        public ObservableCollection<ClassDefinition> Classes
        {
            get
            {
                return new ObservableCollection<ClassDefinition>( ServiceContainer.GetInstance<AppFacade>().RepoClasses );
            }
        }

        public ICommand AddClass { get; private set; }

        public ICommand RemoveClass { get; private set; }

        private Lazy<IAppAPI> _appApi = new Lazy<IAppAPI>( () => ServiceContainer.GetInstance<IAppAPI>() );
        private Lazy<AppFacade> _appFacade = new Lazy<AppFacade>( () => ServiceContainer.GetInstance<AppFacade>() );

        public RepoClassesViewModel()
        {
            AddClass = new Command( ( o ) =>
            {
                SelectedClassViewModel = new ClassViewModel( new ClassDefinition
                {
                    Name = "(nowa klasa)"
                } );
                SelectedClassViewModel.CommandExecuted += ( s, e ) =>
                {
                    if( e.CommandName == "Commit" )
                    {
                        PropertyHasChanged( "Classes" );
                    }
                };
            } );

            RemoveClass = new Command( ( o ) =>
            {
                _appApi.Value.RedirectToViewModel( new ConfirmationViewModel( "Czy na pewno chcesz usunąć tę klasę?",
                                               () =>
                                               {
                                                   _appFacade.Value.RemoveClass( SelectedClass );
                                                   SelectedClassViewModel = null;
                                                   PropertyHasChanged( "Classes" );
                                               } ) );
            } );
        }
    }
}
