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

namespace DnDHelper.GUI.WPF.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AdditiveControl : UserControl
    {
        public static DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(int), typeof(AdditiveControl));

        public static DependencyProperty OnChangeProperty = DependencyProperty.Register("OnChange", typeof(ICommand), typeof(AdditiveControl));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value); 
                if (OnChange != null)
                {
                    OnChange.Execute(null);
                }
            }
        }

        public ICommand OnChange
        {
            get { return (ICommand) GetValue(OnChangeProperty); }
            set
            {
                SetValue(OnChangeProperty, value);
            }
        }

        public AdditiveControl()
        {
            InitializeComponent();
            
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            Value++;
        }

        private void SubtractiveClick(object sender, RoutedEventArgs e)
        {
            Value--;
        }

        private void MultiAddClick(object sender, MouseButtonEventArgs e)
        {
            var wnd = new PopUps.AdditiveControl();
            if (wnd.ShowDialog() == true)
            {
                Value += wnd.Value;
            }
        }

        private void MultiSubtractiveClick(object sender, MouseButtonEventArgs e)
        {
            var wnd = new PopUps.AdditiveControl();
            if (wnd.ShowDialog() == true)
            {
                Value -= wnd.Value;
            }
        }


    }
}
