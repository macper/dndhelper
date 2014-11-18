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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DnDHelper.Domain;
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF.Panels
{
    /// <summary>
    /// Interaction logic for Battle.xaml
    /// </summary>
    public partial class Battle : UserControl
    {
        public Battle()
        {
            InitializeComponent();
        }

        private void ListView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                ServiceContainer.GetInstance<IAppAPI>().ExecuteGlobalCommand(GlobalCommands.SetAttackAsDone, null);
        }
    }
}
