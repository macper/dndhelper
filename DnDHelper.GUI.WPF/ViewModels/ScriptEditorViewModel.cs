using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;
using System.Collections.ObjectModel;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class ScriptEditorViewModel : ViewModelBase
    {
        private Script _selectedScript;
        public Script SelectedScript
        {
            get { return _selectedScript; } 
            set { _selectedScript = value; PropertyHasChanged("SelectedScript"); }
        }

        public ICommand AddScript { get; private set; }
        public ICommand RemoveScript { get; private set; }
        public ICommand Commit { get; private set; }
        public ICommand Close { get; private set; }

        public ObservableCollection<Script> Scripts { get; set; }

        public ScriptEditorViewModel( ScriptContext scriptContext )
        {
            var scriptRepository = ServiceContainer.GetInstance<RepositorySet>().Get<Script>();

            AddScript = new Command((o) =>
                                        {
                                            SelectedScript = new Script(scriptContext) { Name = "Nowy skrypt" };
                                        });
            RemoveScript = new Command((o) =>
                                           {
                                               scriptRepository.RemoveElement( SelectedScript );
                                               scriptRepository.Commit( SelectedScript, false );
                                               Scripts.Remove( SelectedScript );
                                               PropertyHasChanged( "Scripts" );
                                           }, this, () => SelectedScript != null, "SelectedScript");
            Commit = new Command((o) =>
                                     {
                                         var exising = scriptRepository.GetElementByName( SelectedScript.Name );
                                         if( exising == null )
                                         {
                                             scriptRepository.AddElement( SelectedScript );
                                             Scripts.Add( SelectedScript );
                                             PropertyHasChanged( "Scripts" );
                                         }
                                         else
                                         {
                                             ServiceContainer.GetInstance<IPythonEngine>().ReloadScript( SelectedScript );
                                         }

                                         scriptRepository.Commit( SelectedScript, true );
                                         PropertyHasChanged("Repository.Scripts");
                                         ServiceContainer.GetInstance<IAppAPI>().ShowNotifyToUser("Zapisano pomyślnie");
                                     }, this, () => SelectedScript != null, "SelectedScript" );
            Close = new Command((o) => CommandHasExecuted("Close", OperationResult.Success()));

            Scripts = new ObservableCollection<Script>( scriptRepository.Elements.Where( e => e.ScriptContext == scriptContext ) );
        }
    }
}
