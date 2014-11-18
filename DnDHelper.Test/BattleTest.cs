using System.Linq;
using DnDHelper.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DnDHelper.Test
{
    [TestClass]
    public class BattleTest : BaseTest
    {
        [TestMethod]
        public void SimpleBattle()
        {
            var battle = Domain.Battle.Instance;
            var mockStorage = new Mock<IRepositoryStorage>();
            ServiceContainer.Kernel.Rebind<IRepositoryStorage>().ToConstant(mockStorage.Object);
            var characterRepo = ServiceContainer.GetInstance<RepositorySet>().Get<Domain.CharacterGroup>();
            var group = characterRepo.Elements.First();

            var hero = group.Characters.First(m => m.Name == "Hero");

            var zombie1 = DarkTemplar.DeepClone(group.Characters.First(m => m.Name == "Zombie"), true);
            zombie1.Name = "Zombie1";
            var zombie2 = DarkTemplar.DeepClone(zombie1, true);
            zombie2.Name = "Zombie2";

            var heroModel = battle.AddMember(hero, 10);
            var zombieModel1 = battle.AddMember(zombie1, 3);
            var zombieModel2 = battle.AddMember(zombie2, 4);

            battle.Start();
            Assert.IsTrue(battle.Turn == 1);
            var bless = ServiceContainer.GetInstance<RepositorySet>().Get<EffectDefinition>().GetElementByName("B³ogos³awieñstwo").CreateItem();
            bless.Duration = 3;
            battle.AddEffect(bless);
            hero.Controller.AddEffect(bless);
            Assert.IsTrue(hero.Effects.Contains(bless) && hero.Bonuses.Any(b => b.Source == "B³ogos³awieñstwo"));
            Assert.IsTrue(heroModel.IsActive);
            battle.NextMember();
            Assert.IsTrue(zombieModel2.IsActive);
            Assert.IsFalse(heroModel.IsActive);
            battle.NextMember();
            Assert.IsTrue(zombieModel1.IsActive);
            battle.NextMember();
            Assert.IsTrue(battle.Turn == 2);
            Assert.IsTrue(heroModel.IsActive);

            var slow = ServiceContainer.GetInstance<RepositorySet>().Get<EffectDefinition>().GetElementByName("Spowolnienie").CreateItem();
            slow.Duration = 5;
            battle.AddEffect(slow);
            zombie1.Controller.AddEffect(slow);
            zombie2.Controller.AddEffect(slow);

            Assert.IsTrue(zombie1.CurrentStats.Speed == 3 && zombie2.CurrentStats.Speed == 3);
            battle.NextMember();
            battle.NextMember();
            battle.NextMember();

            Assert.IsTrue(battle.Turn == 3);
            var attackInfo = battle.BrutalAttack(hero, zombie1, 0);
            
        }
    }
}