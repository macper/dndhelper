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

namespace DnDHelper.GUI.WPF.PopUps
{
    /// <summary>
    /// Interaction logic for AddGroup.xaml
    /// </summary>
    public partial class AddGroup : Window
    {
        public AddGroup(ViewModels.AddGroupViewModel model)
        {
            InitializeComponent();
            DataContext = model;
            model.CommandExecuted += (s, e) =>
                                         {
                                             if (e.Result.Result == OperationResultType.Error)
                                             {
                                                 MessageBox.Show("Wystąpił błąd: " + e.Result.Message, "Błąd",
                                                                 MessageBoxButton.OK, MessageBoxImage.Error);
                                                 return;
                                             }
                                             DialogResult = true;
                                             Close();
                                         };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBox1.Focus();
        }
    }
}
