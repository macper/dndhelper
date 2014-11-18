using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class AddGroupViewModel : ViewModelBase
    {
        public string GroupName { get; set; }
        public ICommand Save { get; set; }

        public AddGroupViewModel()
        {
            Save = new Command((o) =>
                                   {
                                       var result = ServiceContainer.GetInstance<AppFacade>().AddGroup(new CharacterGroup() { Name = GroupName });
                                       CommandHasExecuted("Save", result);
                                   });
        }
    }
}