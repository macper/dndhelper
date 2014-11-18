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
    /// Interaction logic for CharacterTabSpells.xaml
    /// </summary>
    public partial class CharacterTabSpells : UserControl
    {
        private CharacterTabSpellsModel Model { get { return DataContext as CharacterTabSpellsModel; } }
        public CharacterTabSpells()
        {
            InitializeComponent();
        }

        private void SpellListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                return;

            if (Model.CastSpell.CanExecute(null))
                Model.CastSpell.Execute(null);
            e.Handled = true;
        }

        private void KnownSpellListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                return;

            if (Model.PrepareSpell.CanExecute(null))
                Model.PrepareSpell.Execute(null);

            e.Handled = true;
        }
    }
}
