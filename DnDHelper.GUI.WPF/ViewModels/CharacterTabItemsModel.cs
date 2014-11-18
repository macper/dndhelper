using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class CharacterTabItemsModel : CharacterTabBaseModel
    {
        public int Gold
        {
            get { return Character.Gold; }
            set { Character.Gold = value; PropertyHasChanged("Gold"); _appFacade.Value.CharacterChange(Character); }
        }

        public string Others
        {
            get { return Character.OtherItems; }
            set { Character.OtherItems = value; PropertyHasChanged("Others"); _appFacade.Value.CharacterChange(Character); }
        }

        public EquipItem RightHand
        {
            get { return GetItem(ItemPosition.RightHand); }
        }

        public EquipItem LeftHand
        {
            get { return GetItem(ItemPosition.LeftHand); }
        }

        public EquipItem Torso
        {
            get { return GetItem(ItemPosition.Torso); }
        }

        public EquipItem Head
        {
            get { return GetItem(ItemPosition.Head); }
        }

        public EquipItem Neck
        {
            get { return GetItem(ItemPosition.Neck); }
        }

        public EquipItem Gloves
        {
            get { return GetItem(ItemPosition.Gloves); }
        }

        public EquipItem Boots
        {
            get { return GetItem(ItemPosition.Boots); }
        }

        public EquipItem Cloak
        {
            get { return GetItem(ItemPosition.Cloak); }
        }

        public EquipItem Belt
        {
            get { return GetItem(ItemPosition.Belt); }
        }

        public IEnumerable<EquipItem> Finger
        {
            get { return GetItems(ItemPosition.Finger); }
        }

        private EquipItem _selectedRing;
        public EquipItem SelectedRing
        {
            get { return _selectedRing; }
            set { _selectedRing = value; PropertyHasChanged("SelectedRing"); }
        }

        public IEnumerable<EquipItem> Arrow
        {
            get
            {
                return GetItems(ItemPosition.Arrow);
            }
        }

        private EquipItem _selectedArrow;
        public EquipItem SelectedArrow
        {
            get { return _selectedArrow; }
            set { _selectedArrow = value; PropertyHasChanged("SelectedArrow"); }
        }

        private EquipItem _selectedPotion;
        public EquipItem SelectedPotion
        {
            get { return _selectedPotion; }
            set { _selectedPotion = value; PropertyHasChanged("SelectedPotion"); }
        }

        public IEnumerable<EquipItem> Potion
        {
            get { return GetItems(ItemPosition.Potion); }
        }

        private EquipItem _selectedStaff;
        public EquipItem SelectedStaff
        {
            get { return _selectedStaff; }
            set { _selectedStaff = value; PropertyHasChanged("SelectedStaff"); }
        }
        public IEnumerable<EquipItem> Staff
        {
            get { return GetItems(ItemPosition.Staff); }
        }

        public IEnumerable<EquipItem> Backpack
        {
            get { return GetItems(ItemPosition.Backpack); }
        }

        private EquipItem _currentBackpack;
        public EquipItem CurrentBackpack { get { return _currentBackpack; } 
            set
            {
                _currentBackpack = value;
                PropertyHasChanged("CurrentBackpack");
            } 
        }

        public ICommand ShowItem { get; private set; }

        public ICommand MoveItem { get; private set; }
        public ICommand SetNewItem { get; private set; }
        public ICommand MoveItemToBackpack { get; private set; }
        public ICommand RemoveItem { get; private set; }
        public ICommand EquipItem { get; private set; }
        public ICommand RemoveItemFromBackpack { get; private set; }
        public ICommand MoveSpecificItemToBackpack { get; private set; }
        public ICommand EquipItemChange { get; set; }

        


        public void NotifyItemChanged(ItemPosition itemPosition)
        {
            PropertyHasChanged(itemPosition.ToString());
            _appFacade.Value.CharacterChange(Character);
        }

        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);
        private readonly Lazy<IAppAPI> _appAPI = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);

        private EquipItem GetItem(ItemPosition position)
        {
            var item = Character.MainItems.SingleOrDefault(i => i.Position == position);
            if (item == null)
            {
                return new EquipItem(position, new Item() { Name = "(brak)" });
            }
            return item;
        }

        private IEnumerable<EquipItem> GetItems(ItemPosition position)
        {
            var items = Character.MainItems.Where(i => i.Position == position);
            if (!items.Any())
            {
                return new[] {new EquipItem(position, new Item() {Name = "(brak)"})};
            }
            return items;
        }

        public CharacterTabItemsModel(Character character)
            : base(character)
        {
            ShowItem = new Command((o) =>
                                       {
                                           if (o == null)
                                               return;
                                           
                                           var item = (Item) o;
                                           _appAPI.Value.ShowItem(item);
                                       });

            SetNewItem = new Command((o) =>
                                         {
                                             if (o == null)
                                                 o = ItemPosition.Backpack;
                                             var itemPosition = (ItemPosition)o;
                                             _appAPI.Value.GetItemFromRepo(itemPosition, (item) =>
                                                                                             {
                                                                                                 _appAPI.Value.HandleOperationResult(Character.Controller.EquipItem(item, itemPosition));
                                                                                                 NotifyItemChanged(itemPosition);
                                                                                             });
                                         });

            MoveItemToBackpack = new Command((o) =>
                                                 {
                                                     var itemPosition = (ItemPosition)o;
                                                     _appAPI.Value.HandleOperationResult(Character.Controller.UnEquipItem(itemPosition), () =>
                                                                                                                                             {
                                                                                                                                                 NotifyItemChanged(itemPosition); 
                                                                                                                                                 NotifyItemChanged(ItemPosition.Backpack);
                                                                                                                                             });
                                                 });

            MoveSpecificItemToBackpack = new Command((o) =>
                                                         {
                                                             var item = (EquipItem)o;
                                                             _appAPI.Value.HandleOperationResult(Character.Controller.UnEquipItem(item), () =>
                                                                                                                                             {
                                                                                                                                                 NotifyItemChanged(item.Position);
                                                                                                                                                 NotifyItemChanged(ItemPosition.Backpack);
                                                                                                                                             });
                                                         });

            RemoveItem = new Command((o) =>
                                         {
                                             var item = (EquipItem)o;
                                             if (item == null) return;
                                             _appAPI.Value.HandleOperationResult(Character.Controller.RemoveItem(item), () => NotifyItemChanged(item.Position));
                                         });
            EquipItem = new Command((o) =>
                                        {
                                            var item = o as Item;
                                            if (o == null)
                                            {
                                                _appAPI.Value.HandleOperationResult(OperationResult.Error("Nie wybrano przedmiotu!"));
                                                return;
                                            }
                                            _appAPI.Value.HandleOperationResult(Character.Controller.EquipItem(item), () =>
                                                                                                                          {
                                                                                                                              NotifyItemChanged(Character.MainItems.Single(s => s.Item == item).Position);
                                                                                                                              NotifyItemChanged(ItemPosition.Backpack);
                                                                                                                          });
                                        });

            RemoveItemFromBackpack = new Command((o) =>
                                                     {
                                                         var item = o as EquipItem;
                                                         _appAPI.Value.HandleOperationResult(
                                                             Character.Controller.RemoveItem(item),
                                                             () => NotifyItemChanged(ItemPosition.Backpack));
                                                     });

            EquipItemChange = new Command((o) => _appFacade.Value.CharacterChange(Character));

            MoveItem = new Command((o) =>
            {
                var item = (EquipItem) o;
                _appAPI.Value.RedirectToViewModel(new MoveItemViewModel(_appFacade.Value.RepoGroups.First(g => g.Name == Character.GroupName).Characters.Where(m => m.Name != Character.Name), Character, item), () => NotifyItemChanged(ItemPosition.Backpack));
            });

            SelectedArrow = Arrow.First();
            SelectedPotion = Potion.First();
            SelectedStaff = Staff.First();
        }
    }
}
