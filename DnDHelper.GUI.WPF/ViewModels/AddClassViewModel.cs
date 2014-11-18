using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class AddClassViewModel : ViewModelBase
    {
        public Character CurrentCharacter { get; set; }
        public ClassDefinition SelectedClass { get; set; }
        public IEnumerable<ClassDefinition> Classes { get; set; }
        public int Level { get; set; }
        public ICommand Commit { get; set; }

        public AddClassViewModel(Character currentCharacter, IEnumerable<ClassDefinition> classDefinitions)
        {
            CurrentCharacter = currentCharacter;
            Classes = classDefinitions;
            Commit = new Command((o) =>
                                     {
                                         if (SelectedClass == null)
                                         {
                                             ServiceContainer.GetInstance<IAppAPI>().HandleOperationResult(
                                                 OperationResult.Error("Nie wybrano klasy"));
                                             return;
                                         }
                                         var newClass = SelectedClass.CreateItem();
                                         newClass.Level = Level;
                                         CurrentCharacter.Class.Add(newClass);
                                         ServiceContainer.GetInstance<AppFacade>().CharacterChange(CurrentCharacter);
                                         CommandHasExecuted("Commit", OperationResult.Success());
                                     });
        }
    }
}
