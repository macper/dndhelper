using DnDHelper.GUI.WPF.ViewModels;
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

namespace DnDHelper.GUI.WPF.PopUps
{
    /// <summary>
    /// Interaction logic for AddExperience.xaml
    /// </summary>
    public partial class AddExperience : Window
    {
        public AddExperience(AddExperienceViewModel model)
        {
            InitializeComponent();
            DataContext = model;
            model.CommandExecuted += (o, e) =>
            {
                if (e.CommandName == "Commit")
                {
                    DialogResult = true;
                    Close();
                }
            };
            this.Activated += (o, e) =>
            {
                Name.Focus();
            };
        }
    }
}
