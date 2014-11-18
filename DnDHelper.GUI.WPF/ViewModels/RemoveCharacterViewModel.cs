using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class RemoveCharacterViewModel : ViewModelBase
    {
        public Character Character { get; private set; }
        public ICommand Commit { get; private set; }

        public RemoveCharacterViewModel(Character character)
        {
            Character = character;
            Commit = new Command((o) => CommandHasExecuted("Commit", ServiceContainer.GetInstance<AppFacade>().RemoveCharacter(Character)));
        }
    }
}