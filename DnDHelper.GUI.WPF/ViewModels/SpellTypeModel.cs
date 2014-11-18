using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class SpellTypeModel
    {
        private readonly SpellDefinition _owner;
        private readonly SpellDefinitionViewModel _parent;
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; } 
            set
            {
                _isChecked = value;
                if (value && !_owner.SpellTypes.Contains(Type))
                {
                    _owner.SpellTypes.Add(Type);
                }
                else
                {
                    _owner.SpellTypes.Remove(Type);
                }
                _parent.PendingChanges = true;
            }
        }
        public string Name { get; set; }
        public Domain.SpellType Type { get; set; }

        public SpellTypeModel(SpellDefinition owner, SpellDefinitionViewModel parent, string name, Domain.SpellType type, bool isChecked)
        {
            _owner = owner;
            Name = name;
            Type = type;
            _isChecked = isChecked;
            _parent = parent;
        }
    }
}