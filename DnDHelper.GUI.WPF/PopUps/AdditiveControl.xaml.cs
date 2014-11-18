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
    /// Interaction logic for AdditiveControl.xaml
    /// </summary>
    public partial class AdditiveControl : Window
    {
        public int Value { get; set; }

        public AdditiveControl()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int val;
            if (!int.TryParse(textBox1.Text, out val))
            {
                MessageBox.Show("Nieprawidłowa wartość!");
                return;
            }
            Value = val;
            DialogResult = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                button1_Click(this, null);
            }
        }
    }
}
