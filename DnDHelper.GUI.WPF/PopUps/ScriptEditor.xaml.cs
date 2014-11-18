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
    /// Interaction logic for ScriptEditor.xaml
    /// </summary>
    public partial class ScriptEditor : Window
    {
        public ScriptEditor(ScriptEditorViewModel model)
        {
            InitializeComponent();
            DataContext = model;
            model.CommandExecuted += (o, e) =>
                                         {
                                             if (e.CommandName == "Close")
                                             {
                                                 DialogResult = true;
                                                 Close();
                                             }
                                         };
        }
    }
}
