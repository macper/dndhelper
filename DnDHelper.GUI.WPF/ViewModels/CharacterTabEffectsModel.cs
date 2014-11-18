using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class CharacterTabEffectsModel : CharacterTabBaseModel
    {
        public ObservableCollection<Effect> Effects { get { return new ObservableCollection<Effect>(Character.Effects); } }

        public ICommand AddEffect { get; private set; }
        public ICommand RemoveEffect { get; private set; }

        private readonly Lazy<IAppAPI> _api = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);
        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);

        public ObservableCollection<Script> Scripts { get { return new ObservableCollection<Script>(_appFacade.Value.RepoScripts.Where(s => s.ScriptContext == ScriptContext.Custom)); } }
        
        private Script _selectedScript;
        public Script SelectedScript
        {
            get { return _selectedScript; }
            set { _selectedScript = value; PropertyHasChanged( "SelectedScript" ); }
        }

        public ICommand EditScript { get; private set; }
        public ICommand ExecuteScript { get; private set; }

        private Effect _selectedEffect;
        public Effect SelectedEffect
        {
            get { return _selectedEffect; }
            set
            {
                _selectedEffect = value; 
                PropertyHasChanged("SelectedEffect");
                if (value == null)
                    return;

                EffectModel = new EffectViewModel(value, this, true);
                PropertyHasChanged("EffectModel");
            }
        }

        public EffectViewModel EffectModel { get; set; }

        

        public CharacterTabEffectsModel(Character character) : base(character)
        {
            PropertyHasChanged("Effects");
            AddEffect = new Command((o) => _api.Value.RedirectToViewModel(new RepoEffectsViewModel(true, false, Character), () => { PropertyHasChanged("Effects"); _appFacade.Value.CharacterChange(Character); }));
            RemoveEffect = new Command((o) =>
                                           {
                                               _api.Value.HandleOperationResult(Character.Controller.RemoveEffect(SelectedEffect));
                                               PropertyHasChanged("Effects");
                                               _appFacade.Value.CharacterChange(Character); 
                                           }, this, () => SelectedEffect != null, "SelectedEffect");

            EditScript = new Command( ( o ) =>
            {
                _api.Value.RedirectToViewModel( new ScriptEditorViewModel( ScriptContext.Custom ), () => PropertyHasChanged("Scripts") );
            } );

            ExecuteScript = new Command( ( o ) =>
            {
                var engine = ServiceContainer.GetInstance<IPythonEngine>();
                try
                {
                    engine.ReloadScript( SelectedScript );
                    var method = engine.GetMethod<Action<Character, object>>( ScriptContext.Custom, SelectedScript.Name );
                    string parameter = null;
                    if (! string.IsNullOrEmpty(SelectedScript.ParamName))
                    {
                        parameter = _api.Value.GetVariableFromUser<string>(SelectedScript.ParamName);
                    }
                    method(Character, parameter);
                    _appFacade.Value.CharacterChange( Character );
                    PropertyHasChanged( "Effects" );
                }
                catch( Exception exception )
                {
                    _api.Value.HandleOperationResult( OperationResult.Error( exception.Message ) );
                }
            }, this, () => SelectedScript != null, "SelectedScript" );
        }

        public void RefreshEffects()
        {
            PropertyHasChanged("Effects");
        }
    }
}
