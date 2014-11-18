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
using DnDHelper.Domain;
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF.PopUps
{
    /// <summary>
    /// Interaction logic for CopyEffect.xaml
    /// </summary>
    public partial class CopyEffect : Window
    {
        public CopyEffect(CopyEffectViewModel model)
        {
            DataContext = model;
            InitializeComponent();
            model.CommandExecuted += (o, e) =>
            {
                if (e.Result.Result == OperationResultType.Success)
                {
                    DialogResult = true;
                    Close();
                }
            };
        }
    }
}
