﻿using System;
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
    /// Interaction logic for AddClass.xaml
    /// </summary>
    public partial class AddClass : Window
    {
        public AddClass(AddClassViewModel model)
        {
            InitializeComponent();
            DataContext = model;
            model.CommandExecuted += (o, e) =>
            {
                if (e.Result.Result == OperationResultType.Error)
                {
                    MessageBox.Show(e.Result.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                DialogResult = true;
                Close();
            };
        }
    }
}
