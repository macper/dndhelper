using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class RepoEffectsViewModel : ViewModelBase
    {
        private string _searchName;
        public bool SelectMode { get; set; }
        public bool GlobalEffectsMode { get; set; }

        public string SearchName
        {
            get { return _searchName; }
            set { _searchName = value; PropertyHasChanged("SearchName"); FillList(); }
        }

        public ObservableCollection<EffectDefinition> Effects { get; set; }

        private EffectDefinition _selectedEffect;
        public EffectDefinition SelectedEffect
        {
            get
            {
                return _selectedEffect;
            } 
            set
            {
                _selectedEffect = value;
                PropertyHasChanged("SelectedEffect");
                EffectModel = new EffectDefinitionViewModel(value);
                PropertyHasChanged("EffectModel");
                Effect = null;
            }
        }

        public EffectDefinitionViewModel EffectModel { get; set; }
        private EffectViewModel _effect;
        public EffectViewModel Effect { get { return _effect; } set { _effect = value; PropertyHasChanged("Effect"); PropertyHasChanged("IsEffectVisible"); } }
        public bool IsEffectVisible { get { return Effect != null && SelectMode; } }

        public ICommand AddEffect { get; private set; }
        public ICommand RemoveEffect { get; private set; }
        public ICommand SelectEffect { get; private set; }
        public ICommand ConfirmSelect { get; private set; }
        public ICommand ConvertToMins { get; private set; }
        public ICommand ConvertToHours { get; private set; }
        public ICommand EffectChange { get; private set; }

        private readonly Lazy<IAppAPI> _appApi = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);
        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);

        private readonly Character character;

        public RepoEffectsViewModel(bool selectMode = false, bool globalEffectsMode = false, Character ch = null)
        {
            character = ch;
            SelectMode = selectMode;
            GlobalEffectsMode = globalEffectsMode;
            FillList();
            AddEffect = new Command((o) =>
                                        {
                                            SelectedEffect = new EffectDefinition() {Name = "(nowy efekt)"};
                                            EffectModel.InsertMode = true;
                                        });
            RemoveEffect = new Command((o) =>
                                           {
                                               _appApi.Value.HandleOperationResult(_appFacade.Value.RemoveEffect(SelectedEffect));
                                               PropertyHasChanged("Effects");
                                           }, this, () => SelectedEffect != null, "SelectedEffect");
            SelectEffect = new Command((o) =>
                                           {
                                               Effect = new EffectViewModel(SelectedEffect.CreateItem(), null, false);
                                           }, this, () => SelectedEffect != null, "SelectedEffect");
            ConfirmSelect = new Command((o) =>
                                            {
                                                OperationResult res = OperationResult.Success();
                                                if (!GlobalEffectsMode)
                                                {
                                                    res = character.Controller.AddEffect(Effect.Owner);
                                                    _appApi.Value.HandleOperationResult(res);
                                                }
                                                else
                                                {
                                                    Battle.Instance.GlobalEffects.Add(Effect.Owner);
                                                }
                                                CommandHasExecuted("CommitSelect", res);
                                            });
            ConvertToMins = new Command((o) =>
                                            {
                                                if (!Effect.Duration.HasValue)
                                                    return;
                                                Effect.Duration = DescriptionsDictionary.MinutesToTurns(Effect.Duration.Value);
                                            });
            ConvertToHours = new Command((o) =>
                                             {
                                                 if (!Effect.Duration.HasValue)
                                                    return;
                                                Effect.Duration = DescriptionsDictionary.HoursToTurns(Effect.Duration.Value);
                                             });
            EffectChange = new Command((o) => _appFacade.Value.CharacterChange(character));
        }

        private void FillList()
        {
            var appFacade = ServiceContainer.GetInstance<AppFacade>();
            Effects = new ObservableCollection<EffectDefinition>(appFacade.RepoEffects.Where(e => string.IsNullOrEmpty(_searchName) || e.Name.ToLower().StartsWith(_searchName.ToLower())));
            PropertyHasChanged("Effects");
        }
    }
}
