using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class MoveCharacterViewModel : ViewModelBase
    {
        public IEnumerable<CharacterGroup> Groups { get; set; }
        public Character Character { get; set; }
        public CharacterGroup SourceGroup { get; set; }
        public CharacterGroup DestinationGroup { get; set; }
        public ICommand Save { get; set; }

        public MoveCharacterViewModel(IEnumerable<CharacterGroup> groups, Character character)
        {
            Groups = groups;
            Character = character;
            SourceGroup = Groups.Single(g => g.Characters.Contains(character));
            Save = new Command((o) =>
            {
                var result = ServiceContainer.GetInstance<AppFacade>().MoveCharacter(Character, DestinationGroup);
                CommandHasExecuted("Save", result);
            });
        }
    }
}
