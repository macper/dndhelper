using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace DnDHelper.Domain
{
    public class Battle
    {
        public int Turn { get; set; }
        public List<BattleCharacter> Members { get; set; }
        public BattleCharacter ActiveMember { get; set; }
        public List<Effect> GlobalEffects { get; set; }

        private static readonly ILogger Logger = ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>(Const.LoggerName, typeof(Battle).Name));
        private static Battle _instance;
        public static Battle Instance
        {
            get
            {
                if (_instance == null)
                {
                    var saved = LoadState();
                    if (saved != null)
                    {
                        _instance = saved;
                    }  
                    else
                    {
                        _instance = new Battle();
                        _instance.Members = new List<BattleCharacter>();
                        _instance.GlobalEffects = new List<Effect>();
                        _instance.Turn = 0;
                    }  
                }
                return _instance;
            }
        }

        protected Battle()
        {
            ServiceContainer.GetInstance<IGameTimer>().SubscribeOnTurnChange("Battle.ctor", (amount) =>
                                                                                       {
                                                                                           foreach (var globalEffect in GlobalEffects.Where(e => e.Duration != null))
                                                                                           {
                                                                                               globalEffect.Duration -= amount;
                                                                                           }
                                                                                           foreach (var clone in Members.Where(m => m.IsClone))
                                                                                           {
                                                                                               clone.Character.Effects.ForEach(e => e.Duration -= amount);
                                                                                               clone.Character.Effects.RemoveAll(e => e.Duration <= 0);
                                                                                           }
                                                                                           GlobalEffects.RemoveAll(e => e.Duration <= 0);
                                                                                       });
        }

        public BattleCharacter AddMember(Character character,  int initiative, string nameSuffix = null )
        {
            var battleCharacter = new BattleCharacter() {Character = character, Initiative = initiative, GroupName = character.GroupName};
            if( !string.IsNullOrEmpty( nameSuffix ) )
            {
                battleCharacter.BattleName = string.Format( "{0} - {1}", character.Name, nameSuffix );
            }
            else
            {
                battleCharacter.BattleName = character.Name;
            }
            Members.Add(battleCharacter);
            Members.Sort();
            Logger.Info(string.Format("Dodano membera: {0}, Inicjatywa: {1} + {2} = {3}", character.Name, character.CurrentStats.Initiative, initiative, battleCharacter.Initiative));
            return battleCharacter;
        }

        public void AddMemberCopy(Character character, int initiative, string nameSuffix)
        {
            var copy = DarkTemplar.DeepClone(character, true);
            var member = AddMember(copy, initiative, nameSuffix);
            member.IsClone = true;
        }

        public void AddEffect(Effect effect)
        {
            GlobalEffects.Add(effect);
            Logger.Info("Dodano efekt globalny: " + effect);
        }

        public void RemoveEffect(Effect effect)
        {
            GlobalEffects.Remove(effect);
            foreach (var member in Members)
            {
                if (member.Character.Effects.Contains(effect))
                {
                    member.Character.Controller.RemoveEffect(effect);
                }
            }
        }

        public IEnumerable<Effect> GetEffectForMember(BattleCharacter character)
        {
            return GlobalEffects.Where(globalEffect => character.Character.Effects.Contains(globalEffect));
        }

        public void Start()
        {
            if (Members.Count == 0)
            {
                return;
            }
            Turn = 1;
            SetActiveMember(Members.First());
        }

        public void NewBattle()
        {
            Members.Clear();
            Turn = 0;
            ActiveMember = null;
            ServiceContainer.GetInstance<BriefServiceQueueManager>().RemoveAllEnemies();
        }

        public void SetActiveMember(BattleCharacter character)
        {
            ActiveMember = character;
            Members.ForEach(m => m.IsActive = false);
            character.IsActive = true;
            Logger.Info("Ustawiono aktywnego membera: " + character.Character.Name);
        }

        public void NextMember()
        {
            if (Turn == 0)
            {
                return;
            }

            int index = Members.IndexOf(ActiveMember);
            if (index == Members.Count - 1)
            {
                NewTurn();
                return;
            }
            SetActiveMember(Members[++index]);
            SaveState();
        }

        private void NewTurn()
        {
            Turn++;
            Logger.Info("Nowa tura: " + Turn);
            SetActiveMember(Members.First());
            ServiceContainer.GetInstance<IGameTimer>().AddTurns(1);
            
        }

        public AttackInfo BrutalAttack(Character attacker, Character defender, int environmentModifier)
        {
            var attackInfo = new AttackInfo();
            foreach (var attack in attacker.Attacks)
            {
                var header = new AttackInfoHeader { Header = attack.Name};
                for (var i = 0; i < attack.NumberOfAttacks; i++)
                {
                    header.Attacks.Add(new AttackViewModel()
                                               {
                                                   Damage = attack.Damage.ToString(),
                                                   ToHit = defender.CurrentStats.AC.Total - (attack.ToHit - (i * 5)) - environmentModifier,
                                                   Name = attack.Name,
                                                   MissileName = attack.MissileName
                                               });
                }
                attackInfo.AttacksHeaders.Add(header);
            }

            attackInfo.AttackerModifiers = GetEffectModifiers(attacker);
            attackInfo.DefenderModifiers = GetEffectModifiers(defender);

            return attackInfo;
        }

        private List<AttackEffectModifier> GetEffectModifiers(Character character)
        {
            var retList = new List<AttackEffectModifier>();

            foreach (var effect in character.Effects.Where(e => e.Definition.IsBattleEffect))
            {
                retList.Add(new AttackEffectModifier
                {
                    Name = effect.Definition.Name,
                    Details = effect.Definition.Description,
                    Positive = effect.Definition.IsPositive
                });  
            }
            return retList;
        }

        public void DoDamage(Character attacker, Character target, int damage)
        {
            target.Controller.DoDamage(damage);
            if (target.Life <= 0)
            {
                attacker.Kills.Add(new KilledCreature() { Date = ServiceContainer.GetInstance<IGameTimer>().CurrentTime, Name = target.Name, Race = target.Race.Name, Level = target.Level});
            }
        }

        private void SaveState()
        {
            var serializer = new XmlSerializer(typeof(Battle));
            using (var fs = new FileStream("BattleState.xml", FileMode.Create))
            {
                serializer.Serialize(fs, this);
            }
        }

        private static Battle LoadState()
        {
            var serializer = new XmlSerializer(typeof(Battle));
            if (!File.Exists("BattleState.xml"))
                return null;

            using (var fs = new FileStream("BattleState.xml", FileMode.Open))
            {
                var battle = (Battle) serializer.Deserialize(fs);
                var realMembers = battle.Members.Where(m => !m.IsClone).ToArray();
                var appFacade = ServiceContainer.GetInstance<AppFacade>();
                foreach (var realMember in realMembers)
                {
                    var character = appFacade.Repositories.Get<Character>().Elements.FirstOrDefault(c => c.Name == realMember.Name);
                    if (character == null)
                        continue;

                    realMember.Character = character;
                }
                battle.ActiveMember = battle.Members.SingleOrDefault(m => m.IsActive);
                return battle;
            }
           
        }

        public BattleCharacter GetBattleCharacter(Character character)
        {
            return Members.FirstOrDefault(m => m.Character == character);
        }
    }
}
