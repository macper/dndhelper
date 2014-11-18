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
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF.PopUps
{
    /// <summary>
    /// Interaction logic for RepoEffects.xaml
    /// </summary>
    public partial class RepoEffects : Window
    {
        public RepoEffects(RepoEffectsViewModel model)
        {
            InitializeComponent();
            DataContext = model;
            model.CommandExecuted += (o, e) =>
                                         {
                                             if (e.CommandName != "CommitSelect")
                                                 return;
                                             DialogResult = true;
                                             Close();
                                         };
        }
    }
}
