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
    /// Interaction logic for AddCalendarEntryWnd.xaml
    /// </summary>
    public partial class AddCalendarEntryWnd : Window
    {
        public CalendarEntry Entry { get; set; }

        public AddCalendarEntryWnd(DateTime time)
        {
            InitializeComponent();
            textBox1.Text = time.ToString();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Entry = new CalendarEntry(DateTime.Parse(textBox1.Text), textBox2.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return;
            }
            DialogResult = true;
            Close();
        }
    }
}
