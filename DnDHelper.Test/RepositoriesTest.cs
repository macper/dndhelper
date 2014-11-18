using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using DnDHelper.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DnDHelper.Infrastructure;

namespace DnDHelper.Test
{
    [TestClass]
    public class RepositoriesTest : BaseTest
    {
        private string CreateRepositoryPath()
        {
            if (!Directory.Exists(ConfigurationManager.AppSettings["XmlRepositoryPath"]))
            {
                Directory.CreateDirectory(ConfigurationManager.AppSettings["XmlRepositoryPath"]);
            }
            return ConfigurationManager.AppSettings["XmlRepositoryPath"];
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            var sampleRepo = new SampleDataRepository();
            ServiceContainer.Kernel.Rebind<RepositorySet>().ToConstant(sampleRepo);
        }

        [TestMethod]
        public void SaveRepository()
        {
            CreateRepositoryPath();
            var sampleRepo = ServiceContainer.GetInstance<RepositorySet>();
            var testRepo = sampleRepo.Get<RaceDefinition>();
            //testRepo.Commit();

            var repository = ServiceContainer.GetInstance<IRepositoryStorage>();
            repository.SaveRepository(testRepo);

            Assert.IsTrue(File.Exists(Path.Combine(ConfigurationManager.AppSettings["XmlRepositoryPath"], "RaceDefinition.xml")));
        }

        [TestMethod]
        public void ClassRepoSave()
        {
            CreateRepositoryPath();
            var sampleRepo = ServiceContainer.GetInstance<RepositorySet>();
            var testRepo = sampleRepo.Get<ClassDefinition>();
            //testRepo.Commit();

            var repository = ServiceContainer.GetInstance<IRepositoryStorage>();
            repository.SaveRepository(testRepo);
        }
        
        [TestMethod]
        public void LoadRepository()
        {
            var repo = new Repository<Domain.ClassDefinition>();
            repo.Elements.Add(new Domain.ClassDefinition
                                  {
                                      Name = "Wojownik",
                                      PW = 10,
                                      AttackSkill = Domain.SkillRate.High,
                                      HighEnduranceThrow = true
                                  });
            var storage = ServiceContainer.GetInstance<IRepositoryStorage>();
            CreateRepositoryPath();
            //repo.Commit();
            storage.SaveRepository(repo);

            var newStorage = storage.LoadRepository<Domain.ClassDefinition>();
            Assert.IsTrue(newStorage.Elements.Count() == 1);
            Assert.IsTrue(newStorage.Elements[0].Name == "Wojownik");
        }

        private void CreateRepository<T>() where T:BaseEntity
        {
            CreateRepositoryPath();
            var repo = new Repository<T>();
            var storage = ServiceContainer.GetInstance<IRepositoryStorage>();
            storage.SaveRepository(repo);
        }

        [TestMethod]
        public void AtutDefinitionRepoTest()
        {
            var repo = new Repository<Domain.AtutDefinition>();
            repo.Elements.Add(new Domain.AtutDefinition
            {
                Name = "Skupienie na broni",
                Description = "Opis",
                Requirements = "Req",
                Script = "skupienie"
            });

            var storage = ServiceContainer.GetInstance<IRepositoryStorage>();
            CreateRepositoryPath();
            //repo.Commit();
            storage.SaveRepository(repo);

            var newStorage = storage.LoadRepository<Domain.AtutDefinition>();
            Assert.IsTrue(newStorage.Elements.Count() == 1);

            var el = newStorage.Elements.First();
            Assert.IsTrue(el.Name == "Skupienie na broni" && el.Description == "Opis" && el.Requirements == "Req" && el.Script == "skupienie");
        }
        
        [TestMethod]
        public void RepositorySetTest()
        {
            CreateRepository<Domain.RaceDefinition>();
            CreateRepository<Domain.ClassDefinition>();

            var repositorySet = new RepositorySet();
            repositorySet.Register<Domain.RaceDefinition>();
            repositorySet.Register<Domain.ClassDefinition>();

            Assert.IsTrue(repositorySet.Get<Domain.RaceDefinition>().Elements.Count == 0);
            Assert.IsTrue(repositorySet.Get<Domain.ClassDefinition>().Elements.Count == 0);

            repositorySet.Get<Domain.RaceDefinition>().Elements.Add(new Domain.RaceDefinition() { Name = "Gnom" });
            //repositorySet.Get<Domain.RaceDefinition>().Commit();

            var newRepositorySet = new RepositorySet();
            newRepositorySet.Register<Domain.RaceDefinition>();
            Assert.IsTrue(newRepositorySet.Get<Domain.RaceDefinition>().Elements.Count == 1 && newRepositorySet.Get<Domain.RaceDefinition>().Elements.First().Name == "Gnom");

        }


