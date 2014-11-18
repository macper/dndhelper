using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class SpellDefinitionViewModel : ViewModelBase
    {
        private bool _pendingChanges;
        private readonly SpellDefinition _owner;

        public bool PendingChanges
        {
            get { return _pendingChanges; }
            set { _pendingChanges = value; PropertyHasChanged("PendingChanges"); }
        }

        public string Name
        {
            get { return _owner.Name; }
            set { _owner.Name = value; PropertyHasChanged("Name"); PendingChanges = true; }
        }

        public int Level
        {
            get { return _owner.Level; }
            set { _owner.Level = value; PropertyHasChanged("Level"); PendingChanges = true; }
        }

        private List<SpellTypeModel> _types;
        public List<SpellTypeModel> Types
        {
            get { return _types; }
            set { _types = value; }
        }

        public SpellShool School
        {
            get { return _owner.SpellShool; }
            set { _owner.SpellShool = value; PropertyHasChanged("Schools"); PendingChanges = true; }
        }

        public string Description
        {
            get { return _owner.Description; }
            set { _owner.Description = value; PropertyHasChanged("Description"); PendingChanges = true; }
        }

        public string Duration
        {
            get { return _owner.Duration; }
            set { _owner.Duration = value; PropertyHasChanged("Duration"); PendingChanges = true; }
        }

        public SpellRange Range
        {
            get { return _owner.Range; }
            set { _owner.Range = value; PropertyHasChanged("Range"); PendingChanges = true; }
        }

        public ICommand Commit { get; private set; }

        public SpellDefinitionViewModel(SpellDefinition owner)
        {
            _owner = owner;
            Commit = new Command((o) =>
                                     {
                                         PendingChanges = false;
                                         ServiceContainer.GetInstance<AppFacade>().SpellChange(owner);
                                     }, this, () => PendingChanges, "PendingChanges");
            _types = new List<SpellTypeModel>();
            foreach (var spellType in EnumsDictionary.SpellTypes.Where(s => s.Value != SpellType.All))
            {
                _types.Add(new SpellTypeModel(owner, this, spellType.Name, spellType.Value, owner.SpellTypes.Contains(spellType.Value)));
            }
        }
    }
}
