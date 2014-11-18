using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class CharacterTabSpellsModel : CharacterTabBaseModel
    {
        private readonly Character _owner;

        public ObservableCollection<SpellDefinition> KnownSpells
        {
            get
            {
                return new ObservableCollection<SpellDefinition>(_owner.KnownSpells.OrderBy(o => o.Level));
            }
        }

        private SpellDefinition _selectedKnownSpell;
        public SpellDefinition SelectedKnownSpell
        {
            get { return _selectedKnownSpell; }
            set { 
                _selectedKnownSpell = value; 
                PropertyHasChanged("SelectedKnownSpell");
                SpellDetails = value;
            }
        }

        private SpellDefinition _spellDetails;
        public SpellDefinition SpellDetails
        {
            get { return _spellDetails; }
            set { _spellDetails = value; PropertyHasChanged("SpellDetails"); }
        }

        public ObservableCollection<Spell> Spells
        {
            get
            {
                return new ObservableCollection<Spell>(_owner.Spells.Where(s => !s.IsCasted).OrderBy(o => o.Definition.Level));
            }
        }

        public ObservableCollection<Spell> SpellsCasted
        {
            get
            {
                return new ObservableCollection<Spell>(_owner.Spells.Where(s => s.IsCasted).OrderBy(o => o.Definition.Level));
            }
        }

        private Spell _selectedSpell;
        public Spell SelectedSpell
        {
            get { return _selectedSpell; }
            set
            {
                _selectedSpell = value; 
                PropertyHasChanged("SelectedSpell"); 
                if (value != null)
                    SpellDetails = value.Definition;
            }
        }

        public ObservableCollection<SpellCasting> SpellCastings
        {
            get
            {
                return new ObservableCollection<SpellCasting>(_owner.AvailableCastings);
            }
        }

        private SpellCasting _selectedCasting;
        public SpellCasting SelectedCasting
        {
            get { return _selectedCasting; }
            set { _selectedCasting = value; PropertyHasChanged("SelectedCasting"); }
        }

        public ICommand ChangeKnownSpells { get; private set; }
        public ICommand CastSpell { get; private set; }
        public ICommand ResetSpells { get; private set; }
        public ICommand RemoveSpell { get; private set; }
        public ICommand AddCasting { get; private set; }
        public ICommand RemoveCasting { get; private set; }
        public ICommand PrepareSpell { get; private set; }
        

        private Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);
        private readonly Lazy<IAppAPI> _appAPI = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);

        public CharacterTabSpellsModel(Character owner)
            : base(owner)
        {
            _owner = owner;
            ChangeKnownSpells = new Command((o) => _appAPI.Value.RedirectToViewModel(new RepoSpellsViewModel(true, owner), () => PropertyHasChanged("KnownSpells")));
            CastSpell = new Command((o) =>
                                        {
                                            _appAPI.Value.HandleOperationResult(_owner.Controller.CastSpell(SelectedSpell));
                                            PropertyHasChanged("Spells");
                                            PropertyHasChanged("SpellsCasted");
                                        }, this, () => SelectedSpell != null && !SelectedSpell.IsCasted, "SelectedSpell");
            ResetSpells = new Command((o) =>
                                          {
                                              _owner.Controller.ResetSpells();
                                              PropertyHasChanged("Spells");
                                              PropertyHasChanged("SpellsCasted");
                                          });
            AddCasting = new Command((o) =>
                                         {
                                             if (_owner.AvailableCastings == null)
                                                 _owner.AvailableCastings = new List<SpellCasting>();
                                             _owner.AvailableCastings.Add(new SpellCasting() { Count = 1, Level = 0, Type = SpellType.Bard });
                                             PropertyHasChanged("SpellCastings");
                                         });
            RemoveCasting = new Command((o) =>
                                            {
                                                _owner.AvailableCastings.Remove(SelectedCasting);
                                                PropertyHasChanged("SpellCastings");
                                            }, this, () => SelectedCasting != null, "SelectedCasting");
            PrepareSpell = new Command((o) =>
                                           {
                                               _appAPI.Value.HandleOperationResult(_owner.Controller.AddSpell(SelectedKnownSpell));
                                               PropertyHasChanged("Spells");
                                           }, this, () => SelectedKnownSpell != null, "SelectedKnownSpell");
            RemoveSpell = new Command((o) =>
                                          {
                                              _owner.Spells.Remove(SelectedSpell);
                                              PropertyHasChanged("Spells");
                                              PropertyHasChanged("SpellsCasted");
                                          }, this, () => SelectedSpell != null, "SelectedSpell");

        }
    }
}
