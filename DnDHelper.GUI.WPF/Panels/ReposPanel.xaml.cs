using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF.Panels
{
    /// <summary>
    /// Interaction logic for ReposPanel.xaml
    /// </summary>
    public partial class ReposPanel : UserControl
    {
        private readonly Dictionary<string, UserControl> _panels;

        public ReposPanel()
        {
            InitializeComponent();
            DataContext = new ReposViewModel();
            _panels = new Dictionary<string, UserControl>
                          {
                              {"Items", new Repos.Items()},
                              {"Spells", new Repos.Spells()},
                              {"Effects", new Repos.Effects()},
                              {"Skills", new Repos.Skills()},
                              {"Atutes", new Repos.Atutes()},
                              {"Races", new Repos.Races()},
                              {"Classes", new Repos.Classes()}
                          };
        }

        public void SetPanel(string name, ViewModelBase model)
        {
            if (!_panels.ContainsKey(name))
            {
                MessageBox.Show("Brak takiego panelu: " + name, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ContentPanel.Content = _panels[name];
            _panels[name].DataContext = model;
        }

    }
}
