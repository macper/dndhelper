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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF.Panels
{
    /// <summary>
    /// Interaction logic for TechPanel.xaml
    /// </summary>
    public partial class TechPanel : UserControl
    {
        public TechPanel()
        {
            InitializeComponent();
        }

        public void Log(string message, string logger, MessageType type)
        {
            var brush = new SolidColorBrush(Colors.Black);
            switch (type)
            {
                case MessageType.Debug:
                    brush = new SolidColorBrush(Colors.DarkGray);
                    break;

                case MessageType.Info:
                    brush = new SolidColorBrush(Colors.Black);
                    break;

                case MessageType.Error:
                    brush = new SolidColorBrush(Colors.Red);
                    break;

                case MessageType.Warning:
                    brush = new SolidColorBrush(Colors.Orange);
                    break;
            }
            var tr = new TextRange(LogEditor.Document.ContentEnd, LogEditor.Document.ContentEnd)
                         {
                             Text = string.Format("{0} : {1} : {2}{3}", DateTime.Now, logger, message, System.Environment.NewLine)
                         };
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
            LogEditor.ScrollToEnd();
        }
    }
}
