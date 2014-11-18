using System;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class EffectViewModel : ViewModelBase
    {
        private readonly Effect _owner;
        public Effect Owner { get { return _owner; } }
        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);
        private readonly Lazy<IAppAPI> _appApi = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);
        private readonly CharacterTabEffectsModel _parentModel;
        private readonly bool _refreshProperties;

        public int? Duration
        {
            get { return _owner.Duration; }
            set { _owner.Duration = value; RefreshProperties();  PropertyHasChanged("Duration"); }
        }

        public bool Permanent
        {
            get { return Duration == null; }
            set 
            { 
                if (value)
                {
                    Duration = null;
                }
                else
                {
                    Duration = 0;
                }
                PropertyHasChanged("Permanent");
                PropertyHasChanged("Duration");
            }
        }

        public int Counter
        {
            get { return _owner.Counter; }
            set { _owner.Counter = value; RefreshProperties(); PropertyHasChanged("Counter"); }
        }

        public string InstanceName
        {
            get { return _owner.InstanceName; }
            set { _owner.InstanceName = value; RefreshProperties(); PropertyHasChanged("InstanceName"); }
        }

        public string CustomText
        {
            get { return _owner.CustomText; }
            set { _owner.CustomText = value; RefreshProperties(); PropertyHasChanged("CustomText"); }
        }

        public BonusEditorViewModel Bonuses { get; set; }

        public EffectViewModel(Effect owner, CharacterTabEffectsModel parentModel, bool refreshCharacter)
        {
            _owner = owner;
            _parentModel = parentModel;
            Bonuses = new BonusEditorViewModel(_owner.Bonuses, _owner.Name);
            Bonuses.ChangeOccured += (o, e) => RefreshProperties();
            _refreshProperties = refreshCharacter;
        }

        private void RefreshProperties()
        {
            if (!_refreshProperties)
                return;

            _appFacade.Value.CharacterChange(_parentModel.Character); 
            _parentModel.RefreshEffects();
        }
    }
}