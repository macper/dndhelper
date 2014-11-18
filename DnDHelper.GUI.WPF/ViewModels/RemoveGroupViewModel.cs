using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class RemoveGroupViewModel : ViewModelBase
    {
        public CharacterGroup Group { get; set; }
        public ICommand Commit { get; set; }
        public static string ConfirmationMessage { get { return "Czy na pewno chcesz usun�� t� grup� ? Usuni�cie grupy spowoduje kaskadowe usuni�cie wszystkich nale��cych do niej postaci!"; } }

        public RemoveGroupViewModel(CharacterGroup @group)
        {
            Group = group;
            Commit = new Command((o) =>
                                     {
                                         var result = ServiceContainer.GetInstance<AppFacade>().RemoveGroup(Group);
                                         CommandHasExecuted("Commit", result);
                                     });
        }
    }
}