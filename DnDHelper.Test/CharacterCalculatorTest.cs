using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DnDHelper.Domain;
using DnDHelper.Domain.Bonuses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DnDHelper.Test
{
    [TestClass]
    public class CharacterCalculatorTest : BaseTest
    {
        [TestMethod]
        public void CalculateInitial()
        {
            var repoSet = ServiceContainer.GetInstance<RepositorySet>();
            var @class = repoSet.Get<ClassDefinition>().GetElementByName("Barbarzyńca").CreateItem();
            @class.Level = 10;

            var testCharacter = new Domain.Character
            {
                Race = repoSet.Get<RaceDefinition>().GetElementByName("Elf").CreateItem()
            };
            testCharacter.Class.Add(@class);
            var calculator = new CharacterCalculator(repoSet);
            calculator.CalculateInitialStats(testCharacter);
            Assert.IsTrue(testCharacter.OriginalStats.Throws.EnduranceThrow == 7);
            Assert.IsTrue(testCharacter.OriginalStats.Throws.ReflexThrow == 7);
            Assert.IsTrue(testCharacter.OriginalStats.Throws.WillThrow == 3);
            Assert.IsTrue(testCharacter.OriginalStats.Attack.Melee == 10);
            Assert.IsTrue(testCharacter.SkillPointsLeft.Atutes == 4);
            Assert.IsTrue(testCharacter.InitialBonuses.Count == 10);

            Assert.IsTrue(testCharacter.InitialBonuses.Any(b => b.GetType() == typeof(MainSkillBonus) && ((MainSkillBonus)b).Attribute == Domain.BaseAttribute.Dexterity && b.Value == 2 && b.Source == "Rasa"));
            Assert.IsTrue(testCharacter.InitialBonuses.Any(b => b.GetType() == typeof(SecondarySkillBonus) && ((SecondarySkillBonus)b).SkillName == "Nasłuchiwanie" && b.Value == 2 && b.Source == "Rasa"));
            Assert.IsTrue(testCharacter.InitialBonuses.Any(b => b.GetType() == typeof(AtutBonus) && ((AtutBonus)b).AtutName == "Odporność na zauroczenia" && b.Source == "Rasa"));
            Assert.IsTrue(testCharacter.InitialBonuses.Any(b => b.GetType() == typeof(AtutBonus) && b.Source == "Klasa" && ((AtutBonus)b).AtutName == "Szał" && ((AtutBonus)b).AdditionalInfo == "3/dzień"));
        }

        [TestMethod]
        public void CalculateZorg()
        {
            var repoSet = ServiceContainer.GetInstance<RepositorySet>();
            var @class = repoSet.Get<ClassDefinition>().GetElementByName("Barbarzyńca").CreateItem();
            @class.Level = 10;

            var zorg = new Domain.Character
                           {
                               Name = "Zorg",
                               Race = repoSet.Get<RaceDefinition>().GetElementByName("Półork").CreateItem(),
                               OriginalStats =
                                   {
                                       Strength = 16,
                                       Dexterity = 13,
                                       Constitution = 13,
                                       Wisdom = 9,
                                       Inteligence = 12,
                                       Charisma = 12
                                   }
                           };
            zorg.Class.Add(@class);
            var calculator = new CharacterCalculator(repoSet);
            calculator.CalculateInitialStats(zorg);
            zorg.OriginalStats.HP = 95;
            var atuty = repoSet.Get<AtutDefinition>();
            var at = atuty.GetElementByName("Skupienie na broni").CreateItem();
            at.AdditionalInfo = ItemPrototypes.TwoHandedSword;
            zorg.OriginalStats.Atutes.Add(at);
            at = atuty.GetElementByName("Potężniejsze skupienie na broni").CreateItem();
            at.AdditionalInfo = ItemPrototypes.TwoHandedSword;
            zorg.OriginalStats.Atutes.Add(at);
            var itemRepo = repoSet.Get<ItemDefinition>();
            zorg.Controller.EquipItem(itemRepo.GetElementByName("Miecz dwuręczny+2, Zguba Pająka").CreateItem(), ItemPosition.RightHand);
            zorg.Controller.EquipItem(itemRepo.GetElementByName("Hełm Baldurana").CreateItem(), ItemPosition.Head);
            zorg.Controller.EquipItem(itemRepo.GetElementByName("Skórzana ćwiekowana+2").CreateItem(), ItemPosition.Torso);
            zorg.Controller.EquipItem(itemRepo.GetElementByName("Pas siły+1").CreateItem(), ItemPosition.Belt);

            calculator.Calculate(zorg);
            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(MainSkillBonus) && b.Source == CommonSources.Race && ((MainSkillBonus)b).Attribute == Domain.BaseAttribute.Strength && b.Value == 2));
            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(MainSkillBonus) && b.Source == "Pas siły+1" && ((MainSkillBonus)b).Attribute == Domain.BaseAttribute.Strength && b.Value == 1));
            Assert.IsTrue(zorg.CurrentStats.Strength == 19);

            Assert.IsTrue(zorg.CurrentStats.Inteligence == 10);
            Assert.IsTrue(zorg.CurrentStats.Charisma == 10);

            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(ThrowBonus) && b.Source == CommonSources.Constitution && ((ThrowBonus)b).BonusType.EnduranceThrow == 1));
            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(ThrowBonus) && b.Source == CommonSources.Dexterity && ((ThrowBonus)b).BonusType.ReflexThrow == 1));
            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(ThrowBonus) && b.Source == CommonSources.Wisdom && ((ThrowBonus)b).BonusType.WillThrow == -1));

            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(ThrowBonus) && b.Source == "Hełm Baldurana" && ((ThrowBonus)b).BonusType.EnduranceThrow == 1 && ((ThrowBonus)b).BonusType.ReflexThrow == 1 && ((ThrowBonus)b).BonusType.WillThrow == 1));
            Assert.IsTrue(zorg.CurrentStats.Throws.EnduranceThrow == 9);
            Assert.IsTrue(zorg.CurrentStats.Throws.ReflexThrow == 9);
            Assert.IsTrue(zorg.CurrentStats.Throws.WillThrow == 3);

            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(HPBonus) && b.Source == "Hełm Baldurana" && b.Value == 12));
            Assert.IsTrue(zorg.CurrentStats.HP == 107);

            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(ACBonus) && b.Source == "Hełm Baldurana" && b.Value == 1));
            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(ACBonus) && b.Source == "Skórzana ćwiekowana+2" && ((ACBonus)b).ACType == ACBonusTypes.Armor && b.Value == 3));
            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(ACBonus) && b.Source == "Skórzana ćwiekowana+2" && ((ACBonus)b).ACType == ACBonusTypes.MagicShield && b.Value == 2));
            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(ACBonus) && b.Source == CommonSources.Dexterity && b.Value == 1));
            Assert.IsTrue(zorg.CurrentStats.AC.Total == 17);
            
            Assert.IsTrue(zorg.CurrentStats.Attack.Melee == 14);
            Assert.IsTrue(zorg.CurrentStats.Attack.Range == 11);
            Assert.IsTrue(zorg.CurrentStats.Attack.NumberOfAttacks == 2);

            var attack = zorg.Attacks.First();
            Assert.IsTrue(attack.Damage.ToString() == "2K6+8[Fizyczne]");
            Assert.IsTrue(attack.ToHit == 18);

            zorg.Effects.Add(repoSet.Get<EffectDefinition>().GetElementByName("Szał").CreateItem());
            calculator.Calculate(zorg);

            Assert.IsTrue(zorg.CurrentStats.Strength == 23);
            Assert.IsTrue(zorg.CurrentStats.Constitution == 17);
            attack = zorg.Attacks.First();
            Assert.IsTrue(attack.Damage.ToString() == "2K6+11[Fizyczne]");
            Assert.IsTrue(attack.ToHit == 20);
            Assert.IsTrue(zorg.CurrentStats.AC.Total == 15);
            Assert.IsTrue(zorg.CurrentStats.HP == 127);
            Assert.IsTrue(zorg.CurrentStats.Speed == 12);

            Assert.IsTrue(zorg.Bonuses.Any(b => b.GetType() == typeof(ResistanceBonus) && b.Source == CommonSources.Class && b.Value == 2 && ((ResistanceBonus)b).DamageType == DamageTypes.Physical));

            zorg.Controller.UnEquipItem(ItemPosition.RightHand);
            zorg.Controller.EquipItem(itemRepo.GetElementByName("Ciężka kusza celności").CreateItem(), ItemPosition.RightHand);
            var item = itemRepo.GetElementByName("Bełt").CreateItem();
            item.Charges = 40;
            zorg.Controller.EquipItem(item, ItemPosition.Arrow);
            item = itemRepo.GetElementByName("Bełt+1").CreateItem();
            item.Charges = 20;
            zorg.Controller.EquipItem(item, ItemPosition.Arrow);
            calculator.Calculate(zorg);

            attack = zorg.Attacks[0];
            Assert.IsTrue(attack.Name == string.Format("{0} - {1}({2})", "Ciężka kusza celności", "Bełt", 40));
            Assert.IsTrue(attack.Damage.ToString() == "1K12+1[Fizyczne]");
            Assert.IsTrue(attack.ToHit == 11);
            attack = zorg.Attacks[1];
            Assert.IsTrue(attack.Name == string.Format("{0} - {1}({2})", "Ciężka kusza celności", "Bełt+1", 20));
            Assert.IsTrue(attack.Damage.ToString() == "1K12+2[Fizyczne]");
            Assert.IsTrue(attack.ToHit == 12);

            zorg.OriginalStats.Atutes.Add(repoSet.Get<AtutDefinition>().GetElementByName(AtutDefinition.CommonAtutes.TwoWeaponsCombat).CreateItem());
            zorg.Controller.UnEquipItem(ItemPosition.RightHand);
            item = itemRepo.GetElementByName("Miecz krótki").CreateItem();
            zorg.Controller.EquipItem(item, ItemPosition.RightHand);
            item = itemRepo.GetElementByName("Miecz krótki").CreateItem();
            zorg.Controller.EquipItem(item, ItemPosition.LeftHand);
            calculator.Calculate(zorg);

            attack = zorg.Attacks[0];
            Assert.IsTrue(attack.Damage.ToString() == "1K6+6[Fizyczne]");
            Assert.IsTrue(attack.ToHit == 14);
            Assert.IsTrue(attack.NumberOfAttacks == 2);
            attack = zorg.Attacks[1];
            Assert.IsTrue(attack.Damage.ToString() == "1K6+6[Fizyczne]");
            Assert.IsTrue(attack.ToHit == 14);
            Assert.IsTrue(attack.NumberOfAttacks == 1);
        }

        [TestMethod]
        public void CalculateTwoClassesCharacter()
        {
            var repoSet = ServiceContainer.GetInstance<RepositorySet>();
            var class1 = repoSet.Get<ClassDefinition>().GetElementByName("Barbarzyńca").CreateItem();
            class1.Level = 5;
            var class2 = repoSet.Get<ClassDefinition>().GetElementByName("Złodziej").CreateItem();
            class2.Level = 5;

            var ziutek = new Domain.Character
                             {
                                 Name = "Ziutek",
                                 Race = repoSet.Get<RaceDefinition>().GetElementByName("Elf").CreateItem(),
                                 OriginalStats = new Domain.Stats
                                                     {
                                                         Strength = 14,
                                                         Dexterity = 17,
                                                         Constitution = 14,
                                                         Inteligence = 12,
                                                         Wisdom = 8,
                                                         Charisma = 10
                                                     }
                             };
            ziutek.Class.AddRange(new [] { class1, class2});
            var calculator = new CharacterCalculator(repoSet);
            calculator.CalculateInitialStats(ziutek);

            Assert.IsTrue(ziutek.Level == 10);
            Assert.IsTrue(ziutek.OriginalStats.Attack.Melee == 8);
            Assert.IsTrue(ziutek.OriginalStats.Attack.NumberOfAttacks == 2);
            Assert.IsTrue(ziutek.OriginalStats.Throws.EnduranceThrow == 5);
            Assert.IsTrue(ziutek.OriginalStats.Throws.ReflexThrow == 8);
            Assert.IsTrue(ziutek.OriginalStats.Throws.WillThrow == 2);
            Assert.IsTrue(ziutek.OriginalStats.HP == 74);
        }

        [TestMethod]
        public void CalculateMonster()
        {
            var dragon = new Domain.Character
                             {
                                 Name = "Smok",
                                 OriginalStats = new Domain.Stats
                                                     {
                                                         Strength = 20,
                                                         Dexterity = 20,
                                                         Constitution = 20,
                                                         Wisdom = 20
                                                     }
                             };
            dragon.OriginalStats.HP = 300;
            dragon.OriginalStats.AC.Increase(ACBonusTypes.NaturalArmor, 20);
            dragon.OriginalStats.Throws = new Domain.Throw(10,10,10);
            dragon.OriginalStats.Speed = 16;
            dragon.OriginalStats.Attack.Melee = 10;
            dragon.OriginalStats.Attack.Range = 5;
            dragon.OriginalStats.Attack.NumberOfAttacks = 2;
            dragon.Controller.AddCustomAttack(new CustomAttack()
                                                  {
                                                      Name = "Paszcza",
                                                      Bonuses = new List<BaseBonus>(new BaseBonus[] { new DamageBonus("Paszcza", new DamageBone(20, 4)), new NumberOfAttacksBonus("Paszcza", 2) })
                                                  });
            dragon.Controller.AddCustomAttack(new CustomAttack()
                                                  {
                                                      Name = "Zionięcie",
                                                      Bonuses = new List<BaseBonus>(new BaseBonus[] 
                                                      { 
                                                          new DamageBonus("Zionięcie", new DamageBone(10, 4) { DamageType = DamageTypes.Fire}),
                                                          new RangeBonus("Zionięcie", 20),
                                                          new AttackBonus("Zionięcie", 2)
                                                      
                                                  })});
            Assert.IsTrue(dragon.CurrentStats.AC.Total == 35);
            Assert.IsTrue(dragon.CurrentStats.Throws.WillThrow == 15);
            Assert.IsTrue(dragon.CurrentStats.Attack.Melee == 15);

            var attack = dragon.Attacks.SingleOrDefault(a => a.Name == "Paszcza");
            Assert.IsNotNull(attack);
            Assert.IsTrue(attack.Damage.ToString() == "4K20+5[Fizyczne]");
            Assert.IsTrue(attack.NumberOfAttacks == 4);
            Assert.IsTrue(attack.ToHit == 15);

            attack = dragon.Attacks.SingleOrDefault(a => a.Name == "Zionięcie");
            Assert.IsNotNull(attack);
            Assert.IsTrue(attack.Damage.ToString() == "4K10[Ogień]");
            Assert.IsTrue(attack.ToHit == 12);
            Assert.IsTrue(attack.Range == 20);
        }
    }

    


}
