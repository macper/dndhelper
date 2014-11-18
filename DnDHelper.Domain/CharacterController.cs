using System;
using System.Collections.Generic;
using System.Linq;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.Domain
{
    [Serializable]
    public class CharacterController
    {
        private readonly Character _owner;
        public Character Owner { get { return _owner; } }
        private static readonly ILogger Logger = ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>(Const.LoggerName, typeof(CharacterController).Name));
        public bool DisableRecalculation { get; set; }

        public CharacterController(Character owner)
        {
            _owner = owner;
        }

        public OperationResult Recalculate()
        {
            if (DisableRecalculation)
                return OperationResult.Success();

            try
            {
                ServiceContainer.GetInstance<ICharacterCalculator>().Calculate(_owner);
            }
            catch (Exception exception)
            {
                return OperationResult.Error(exception.Message);
            }
            return OperationResult.Success();
        }

        public OperationResult EquipItem(Item item, ItemPosition position)
        {
            if (item == null)
                return OperationResult.Warning("Item is null");

            if (!Rules.IsContainerItem(position))
            {
                _owner.MainItems.RemoveAll(r => r.Position == position);
            }
            _owner.MainItems.RemoveAll(r => r.Item == item);
            
            _owner.MainItems.Add(new EquipItem(position, item));
            
            return Recalculate();
        }

        public OperationResult EquipItem(Item item)
        {
            if (item == null)
                return OperationResult.Error("Item is null");

            switch (item.Definition.BaseType)
            {
                case BaseTypes.OneHandWeapon:
                    if (_owner.MainItems.Any(s => s.Position == ItemPosition.RightHand))
                    {
                        if (_owner.MainItems.Any(s => s.Position == ItemPosition.LeftHand))
                        {
                            UnEquipItem(ItemPosition.RightHand);
                            return EquipItem(item, ItemPosition.RightHand);
                        }
                        else
                        {
                            return EquipItem(item, ItemPosition.LeftHand);
                        }
                    }
                    else
                    {
                        return EquipItem(item, ItemPosition.RightHand);
                    }

                case BaseTypes.TwoHandedWeapon:
                    if (_owner.MainItems.Any(s => s.Position == ItemPosition.LeftHand))
                    {
                        UnEquipItem(ItemPosition.LeftHand);
                    }
                    if (_owner.MainItems.Any(s => s.Position == ItemPosition.RightHand))
                    {
                        UnEquipItem(ItemPosition.RightHand);
                    }
                    return EquipItem(item, ItemPosition.RightHand);

                case BaseTypes.Armor:
                    return CheckAndUnequipIfNecessary(item, ItemPosition.Torso);

                case BaseTypes.Arrow:
                    return EquipItem(item, ItemPosition.Arrow);

                case BaseTypes.Belt:
                    return CheckAndUnequipIfNecessary(item, ItemPosition.Belt);

                case BaseTypes.Boots:
                    return CheckAndUnequipIfNecessary(item, ItemPosition.Boots);

                case BaseTypes.Cloak:
                    return CheckAndUnequipIfNecessary(item, ItemPosition.Cloak);

                case BaseTypes.Gloves:
                    return CheckAndUnequipIfNecessary(item, ItemPosition.Gloves);

                case BaseTypes.Helmet:
                    return CheckAndUnequipIfNecessary(item, ItemPosition.Head);

                case BaseTypes.Necklease:
                    return CheckAndUnequipIfNecessary(item, ItemPosition.Neck);

                case BaseTypes.Potion:
                    return EquipItem(item, ItemPosition.Potion);

                case BaseTypes.Ring:
                    return EquipItem(item, ItemPosition.Finger);

                case BaseTypes.Scroll:
                case BaseTypes.Wand:
                    return EquipItem(item, ItemPosition.Staff);

                case BaseTypes.Shield:
                    return CheckAndUnequipIfNecessary(item, ItemPosition.LeftHand);

                default:
                    return OperationResult.Error("Nie można automatycznie założyć przedmiotu");
            }
        }

        public OperationResult EquipItem(string itemName)
        {
            var item = _owner.MainItems.SingleOrDefault(s => s.Item.Name == itemName);
            if (item == null)
                return OperationResult.Error("Brak itema");

            return EquipItem(item.Item);
        }

        private OperationResult CheckAndUnequipIfNecessary(Item item, ItemPosition position)
        {
            if (_owner.MainItems.Any(s => s.Position == position))
            {
                UnEquipItem(position);
            }
            return EquipItem(item, position);
        }

        public OperationResult UnEquipItem(ItemPosition position)
        {
            var item = _owner.MainItems.FirstOrDefault(i => i.Position == position);
            if (item == null)
                return OperationResult.Warning("Brak takiego itema");
            
            _owner.MainItems.RemoveAll(r => r.Position == position);
            _owner.MainItems.Add(new EquipItem(ItemPosition.Backpack, item.Item));
            return Recalculate();
        }

        public OperationResult UnEquipItem(EquipItem item)
        {
            _owner.MainItems.Remove(item);
            _owner.MainItems.Add(new EquipItem(ItemPosition.Backpack, item.Item));
            return Recalculate();
        }

        public OperationResult RemoveItem(EquipItem equipItem)
        {
            _owner.MainItems.Remove(equipItem);
            return Recalculate();
        }

        public OperationResult AddBonus(BaseBonus bonus)
        {
            _owner.Bonuses.Add(bonus);
            LogDebug(string.Format("Dodano Bonus - {0} {1}", bonus.Source, bonus));
            return Recalculate();
        }

        public OperationResult AddBonusOriginal(BaseBonus bonus)
        {
            _owner.InitialBonuses.Add(bonus);
            LogDebug(string.Format("Dodano InitialBonus - {0} {1}", bonus.Source, bonus));
            return Recalculate();
        }

        public OperationResult RemoveBonus(BaseBonus bonus)
        {
            _owner.Bonuses.Remove(bonus);
            LogDebug(string.Format("Usunięto bonus {0}", bonus));
            return Recalculate();
        }

        public OperationResult RemoveBonusOriginal(BaseBonus bonus)
        {
            _owner.InitialBonuses.Remove(bonus);
            LogDebug(string.Format("Usunięto bonus {0} - {1}", "Initial", bonus));
            return Recalculate();
        }

        private OperationResult ChangeSecondarySkillInternal(string skillName, int value, bool original)
        {
            var stats = original ? _owner.OriginalStats : _owner.CurrentStats;
            if (!stats.Skills.Any(s => s.Name == skillName))
            {
                var skill = ServiceContainer.GetInstance<RepositorySet>().Get<SkillDefinition>().GetElementByName(skillName);
                if (skill == null)
                    return OperationResult.Error("Nie odnaleziono skilla: " + skillName);

                stats.Skills.Add(skill.CreateItem());
            }

            stats.Skills.Single(s => s.Name == skillName).Value += value;
            LogDebug(string.Format("Zwiększono wartość skilla {0} o {1}", skillName, value));
            return Recalculate();
        }

        public OperationResult ChangeSecondarySkill(string skillName, int value)
        {
            return ChangeSecondarySkillInternal(skillName, value, false);
        }

        public OperationResult ChangeSecondarySkillOriginal(string skillName, int value)
        {
            if (_owner.SkillPointsLeft.SecondarySkills < value)
            {
                return OperationResult.Error("Brak wystarczającej liczby punktów umiejętności");
            }
            _owner.SkillPointsLeft.SecondarySkills -= value;
            return ChangeSecondarySkillInternal(skillName, value, true);
        }

        public OperationResult IncreaseMainSkill(BaseAttribute skill, int value)
        {
            return IncreaseMainSkillInternal(skill, value, false);
        }

        public OperationResult IncreaseMainSkillOriginal(BaseAttribute skill, int value)
        {
            return IncreaseMainSkillInternal(skill, value, true);
        }

        private OperationResult IncreaseMainSkillInternal(BaseAttribute skill, int value, bool original)
        {
            var stats = original ? _owner.OriginalStats : _owner.CurrentStats;
            switch (skill)
            {
                case BaseAttribute.Charisma:
                    stats.Charisma += value;
                    break;

                case BaseAttribute.Constitution:
                    stats.Constitution += value;
                    break;

                case BaseAttribute.Dexterity:
                    stats.Dexterity += value;
                    break;

                case BaseAttribute.Inteligence:
                    stats.Inteligence += value;
                    break;

                case BaseAttribute.Strength:
                    stats.Strength += value;
                    break;

                case BaseAttribute.Wisdom:
                    stats.Wisdom += value;
                    break;
            }
            LogDebug(string.Format("Zwiększono wartość współczynnika {0} ({1}) o {2}", skill,
                                   original ? "Oryginalnego" : "Bieżącego", value));
            return Recalculate();
        }

        private OperationResult AddAtuteInternal(Atut atut, bool original)
        {
            var stats = original ? _owner.OriginalStats : _owner.CurrentStats;
            
            stats.Atutes.Add(atut);
            LogDebug("Dodano atut: " + atut.Name);
            return Recalculate();
        }

        public OperationResult AddAtute(Atut atut)
        {
            return AddAtuteInternal(atut, false);
        }

        public OperationResult AddAtuteOriginal(Atut atut)
        {
            return AddAtuteInternal(atut, true);
        }

        public OperationResult RemoveAtute(Atut atut)
        {
            _owner.CurrentStats.Atutes.Remove(atut);
            LogDebug("Usunięto atut:" + atut.Name);
            return Recalculate();
        }

        public OperationResult RemoveAtuteOriginal(Atut atut)
        {
            _owner.OriginalStats.Atutes.Remove(atut);
            LogDebug("Usunięto atut (Initial):" + atut.Name);
            return Recalculate();
        }

        public OperationResult AddEffect(Effect effect)
        {
            _owner.Effects.Add(effect);
            LogDebug("Dodano efekt: " + effect);
            
            return Recalculate();
        }

        public OperationResult RemoveEffect(Effect effect)
        {
            _owner.Effects.Remove(effect);
            LogDebug("Usunięto efekt: " + effect);
            return Recalculate();
        }

        public OperationResult AddAttack(Attack attack)
        {
            _owner.Attacks.Add(attack);
            LogDebug("Dodano atak: " + attack);
            return Recalculate();
        }

        public OperationResult DoDamage(int value)
        {
            _owner.Life -= value;
            Logger.Debug("otrzymano " + value + " obrażeń");
            ServiceContainer.GetInstance<AppFacade>().CharacterChange(_owner);
            return OperationResult.Success();
        }

        public OperationResult ChangeThrow(int value, ThrowType throwType)
        {
            _owner.CurrentStats.Throws.Change(value, throwType);
            LogDebug("Zmieniono rzut " + throwType + " na " + value.ToString());
            return Recalculate();
        }

        public OperationResult ChangeThrowOriginal(int value, ThrowType throwType)
        {
            _owner.OriginalStats.Throws.Change(value, throwType);
            LogDebug("Zmieniono rzut (oryginalny) " + throwType + " na " + value.ToString());
            return Recalculate();
        }

        public OperationResult Heal(int value, bool allowOverflow = false)
        {
            _owner.Life += value;
            if (!allowOverflow && _owner.Life > _owner.CurrentStats.HP)
            {
                _owner.Life = _owner.CurrentStats.HP;
            }
            return OperationResult.Success();
        }

        public OperationResult ChangeACOriginal(int value, string ACType)
        {
            try
            {
                _owner.OriginalStats.AC.Increase(ACType, value);
                LogDebug("Zmieniono AC - " + ACType + " o " + value);
            }
            catch (Exception exception)
            {
                return OperationResult.Error(exception.Message);
            }
            return Recalculate();
        }

        public OperationResult ChangeAttackSkillOriginal(int value, bool melee)
        {
            if (melee)
            {
                _owner.OriginalStats.Attack.Melee += value;
                LogDebug("Zmieniono atak wręcz o " + value);
            }
            else
            {
                _owner.OriginalStats.Attack.Range += value;
                LogDebug("Zmieniono atak dystansowy o " + value);
            }
            return Recalculate();
        }

        public OperationResult ChangeAttacksCountOriginal(int value)
        {
            _owner.OriginalStats.Attack.NumberOfAttacks += value;
            LogDebug("Zmieniono liczbę ataków o " + value);
            return Recalculate();
        }

        public OperationResult ChangeSpeedOriginal(int value)
        {
            _owner.OriginalStats.Speed += value;
            if (_owner.OriginalStats.Speed < 0)
                _owner.OriginalStats.Speed = 0;
            LogDebug("Zmienio szybkość o " + value);
            return Recalculate();
        }

        public OperationResult ChangeInitiativeOriginal(int value)
        {
            _owner.OriginalStats.Initiative += value;
            LogDebug("Zmienio inicjatywę o " + value);
            return Recalculate();
        }

        public OperationResult ChangeHPOriginal(int value)
        {
            _owner.OriginalStats.HP += value;
            LogDebug("Zmieniono bazową liczbę punktów życia o " + value);
            return Recalculate();
        }

        public OperationResult AddCustomAttack(CustomAttack customAttack)
        {
            if (_owner.CustomAttacks.Any(a => a.Name == customAttack.Name))
            {
                return OperationResult.Error("Isnieje już atak o takiej nazwie");
            }
            if (!customAttack.Bonuses.Any(b => b.GetType() == typeof(Bonuses.DamageBonus)))
            {
                return OperationResult.Error("Dodawany atak musi mieć bonus z obrażeń");
            }
            _owner.CustomAttacks.Add(customAttack);
            return Recalculate();
        }

        public OperationResult RemoveCustomAttack(CustomAttack customAttack)
        {
            _owner.CustomAttacks.Remove(customAttack);
            return Recalculate();
        }

        public OperationResult CastSpell(Spell spell)
        {
            if (!_owner.Spells.Contains(spell))
                return OperationResult.Error("Brak takiego czaru");
            spell.IsCasted = true;
            ServiceContainer.GetInstance<AppFacade>().CharacterChange(_owner);
            LogDebug("Rzucono czar: " + spell.Name);
            return OperationResult.Success();
        }

        public OperationResult ResetSpells()
        {
            ServiceContainer.GetInstance<AppFacade>().CharacterChange(_owner);
            _owner.Spells.ForEach(s => s.IsCasted = false);
            LogDebug("Zresetowano czary");
            return OperationResult.Success();
        }

        public OperationResult AddSpell(SpellDefinition spellDefinition)
        {
            if (!_owner.AvailableCastings.Any(c => c.Level == spellDefinition.Level && spellDefinition.SpellTypes.Contains(c.Type)))
                return OperationResult.Error("Brak możliwości rzucania tego typu czaru");

            if (_owner.Spells.Count(s => s.Definition.Level == spellDefinition.Level) >= _owner.AvailableCastings.Single(c => c.Level == spellDefinition.Level).Count)
                return OperationResult.Error("Osiągnięto limit czarów z tego poziomu");

            _owner.Spells.Add(spellDefinition.CreateItem());
            ServiceContainer.GetInstance<AppFacade>().CharacterChange(_owner);
            LogDebug("Dodano czar: " + spellDefinition.Name);
            return OperationResult.Success();
        }

        public OperationResult AddKnownSpell(SpellDefinition spellDefinition)
        {
            if (!_owner.AvailableCastings.Any(c => c.Level == spellDefinition.Level && spellDefinition.SpellTypes.Contains(c.Type)))
                return OperationResult.Error("Brak możliwości rzucania tego typu czaru");

            if (_owner.KnownSpells.Any(s => s.Name == spellDefinition.Name))
                return OperationResult.Error("Już wybrano ten czar");
            _owner.KnownSpellsNames.Add(spellDefinition.Name);
            ServiceContainer.GetInstance<AppFacade>().CharacterChange(_owner);
            LogDebug("Dodano znany czar: " + spellDefinition.Name);
            return OperationResult.Success();
        }

        public OperationResult ShotMissile(string missileName)
        {
            var item = _owner.MainItems.FirstOrDefault(m => m.Position == ItemPosition.Arrow && m.Item.Name == missileName);
            if (item == null)
                return OperationResult.Error("Brak takiego pocisku");
            if (item.Item.Charges <= 0)
                return OperationResult.Error("Brak pocisków");
            item.Item.Charges--;
            return OperationResult.Success();
        }

        private void LogDebug(string message)
        {
            Logger.Debug(_owner.Name + ": " + message);
        }
    }
}