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
using DnDHelper.Domain;
using DnDHelper.Domain.Bonuses;
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF.Controls
{
    /// <summary>
    /// Interaction logic for BonusHelper.xaml
    /// </summary>
    public partial class BonusHelper : UserControl
    {
        public static DependencyProperty ValueProperty = DependencyProperty.Register(
            "Bonuses", typeof(IEnumerable<BaseBonus>), typeof(BonusHelper));


        public IEnumerable<BaseBonus> Bonuses
        {
            get { return (IEnumerable<BaseBonus>)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public bool NoBonuses
        {
            get { return Bonuses == null || !Bonuses.Any(); }
        }

        public bool FullDescriptionMode { get; set; }

        public BonusHelper()
        {
            InitializeComponent();
        }

    }
}
