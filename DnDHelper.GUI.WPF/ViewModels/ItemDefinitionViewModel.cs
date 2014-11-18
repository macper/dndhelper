using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class ItemDefinitionViewModel : ViewModelBase
    {
        private readonly ItemDefinition _owner;

        public string Name
        {
            get { return _owner.Name; }
            set { _owner.Name = value; PropertyHasChanged("Name");
                BonusEditorModel.BonusSource = value; PendingChanges = true; }
        }

        public BaseTypes Type
        {
            get { return _owner.BaseType; }
            set { _owner.BaseType = value; PropertyHasChanged("Type"); PendingChanges = true; }
        }

        public bool IsPrototype
        {
            get { return _owner.IsPrototype; }
            set { _owner.IsPrototype = value; PropertyHasChanged("IsPrototype"); PendingChanges = true; }
        }

        public IEnumerable<string> Prototypes
        {
            get { return ServiceContainer.GetInstance<AppFacade>().RepoItems.Where(i => i.IsPrototype).Select(s => s.Name); }
        }

        public string Prototype
        {
            get { return _owner.PrototypeName; }
            set { _owner.PrototypeName = value; PropertyHasChanged("Prototype"); PendingChanges = true; }
        }

        public int Price
        {
            get { return _owner.Cost; }
            set { _owner.Cost = value; PropertyHasChanged("Price"); PendingChanges = true; }
        }

        public int Charges
        {
            get { return _owner.InitialCharges; }
            set { _owner.InitialCharges = value; PropertyHasChanged("Charges"); PendingChanges = true; }
        }

        public string Specials
        {
            get { return _owner.Specials; }
            set
            {
                _owner.Specials = value;
                PropertyHasChanged("Specials");
                PendingChanges = true;
            }
        }

        private BonusEditorViewModel _bonusEditorModel;
        public BonusEditorViewModel BonusEditorModel
        {
            get { return _bonusEditorModel; }
            set 
            { 
                _bonusEditorModel = value;
                _bonusEditorModel.ChangeOccured += (o, e) =>
                                                       {
                                                           PendingChanges = true;
                                                       };
                PropertyHasChanged("BonusEditorModel");
            }
        }

        public ICommand Save { get; set; }

        private bool _pendingChanges;
        public bool PendingChanges
        {
            get { return _pendingChanges; }
            set { _pendingChanges = value; PropertyHasChanged("PendingChanges"); }
        }

        public bool InsertMode { get; set; }
        public bool SelectMode { get; set; }

        public ItemDefinitionViewModel(ItemDefinition owner)
        {
            _owner = owner;
            BonusEditorModel = new BonusEditorViewModel(_owner.Bonuses, Name);
            Save = new Command((o) =>
                                   {
                                       if (InsertMode)
                                       {
                                           var result = ServiceContainer.GetInstance<AppFacade>().AddItem(_owner);
                                           ServiceContainer.GetInstance<IAppAPI>().HandleOperationResult(result);
                                       }
                                       else
                                       {
                                           ServiceContainer.GetInstance<AppFacade>().ItemsChange(_owner);
                                       }
                                       PendingChanges = false;
                                   }, this, () => PendingChanges, "PendingChanges");
            
        }
    }
}
