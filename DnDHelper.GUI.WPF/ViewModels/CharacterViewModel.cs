using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class CharacterViewModel : ViewModelBase
    {
        public ICommand ChangeTab { get; set; }
        public Character SelectedCharacter { get; set; }
        private Dictionary<string, CharacterTabBaseModel> _tabsViewModels;
        public bool IsVisible { get { return SelectedCharacter != null; } }
        public BattleCharacter BattleCharacter { get; set; }
        
        

        public CharacterTabBaseModel ActiveTab
        {
            get { return _activeTab; }
            set { _activeTab = value; PropertyHasChanged("ActiveTab"); }
        }

        private CharacterTabBaseModel _activeTab;

        public CharacterViewModel(Character character)
        {
            SelectedCharacter = character;
            if (character == null)
                return;

            if (!character.Bonuses.Any())
                ServiceContainer.GetInstance<AppFacade>().Calculate(character);

            var api = ServiceContainer.GetInstance<IAppAPI>();
            ChangeTab = new Command((o) =>
            {
                api = ServiceContainer.GetInstance<IAppAPI>();
                var name = o as string;
                if (name == null || !_tabsViewModels.ContainsKey(name))
                {
                    api.HandleOperationResult(OperationResult.Error("Nie zarejestrowano takiej zak³adki: " + name));
                    return;
                }
                ActiveTab = _tabsViewModels[name];
                api.SetGlobalVariable(GlobalVariables.ActiveCharacterTab, name);
            });

            _tabsViewModels = new Dictionary<string, CharacterTabBaseModel>();

            _tabsViewModels.Add(TabNames.Stats, new CharacterTabStatsModel(character));
            _tabsViewModels.Add(TabNames.Items, new CharacterTabItemsModel(character));
            _tabsViewModels.Add(TabNames.Spells, new CharacterTabSpellsModel(character));
            _tabsViewModels.Add(TabNames.Effects, new CharacterTabEffectsModel(character));
            _tabsViewModels.Add(TabNames.Skills, new CharacterTabSkillsModel(character));
            _tabsViewModels.Add(TabNames.Others, new CharacterTabOthersModel(character));
            _tabsViewModels.Add(TabNames.Bonuses, new CharacterTabBonusesModel(character));
            var savedTab = api.GetGlobalVariable<string>(GlobalVariables.ActiveCharacterTab);
            if (savedTab != null)
            {
                ChangeTab.Execute(savedTab);
            }
            else if (ChangeTab.CanExecute(TabNames.Stats))
                ChangeTab.Execute(TabNames.Stats);

            
            api.RegisterGlobalCommand(ChangeTab, GlobalCommands.ChangeCharacterTab, () => api.GetGlobalVariable<string>(GlobalVariables.MainTab) == MainViewModel.TabNames.Char || api.GetGlobalVariable<string>(GlobalVariables.MainTab) == MainViewModel.TabNames.Battle);
            PropertyHasChanged("IsVisible");
        }

        public CharacterViewModel(BattleCharacter battleCharacter) : this(battleCharacter == null ? null : battleCharacter.Character)
        {
            BattleCharacter = battleCharacter;
            foreach (var value in _tabsViewModels.Values)
            {
                value.BattleCharacter = battleCharacter;
                
            }
        }
        
        

        public static class TabNames
        {
            public static readonly string Stats = "Stats";
            public static readonly string Items = "Items";
            public static readonly string Spells = "Spells";
            public static readonly string Effects = "Effects";
            public static readonly string Skills = "Skills";
            public static readonly string Others = "Others";
            public static readonly string Bonuses = "Bonuses";
        }
    }


}