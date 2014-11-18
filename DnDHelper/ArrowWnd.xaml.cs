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
    /// Interaction logic for ArrowWnd.xaml
    /// </summary>
    public partial class ArrowWnd : Window
    {
        BaseTypes _type;
        List<Item> _list;
        Helper _helper;

        public ArrowWnd(BaseTypes baseType, List<Item> list, Helper helper)
        {
            InitializeComponent();
            _type = baseType;
            Title = "Lista dla: " + _type.ToString();
            _list = list;
            dataGrid1.ItemsSource = _list;
            _helper = helper;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, true, _type);
            if (itWnd.ShowDialog() == true)
            {
                _list.Add(itWnd.SelectedItem);
            }
            dataGrid1.ItemsSource = _list;
            dataGrid1.Items.Refresh();
        }

        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                Item it = dataGrid1.SelectedItem as Item;
                if (it != null)
                {
                    _list.Remove(it);
                }
            }
            dataGrid1.ItemsSource = _list;
            dataGrid1.Items.Refresh();
        }
    }
}
