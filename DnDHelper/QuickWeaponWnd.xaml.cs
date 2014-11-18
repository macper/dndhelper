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

namespace DnDHelper.WPF
{
    /// <summary>
    /// Interaction logic for QuickWeaponWnd.xaml
    /// </summary>
    public partial class QuickWeaponWnd : Window
    {
        Item _item;
        public QuickWeaponWnd(Item it)
        {
            if (it == null)
            {
                Close();
                return;
            }
            InitializeComponent();
            _item = it;
            Title = it.Name;
            DataContext = _item;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
