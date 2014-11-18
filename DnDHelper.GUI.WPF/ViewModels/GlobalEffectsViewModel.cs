using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class GlobalEffectsViewModel : ViewModelBase
    {
        public ObservableCollection<Effect> GlobalEffects
        {
            get { return new ObservableCollection<Effect>(Battle.Instance.GlobalEffects); }
        }

        public Effect SelectedEffect
        {
            get { return _selectedEffect; }
            set 
            { 
                _selectedEffect = value; 
                PropertyHasChanged("SelectedEffect");
                PropertyHasChanged("MembersWithEffect");
                PropertyHasChanged("MembersWithoutEffect");
            }
        }

        private Effect _selectedEffect;

        public ObservableCollection<BattleCharacter> MembersWithEffect
        {
            get { return new ObservableCollection<BattleCharacter>(SelectedEffect != null ? Battle.Instance.Members.Where(m => m.Character.Effects.Any(e => e.OriginalInstanceName == SelectedEffect.InstanceName)) : new List<BattleCharacter>()); }
        }

        public ObservableCollection<BattleCharacter> MembersWithoutEffect
        {
            get { return new ObservableCollection<BattleCharacter>(SelectedEffect != null ? Battle.Instance.Members.Where(m => !m.Character.Effects.Any(e => e.OriginalInstanceName == SelectedEffect.InstanceName)) : new List<BattleCharacter>()); }
        }

        private BattleCharacter _selectedCharacter;
        public BattleCharacter SelectedCharacter
        {
            get { return _selectedCharacter; }
            set { _selectedCharacter = value; PropertyHasChanged("SelectedCharacter"); }
        }

        public ICommand AddEffect { get; private set; }
        public ICommand RemoveEffect { get; private set; }
        public ICommand AssignEffect { get; private set; }
        public ICommand ResignEffect { get; private set; }
        public ICommand Commit { get; private set; }
        

        private readonly Lazy<IAppAPI> _api = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);
        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);

        public GlobalEffectsViewModel()
        {
            AddEffect = new Command((o) => _api.Value.RedirectToViewModel(new RepoEffectsViewModel(true, true), () => PropertyHasChanged("GlobalEffects")));
            RemoveEffect = new Command((o) =>
                                           {
                                               Battle.Instance.GlobalEffects.Remove(SelectedEffect);
                                               Battle.Instance.Members.Where(m => m.Character.Effects.Contains(SelectedEffect)).ToList().ForEach(g => g.Character.Controller.RemoveEffect(SelectedEffect));
                                               _appFacade.Value.CharacterChange(null);
                                               SelectedEffect = null;
                                               PropertyHasChanged("GlobalEffects");
                                               PropertyHasChanged("MembersWithEffect");
                                               PropertyHasChanged("MembersWithoutEffect");
                                           }, this, () => SelectedEffect != null, "SelectedEffect");
            AssignEffect = new Command((o) =>
                                           {
                                               var newEff = DarkTemplar.DeepClone(SelectedEffect, true);
                                               newEff.OriginalInstanceName = SelectedEffect.InstanceName;
                                               SelectedCharacter.Character.Controller.AddEffect(newEff);
                                               PropertyHasChanged("MembersWithEffect");
                                               PropertyHasChanged("MembersWithoutEffect");
                                               _appFacade.Value.CharacterChange(null);
                                           }, this, () => SelectedCharacter != null && SelectedEffect != null && MembersWithoutEffect.Contains(SelectedCharacter), "SelectedCharacter");
            ResignEffect = new Command((o) =>
                                           {
                                               SelectedCharacter.Character.Controller.RemoveEffect(SelectedCharacter.Character.Effects.SingleOrDefault(e => e.OriginalInstanceName == SelectedEffect.InstanceName));
                                               PropertyHasChanged("MembersWithEffect");
                                               PropertyHasChanged("MembersWithoutEffect");
                                               _appFacade.Value.CharacterChange(null);
                                           }, this, () => SelectedCharacter != null && SelectedEffect != null && MembersWithEffect.Contains(SelectedCharacter), "SelectedCharacter");
            Commit = new Command((o) => CommandHasExecuted("Commit", OperationResult.Success()));
        }
    }
}
