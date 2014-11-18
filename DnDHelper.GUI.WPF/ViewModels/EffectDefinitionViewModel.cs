using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class EffectDefinitionViewModel : ViewModelBase
    {
        private readonly EffectDefinition _owner;

        public string Name
        {
            get { return _owner.Name; }
            set { _owner.Name = value; PropertyHasChanged("Name"); PendingChanges = true; BonusModel.BonusSource = value; }
        }

        public string Description
        {
            get { return _owner.Description; }
            set { _owner.Description = value; PropertyHasChanged("Description"); PendingChanges = true; }
        }

        public int? DefaultDuration
        {
            get { return _owner.DefaultDuration; }
            set { _owner.DefaultDuration = value; PropertyHasChanged("DefaultDuration"); PendingChanges = true; }
        }

        public int DefaultCounter
        {
            get { return _owner.DefaultCounter; }
            set { _owner.DefaultCounter = value; PropertyHasChanged("DefaultCounter"); PendingChanges = true; }
        }

        public bool IsPositive
        {
            get { return _owner.IsPositive; }
            set { _owner.IsPositive = value; PropertyHasChanged("IsPositive"); PendingChanges = true; }
        }

        public bool IsBattleEffect
        {
            get { return _owner.IsBattleEffect; }
            set { _owner.IsBattleEffect = value; PropertyHasChanged("IsBattleEffect"); PendingChanges = true; }
        }

        public BonusEditorViewModel BonusModel { get; set; }

        private bool _pendingChanges;
        public bool PendingChanges { get { return _pendingChanges; } set { _pendingChanges = value; PropertyHasChanged("PendingChanges"); } }
        public bool InsertMode { get; set; }
        public ICommand Commit { get; private set; }

        public EffectDefinitionViewModel(EffectDefinition owner)
        {
            _owner = owner;
            BonusModel = new BonusEditorViewModel(_owner.Bonuses, Name);
            BonusModel.ChangeOccured += (o, e) => PendingChanges = true;
            Commit = new Command((o) =>
                                     {
                                         var appApi = ServiceContainer.GetInstance<IAppAPI>();
                                         var appFacade = ServiceContainer.GetInstance<AppFacade>();
                                         if (InsertMode)
                                         {
                                             appApi.HandleOperationResult(appFacade.AddEffect(_owner));
                                         }
                                         else
                                         {
                                             appFacade.EffectsChanged( _owner );
                                         }
                                         PendingChanges = false;
                                     }, this, () => PendingChanges, "PendingChanges");
        }
    }
}