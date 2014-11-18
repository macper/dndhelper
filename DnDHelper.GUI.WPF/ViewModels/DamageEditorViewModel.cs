using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class DamageEditorViewModel : ViewModelBase
    {
        private readonly Damage _owner;

        public DamageEditorViewModel(Damage owner)
        {
            _owner = owner;
            AddDamage = new Command((o) =>
                                        {
                                            var bone = new DamageBoneModel { Count = 1, DamageType = DamageTypes.Physical, MaxValue = 1 };
                                            bone.CommandExecuted += (s, e) => Save();
                                            Damages.Add(bone);
                                            PropertyHasChanged("Damages");
                                            Save();
                                        });
            RemoveDamage = new Command((o) =>
                                           {
                                               Damages.Remove(SelectedElement);
                                               PropertyHasChanged("Damages");
                                               Save();
                                           }, this, () => SelectedElement != null, "SelectedElement");

            Damages = new ObservableCollection<DamageBoneModel>(owner.Elements.Select(e =>
                                                                                          {
                                                                                              var bone = new DamageBoneModel
                                                                                               {
                                                                                                   Count = e.Count,
                                                                                                   DamageType = e.DamageType,
                                                                                                   MaxValue = e.MaxValue
                                                                                               };
                                                                                              bone.CommandExecuted += (s, ev) => Save();
                                                                                              return bone;
                                                                                          }));

        }

        public ObservableCollection<DamageBoneModel> Damages { get; set; }
        private DamageBoneModel _selectedElement;
        public DamageBoneModel SelectedElement
        {
            get { return _selectedElement; }
            set
            {
                _selectedElement = value;
                PropertyHasChanged("SelectedElement");
            }
        }

        public ICommand AddDamage { get; private set; }
        public ICommand RemoveDamage { get; private set; }
        
        public void Save()
        {
            _owner.Elements.Clear();
            foreach (var damageBoneModel in Damages)
            {
                _owner.Elements.Add(new DamageBone(damageBoneModel.MaxValue, damageBoneModel.Count, damageBoneModel.DamageType));
            }
        }
        
    }
}
