using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class RepoSpellsViewModel : ViewModelBase
    {
        public bool SelectMode { get; set; }

        public string SearchName
        {
            get { return _searchName; }
            set { _searchName = value; PropertyHasChanged("SearchName"); FillList(); }
        }

        public int? SearchLevel
        {
            get { return _searchLevel; }
            set { _searchLevel = value; PropertyHasChanged("SearchLevel"); FillList(); }
        }

        public SpellType SearchType
        {
            get { return _searchType; }
            set { _searchType = value; PropertyHasChanged("SearchType"); FillList(); }
        }

        public SpellDefinition SelectedSpell
        {
            get { return _selectedSpell; }
            set
            {
                _selectedSpell = value; 
                PropertyHasChanged("SelectedSpell");
                if (_selectedSpell != null)
                {
                    SpellDetails = new SpellDefinitionViewModel(_selectedSpell);
                }
            }
        }

        private SpellDefinition _knownSelectedSpell;
        public SpellDefinition KnownSelectedSpell
        {
            get { return _knownSelectedSpell; }
            set
            {
                _knownSelectedSpell = value;
                PropertyHasChanged("KnownSelectedSpell");
            }
        }

        public ObservableCollection<SpellDefinition> Spells { get; set; }
        public ObservableCollection<SpellDefinition> SelectedSpells
        {
            get
            {
                if (_owner != null)
                {
                    return new ObservableCollection<SpellDefinition>(_owner.KnownSpells);
                }
                return null;
            }
        }

        private SpellDefinitionViewModel _spellDetails;
        public SpellDefinitionViewModel SpellDetails
        {
            get { return _spellDetails; }
            set
            {
                _spellDetails = value;
                PropertyHasChanged("SpellDetails");
            }
        }

        public ICommand AddSpell { get; private set; }
        public ICommand RemoveSpell { get; private set; }
        public ICommand SelectSpell { get; private set; }
        public ICommand UnSelectSpell { get; private set; }
        public ICommand Close { get; private set; }
        

        private string _searchName;
        private int? _searchLevel;
        private SpellType _searchType;
        private SpellDefinition _selectedSpell;
        private readonly Character _owner;

        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);
        private readonly Lazy<IAppAPI> _appAPI = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);

        public RepoSpellsViewModel(bool selectMode = false, Character character = null)
        {
            SelectMode = selectMode;
            _owner = character;
            FillList();
            AddSpell = new Command((o) =>
                                       {
                                           var newSpellDef = new SpellDefinition { Name = "(nowy czar)" };
                                           _appAPI.Value.HandleOperationResult(_appFacade.Value.AddSpell(newSpellDef));
                                           SelectedSpell = newSpellDef;
                                       });
            RemoveSpell = new Command((o) =>
                                          {
                                              _appAPI.Value.HandleOperationResult(_appFacade.Value.RemoveSpell(SelectedSpell));
                                              SelectedSpell = null;
                                          }, this, () => SelectedSpell != null, "SelectedSpell");
            SelectSpell = new Command((o) =>
                                          {
                                              _appAPI.Value.HandleOperationResult(_owner.Controller.AddKnownSpell(SelectedSpell));
                                              PropertyHasChanged("SelectedSpells");
                                          }, this, () => SelectMode && SelectedSpell != null, "SelectedSpell");
            UnSelectSpell = new Command((o) =>
                                            {
                                                _owner.KnownSpellsNames.Remove(KnownSelectedSpell.Name);
                                                PropertyHasChanged("SelectedSpells");
                                            }, this, () => SelectMode && KnownSelectedSpell != null, "KnownSelectedSpell");

            Close = new Command((o) => CommandHasExecuted("Close", OperationResult.Success()));
        }

        private void FillList()
        {
            Spells = new ObservableCollection<SpellDefinition>(ServiceContainer.GetInstance<AppFacade>().RepoSpells.Where(s =>
                (_searchName == null || s.Name.ToLower().StartsWith(_searchName.ToLower())) &&
                (_searchType == SpellType.All || s.SpellTypes.Contains(_searchType)) &&
                (_searchLevel == null || _searchLevel == 0 || s.Level == _searchLevel)
                ).OrderBy(o => o.Name));
            PropertyHasChanged("Spells");
        }
    }
}
