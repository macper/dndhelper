using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using DnDHelper.Domain;
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF.PopUps
{
    /// <summary>
    /// Interaction logic for EditDamages.xaml
    /// </summary>
    public partial class EditDamages : Window, INotifyPropertyChanged
    {
        public ObservableCollection<DamageBoneModel> Damages
        {
            get;
            set;
        }

        private DamageBoneModel _selectedElement;
        public DamageBoneModel SelectedElement
        {
            get { return _selectedElement; }
            set
            {
                _selectedElement = value; if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs("SelectedElement")); } }

        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Commit { get; private set; }

        public EditDamages(Damage dmg)
        {
            InitializeComponent();
            DataContext = this;
            var damage = dmg ?? new Damage();
            Damages = new ObservableCollection<DamageBoneModel>();
            damage.Elements.ForEach(e => Damages.Add(new DamageBoneModel() { MaxValue = e.MaxValue, Count = e.Count, DamageType = e.DamageType }));

            Add = new Command((o) =>
                                  {
                                      Damages.Add(new DamageBoneModel(){ Count = 1, MaxValue = 1, DamageType = DamageTypes.Physical});
                                      if (PropertyChanged != null)
                                          PropertyChanged(this, new PropertyChangedEventArgs("Damages"));
                                  });
            Remove = new Command((o) =>
                                     {
                                         Damages.Remove(SelectedElement);
                                         if (PropertyChanged == null) return;
                                         PropertyChanged(this, new PropertyChangedEventArgs("Damages"));
                                         PropertyChanged(this, new PropertyChangedEventArgs("SelectedElement"));
                                     });
            Commit = new Command((o) =>
                                     {
                                         DialogResult = true;
                                         Close();
                                     });
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
