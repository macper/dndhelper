using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class BattleViewModel : ViewModelBase
    {
        public Battle Battle { get { return Battle.Instance;}}

        public ObservableCollection<BattleCharacter> Members { get { return new ObservableCollection<BattleCharacter>(Battle.Members); }}

        public BattleCharacter ActiveMember { get { return Members.SingleOrDefault(s => s.IsActive); } }

        private BattleCharacter _selectedMember;
        public BattleCharacter SelectedMember { get { return _selectedMember; } 
            set 
            { 
                _selectedMember = value; 
                PropertyHasChanged("SelectedMember"); 
                PropertyHasChanged("Effects");
                PropertyHasChanged("CustomScripts");
                PropertyHasChanged("Missiles");
                PropertyHasChanged("Potions");
                PropertyHasChanged("Staffes");
                PropertyHasChanged( "BattleName" );
                if (SelectedMember == null)
                    return;
                _appApi.Value.ExecuteGlobalCommand(GlobalCommands.SetCharacter, SelectedMember);
                SelectedMemberModel = new CharacterViewModel(SelectedMember);
                EnvironmentModifier = 0;

            } 
        }

        private CharacterViewModel _selectedMemberModel;
        public CharacterViewModel SelectedMemberModel
        {
            get { return _selectedMemberModel; }
            set { _selectedMemberModel = value; PropertyHasChanged("SelectedMemberModel"); }
        }

        private BattleCharacter _attacker;
        private BattleCharacter _defender;

        public BattleCharacter Attacker
        {
            get { return _attacker; }
            set { _attacker = value; PropertyHasChanged("Attacker"); PerformAttack(); }
        }

        public BattleCharacter Defender
        {
            get { return _defender; }
            set { _defender = value; PropertyHasChanged("Defender"); PerformAttack(); }
        }

        public int Turn { get { return Battle.Turn; } }

        private int _environmentModifier;
        public int EnvironmentModifier { get { return _environmentModifier; } set { _environmentModifier = value; PropertyHasChanged("EnvironmentModifier"); PerformAttack(); } }
        public int Damage { get; set; }

        private AttackViewModel _selectedAttack;
        public AttackViewModel SelectedAttack { get { return _selectedAttack; } set { _selectedAttack = value; PropertyHasChanged("SelectedAttack"); } }

        private AttackInfoHeader _selectedAttackInfoHeader;
        public AttackInfoHeader SelectedAttackHeader { get { return _selectedAttackInfoHeader; } set { _selectedAttackInfoHeader = value; PropertyHasChanged("SelectedAttackHeader"); } }

        public ObservableCollection<Effect> GlobalEffects { get { return new ObservableCollection<Effect>(Battle.GlobalEffects);}}

        public AttackInfo AttackInfo { get; set; }
        public ObservableCollection<AttackInfoHeader> Attacks { get { return AttackInfo == null ? null : new ObservableCollection<AttackInfoHeader>(AttackInfo.AttacksHeaders.OrderBy(a => a.Header)); } }

        public ObservableCollection<Effect> Effects 
        { 
            get 
            {
                if( SelectedMember == null )
                    return null;

                return new ObservableCollection<Effect>(SelectedMember.Character.Effects); 
            } 
        }

        //public ObservableCollection<CustomScript> CustomScripts 
        //{ 
        //    get 
        //    {
        //        if( SelectedMember == null )
        //            return null;

        //        return new ObservableCollection<CustomScript>(SelectedMember.Character.CustomScripts); 
        //    } 
        //}

        private Effect _selectedEffect;
        public Effect SelectedEffect
        {
            get { return _selectedEffect; }
            set
            {
                _selectedEffect = value;
                PropertyHasChanged("SelectedEffect");
                if (value == null)
                    return;
            }
        }

        //private CustomScript _selectedScript;
        //public CustomScript SelectedScript
        //{
        //    get { return _selectedScript; }
        //    set { _selectedScript = value; PropertyHasChanged("SelectedScript"); }
        //}

        public ObservableCollection<EquipItem> Missiles
        {
            get
            {
                if( SelectedMember == null )
                    return null;

                var missiles = SelectedMember.Character.MainItems.Where(i => i.Position == ItemPosition.Arrow && i.Item.Charges > 0);
                return new ObservableCollection<EquipItem>(missiles);
            }
        }

        private EquipItem _selectedMissile;
        public EquipItem SelectedMissile
        {
            get { return _selectedMissile; }
            set { _selectedMissile = value; PropertyHasChanged("SelectedMissile"); }
        }

        public ObservableCollection<EquipItem> Potions
        {
            get
            {
                if( SelectedMember == null )
                    return null;

                return new ObservableCollection<EquipItem>(SelectedMember.Character.MainItems.Where(i => i.Position == ItemPosition.Potion && i.Item.Charges > 0));
            }
        }

        private EquipItem _selectedPotion;
        public EquipItem SelectedPotion
        {
            get { return _selectedPotion; }
            set { _selectedPotion = value; PropertyHasChanged("SelectedPotion"); }
        }

        public ObservableCollection<EquipItem> Staffes
        {
            get
            {
                if( SelectedMember == null )
                    return null;

                return new ObservableCollection<EquipItem>(SelectedMember.Character.MainItems.Where(i => i.Position == ItemPosition.Staff && i.Item.Charges > 0));
            }
        }
        private EquipItem _selectedStaff;
        public EquipItem SelectedStaff
        {
            get { return _selectedStaff; }
            set { _selectedStaff = value; PropertyHasChanged("SelectedStaff"); }
        }

        public string BattleName
        {
            get
            {
                if( SelectedMember == null )
                    return null;

                return SelectedMember.BattleName;
            }
            set
            {
                SelectedMember.BattleName = value;
                PropertyHasChanged( "Members" );
            }
        }

        public ICommand NewBattle { get; private set; }
        public ICommand Start { get; private set; }
        public ICommand AddMember { get; private set; }
        public ICommand Attack { get; private set; }
        public ICommand OpportunityAttack { get; private set; }
        public ICommand DoDamage { get; private set; }
        public ICommand NextMember { get; private set; }
        public ICommand SetActiveMember { get; private set; }
        public ICommand EditEffects { get; private set; }
        public ICommand SetAttackAsDone { get; private set; }
        public ICommand DoDamageDirect { get; private set; }
        public ICommand ShowEffects { get; private set; }

        public ICommand AddEffect { get; private set; }

        public ICommand RemoveEffect { get; private set; }

        public ICommand ExecuteScript { get; private set; }

        public ICommand CopyEffect { get; set; }


        private readonly Lazy<IAppAPI> _appApi = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);
        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);

        public BattleViewModel()
        {
            NewBattle = new Command((o) => { Battle.NewBattle(); _appApi.Value.RedirectToViewModel(new AddBattleMemberViewModel()); Battle.Start(); UpdateView(); });
            Start = new Command((o) => { Battle.Start(); UpdateView(); });
            AddMember = new Command((o) => _appApi.Value.RedirectToViewModel(new AddBattleMemberViewModel(), UpdateView));
            Attack = new Command((o) => { Attacker = ActiveMember; Defender = SelectedMember; PerformAttack(); }, this, () => ActiveMember != null && SelectedMember != null, "SelectedMember");
            OpportunityAttack = new Command((o) => { Attacker = SelectedMember; Defender = ActiveMember; PerformAttack(); }, this, () => ActiveMember != null && SelectedMember != null, "SelectedMember");
            DoDamage = new Command((o) => { Battle.DoDamage(Attacker.Character, Defender.Character, Damage); PropertyHasChanged("Attacks"); UpdateView(); }, this, () => Attacker != null && Defender != null, "Defender");
            NextMember = new Command((o) => { Battle.NextMember(); SelectedMember = ActiveMember; UpdateView(); });
            SetActiveMember = new Command((o) => { Battle.SetActiveMember(SelectedMember); UpdateView(); }, this, () => SelectedMember != null, "SelectedMember");
            EditEffects = new Command((o) => _appApi.Value.RedirectToViewModel(new GlobalEffectsViewModel(), UpdateView));

            AddEffect = new Command((o) => _appApi.Value.RedirectToViewModel(new RepoEffectsViewModel(true, false, SelectedMember.Character), () => { PropertyHasChanged("Effects"); _appFacade.Value.CharacterChange(SelectedMember.Character); }));
            CopyEffect = new Command((o) => _appApi.Value.RedirectToViewModel(new CopyEffectViewModel(SelectedEffect, SelectedMember, Members), () => { PropertyHasChanged("Effects"); PropertyHasChanged("Members"); }), this, () => SelectedEffect != null, "SelectedEffect");
            
            RemoveEffect = new Command((o) =>
            {
                _appApi.Value.HandleOperationResult(SelectedMember.Character.Controller.RemoveEffect(SelectedEffect));
                PropertyHasChanged("Effects");
                _appFacade.Value.CharacterChange(SelectedMember.Character);
            }, this, () => SelectedEffect != null, "SelectedEffect");

            //ExecuteScript = new Command((o) =>
            //{
            //    var param = string.IsNullOrEmpty(SelectedScript.ParamName) ? null : _appApi.Value.GetVariableFromUser<string>(SelectedScript.ParamName);
            //    var engine = ServiceContainer.GetInstance<IPythonEngine>();
            //    try
            //    {
            //        engine.ReloadScript(SelectedScript.Script);
            //        engine.GetMethod<Action<Character, object>>(SelectedScript.Name)(SelectedMember.Character, param);
            //        _appFacade.Value.CharacterChange(SelectedMember.Character);
            //        PropertyHasChanged("Effects");
            //    }
            //    catch (Exception exception)
            //    {
            //        _appApi.Value.HandleOperationResult(OperationResult.Error(exception.Message));
            //    }

            //}, this, () => SelectedScript != null, "SelectedScript");

            var refreshCommand = new Command((o) =>
                                                 {
                                                     Battle.Members.Sort();
                                                     UpdateView();
                                                 });
            SetAttackAsDone = new Command((o) =>
                                              {
                                                  SelectedAttack.Done = true;
                                                  if (SelectedAttack.MissileName != null)
                                                      _appApi.Value.HandleOperationResult(Attacker.Character.Controller.ShotMissile(SelectedAttack.MissileName));

                                                  PropertyHasChanged("Attacks");
                                              }, this, () => SelectedAttack != null, "SelectedAttack");
            DoDamageDirect = new Command((o) =>
                                             {
                                                 Battle.DoDamage(ActiveMember.Character, SelectedMember.Character, _appApi.Value.GetVariableFromUser<int>("Obrażenia:"));
                                                 UpdateView();
                                             }, this, () => ActiveMember != null && SelectedMember != null, "SelectedMember");
            ShowEffects = new Command((o) => _appApi.Value.RedirectToViewModel(new GlobalEffectsViewModel(), UpdateView));
            _appApi.Value.RegisterGlobalCommand(refreshCommand, GlobalCommands.RefreshBattleMembers);
            _appApi.Value.RegisterGlobalCommand(NextMember, GlobalCommands.NextBattleMember, () => _appApi.Value.GetGlobalVariable<string>(GlobalVariables.MainTab) == MainViewModel.TabNames.Battle);
            _appApi.Value.RegisterGlobalCommand(SetAttackAsDone, GlobalCommands.SetAttackAsDone);
            _appFacade.Value.CharacterHasChanged += (s, e) => UpdateView();

        }

        private void PerformAttack()
        {
            if (Attacker == null || Defender == null)
                return;

            AttackInfo = Battle.BrutalAttack(Attacker.Character, Defender.Character, EnvironmentModifier); 
            PropertyHasChanged("Attacks");
            PropertyHasChanged("AttackInfo");
            SelectedMemberModel = new CharacterViewModel(Attacker);
        }

        private void UpdateView()
        {
            PropertyHasChanged("Turn");
            PropertyHasChanged("Members");
            PropertyHasChanged("GlobalEffects");
        }
    }
}
