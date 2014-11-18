using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DnDHelper.Domain;
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF.PopUps
{
    /// <summary>
    /// Interaction logic for MoveCharacter.xaml
    /// </summary>
    public partial class MoveCharacter : Window
    {
        public MoveCharacter(MoveCharacterViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.CommandExecuted += (s, e) =>
            {
                if (e.Result.Result == OperationResultType.Error)
                {
                    MessageBox.Show("Nie udało się przenieś postaci: " + e.Result.Message);
                    return;
                }
                DialogResult = true;
                Close();
            };
        }
    }
}
