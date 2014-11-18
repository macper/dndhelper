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
    /// Interaction logic for GetVariable.xaml
    /// </summary>
    public partial class GetVariable : Window
    {
        public object Variable { get; set; }
        private readonly ExpectedVariableTypes _type;

        public GetVariable(string message, ExpectedVariableTypes type)
        {
            InitializeComponent();
            label1.Content = message;
            textBox1.Focus();
            _type = type;
        }

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Proszę podać wartość");
                return;
            }
            if (_type == ExpectedVariableTypes.Int)
            {
                int tmp;
                if (!int.TryParse(textBox1.Text, out tmp))
                {
                    MessageBox.Show("Błąd - oczekiwana liczba całkowita");
                    return;
                }
                Variable = tmp;
            }
            else if (_type == ExpectedVariableTypes.Double)
            {
                double tmp;
                if (!double.TryParse(textBox1.Text, out tmp))
                {
                    MessageBox.Show("Nieprawidłowa wartość - oczekiwana: liczba");
                    return;
                }
                Variable = tmp;
            }
            else
            {
                Variable = textBox1.Text;
            }
            DialogResult = true;
            Close();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button1Click(this, null);
            }
        }
    }

    public enum ExpectedVariableTypes { String, Int, Double }
}