        [TestMethod]
        public void DefinitionRepositoryTest()
        {
            CreateRepositoryPath();
            var spellRepo = new Repository<Domain.SpellDefinition>();
            spellRepo.Elements.Add(new Domain.SpellDefinition()
                                       {
                                           Name = "Magiczny pocisk",
                                           SpellShool = Domain.SpellShool.Conjuration,
                                           Range = Domain.SpellRange.Long
                                       });
            spellRepo.Elements.Add(new Domain.SpellDefinition() { Name = "Cisza"});
            ServiceContainer.GetInstance<IRepositoryStorage>().SaveRepository(spellRepo);
            var repositorySet = new RepositorySet();
            repositorySet.Register(spellRepo);
            ServiceContainer.Kernel.Rebind<RepositorySet>().ToConstant(repositorySet);

            var group = new Domain.CharacterGroup() { Name = "Testowa grupa" };
            var character = new Domain.Character();
            var spellDef = repositorySet.Get<Domain.SpellDefinition>().GetElementByName("Magiczny pocisk");
            character.KnownSpellsNames.Add(spellDef.Name);
            var spell = spellDef.CreateItem();
            character.Spells.Add(spell);
            character.KnownSpellsNames.Add("Cisza");

            group.Characters.Add(character);

            var characterRepo = new Repository<Domain.CharacterGroup>();
            characterRepo.Elements.Add(group);
            repositorySet.Register(characterRepo);
            //characterRepo.Commit();

            characterRepo = ServiceContainer.GetInstance<IRepositoryStorage>().LoadRepository<Domain.CharacterGroup>();
            Assert.IsTrue(characterRepo.Elements.Count == 1);
            group = characterRepo.Elements.First();
            Assert.IsTrue(group.Name == "Testowa grupa");
            character = group.Characters.First();
            Assert.IsTrue(character.KnownSpells.Count() == 2);
            spellDef = character.KnownSpells.First();
            Assert.IsTrue(spellDef.SpellShool == Domain.SpellShool.Conjuration);
            spell = character.Spells.First();
            Assert.IsTrue(spell.Definition == spellDef);
        }

        [TestMethod]
        public void SampleRepositoryTest()
        {
            var repo = ServiceContainer.GetInstance<RepositorySet>();
            foreach (var repository in repo.GetAll())
            {
                //repository.Commit();
            }
        }
        
        [TestMethod]
        public void ConvertFromXmlToJson()
        {
            var xmlStorage = new XmlRepositoryStorage(ServiceContainer.GetInstance<IGenericFilePathProvider>());
            var repo = xmlStorage.LoadRepository<CharacterGroup>();
            var jsonStorage = new JsonRepositoryStorage(ServiceContainer.GetInstance<IGenericFilePathProvider>());
            jsonStorage.SaveRepository<CharacterGroup>(repo);

            //var repo2 = xmlStorage.LoadRepository<AtutDefinition>();
            //jsonStorage.SaveRepository<AtutDefinition>(repo2);

            //var repo3 = xmlStorage.LoadRepository<ClassDefinition>();
            //jsonStorage.SaveRepository<ClassDefinition>(repo3);

            //var repo4 = xmlStorage.LoadRepository<EffectDefinition>();
            //jsonStorage.SaveRepository<EffectDefinition>(repo4);

            //var repo5 = xmlStorage.LoadRepository<ItemDefinition>();
            //jsonStorage.SaveRepository<ItemDefinition>(repo5);

            //var repo6 = xmlStorage.LoadRepository<SkillDefinition>();
            //jsonStorage.SaveRepository<SkillDefinition>(repo6);

            //var repo7 = xmlStorage.LoadRepository<SpellDefinition>();
            //jsonStorage.SaveRepository<SpellDefinition>(repo7);
        }

        [TestMethod]
        public void CopyCharactersFromOldCharacterGroup()
        {
            var repo = ServiceContainer.GetInstance<RepositorySet>();
            var chG = repo.Get<CharacterGroup>();
            var ch = repo.Get<Character>();

            foreach (var g in chG.Elements)
            {
                ch.Elements.AddRange(g.Characters);
            }

            //ch.Commit();
        }
    }
}
