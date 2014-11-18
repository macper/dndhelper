using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DnDHelper.Domain;
using System.Linq;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class RepoItemsViewModel : ViewModelBase
    {
        private BaseTypes _searchType;
        public BaseTypes SearchType { get { return _searchType; } 
        set
            {
                _searchType = value;
                FillList();
                PropertyHasChanged("SearchType");
                PropertyHasChanged("Items");
            }
        }

        private string _searchName;
        public string SearchName 
        { 
            get { return _searchName; }
            set
            {
                _searchName = value;
                FillList();
                PropertyHasChanged("SearchName");
                PropertyHasChanged("Items");
            } 
        }

        private string _prototype;
        public string Prototype
        {
            get { return _prototype; }
            set
            {
                _prototype = value;
                FillList();
                PropertyHasChanged( "Prototype" );
                PropertyHasChanged( "Items" );
            }
        }

        private int _priceMax;
        public int PriceMax
        {
            get
            {
                return _priceMax;
            }
            set
            {
                _priceMax = value;
                FillList();
                PropertyHasChanged( "PriceMax" );
                PropertyHasChanged( "Items" );
            }
        }

        public List<ItemDefinition> Items { get; set; }

        public List<string> Prototypes { get; set; }

        private ItemDefinition _selectedItem;
        public ItemDefinition SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                PropertyHasChanged("SelectedItem");
                if (_selectedItem != null)
                ItemDetails = new ItemDefinitionViewModel(_selectedItem);
            }
        }

        private ItemDefinitionViewModel _itemDetails;
        public ItemDefinitionViewModel ItemDetails
        {
            get { return _itemDetails; }
            set { _itemDetails = value;
            PropertyHasChanged("ItemDetails");}
        }

        public ICommand AddItem { get; set; }
        public ICommand CopyItem { get; set; }
        public ICommand RemoveItem { get; set; }

        public bool SelectMode { get; set; }
        public ICommand SelectItem { get; set; }
        public ICommand RandomItem { get; set; }

        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);
        private readonly Lazy<IAppAPI> _appAPI = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);


        public RepoItemsViewModel()
        {
            Prototypes = _appFacade.Value.GetItemPrototypes().Select(s => s.Name).ToList();
            AddItem = new Command((o) =>
            {
                SelectedItem = new ItemDefinition() {Name = "Nowy przedmiot"};
                ItemDetails.InsertMode = true;
            });

            CopyItem = new Command((o) =>
            {
                SelectedItem = _appFacade.Value.GetItemCopy(SelectedItem);
                ItemDetails.InsertMode = true;
            }, this, () => SelectedItem != null, "SelectedItem");

            RemoveItem = new Command(
                    (o) =>
                    _appAPI.Value.RedirectToViewModel(new ConfirmationViewModel("Czy na pewno chcesz usun¹æ ten przedmiot ?",
                                               () => _appFacade.Value.RemoveItem(SelectedItem))),
                    this, () => SelectedItem != null, "SelectedItem");

            SelectItem = new Command((o) =>
                CommandHasExecuted("SelectItem", OperationResult.Success()), this, () => SelectedItem != null, "SelectedItem");

            RandomItem = new Command( ( o ) =>
            {
                var rnd = new Random();
                SelectedItem = Items[rnd.Next( 0, Items.Count - 1 )];
            } );
            FillList();
        }

        private void FillList()
        {
            Items = ServiceContainer.GetInstance<AppFacade>().RepoItems.Where(
                    i => i.Name != null && ((string.IsNullOrEmpty(SearchName) || i.Name.ToUpper().Contains(SearchName.ToUpper())) && 
                        (SearchType == BaseTypes.None || i.BaseType == SearchType))
                        && ( Prototype == null || i.PrototypeName == Prototype )
                        && (PriceMax == 0 || i.Cost <= PriceMax )
                        )
                    .ToList();
        }
    }
}