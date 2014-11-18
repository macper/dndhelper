using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class MoveItemViewModel : ViewModelBase
    {
        public ObservableCollection<Character> Characters { get; set; }

        public Character Source { get; set; }

        public EquipItem Item { get; set; }

        public Character SelectedCharacter
        {
            get { return _selectedCharacter; }
            set { _selectedCharacter = value; PropertyHasChanged("SelectedCharacter"); }
        }

        private Character _selectedCharacter;

        public ICommand Confirm { get; set; }

        public MoveItemViewModel(IEnumerable<Character> characters, Character source, EquipItem item)
        {
            Characters = new ObservableCollection<Character>(characters);
            Source = source;
            Item = item;

            Confirm = new Command((o) =>
            {
                var facade = ServiceContainer.GetInstance<AppFacade>();
                Source.Controller.RemoveItem(Item);
                SelectedCharacter.Controller.EquipItem(Item.Item, ItemPosition.Backpack);
                facade.CharacterChange(Source);
                CommandHasExecuted("Confirm", OperationResult.Success());
            });
        }
    }
}
