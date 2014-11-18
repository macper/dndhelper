using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public abstract class CharacterTabBaseModel : ViewModelBase
    {
        public Character Character { get; private set; }
        public BattleCharacter BattleCharacter { get; set; }
        public bool BattleMode { get { return BattleCharacter != null; } }


        protected CharacterTabBaseModel(Character character)
        {
            Character = character;
        }
    }

    public enum CharacterTabContext { Groups, Battle }
}