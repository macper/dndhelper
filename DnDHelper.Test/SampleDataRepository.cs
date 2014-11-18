using System;
using System.Collections.Generic;
using DnDHelper.Domain;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.Test
{
    public class SampleDataRepository : RepositorySet
    {
        public SampleDataRepository()
        {
            var appSettingRepo = new Repository<AppSetting>();
            appSettingRepo.DefaultEntityProvider = new AppSettingDefaultValueProvider();
            Register(appSettingRepo);
        }

        public void Init()
        {
            RegisterRaces();
            RegisterClasses();
            RegisterMainSkills();
            RegisterAtutes();
            RegisterSpells();
            RegisterItems();
            RegisterEffects();
            RegisterScripts();
        }

        private void RegisterScripts()
        {
            var scriptRepo = new Repository<Script>();
            Register(scriptRepo);

            //atutRepo.Elements.Add( new AtutDefinition
            //{
            //    Name = "Skupienie na broni",
            //    Script = "ATUT_Skupienie"
            //} );
            //atutRepo.Elements.Add( new AtutDefinition
            //{
            //    Name = "Potê¿niejsze skupienie na broni",
            //    Script = "ATUT_PotezniejszeSkupienie"
            //} );
        }

        public void RegisterCharacters()
        {
            var characterRepo = new Repository<Domain.CharacterGroup>();
            var testGroup = new Domain.CharacterGroup() {Name = "Testowa grupa"};

            var hero = new Domain.Character() {Name = "Hero", Race = Get<RaceDefinition>().GetElementByName("Elf").CreateItem()};
            var @class = Get<ClassDefinition>().GetElementByName("Barbarzyñca").CreateItem();
            @class.Level = 10;
            hero.Class.Add(@class);
            var calculator = ServiceContainer.GetInstance<ICharacterCalculator>();
            calculator.CalculateInitialStats(hero);

            hero.Controller.EquipItem(Get<ItemDefinition>().GetElementByName("Miecz dwurêczny+2, Zguba Paj¹ka").CreateItem(), ItemPosition.RightHand);

            var zombie = new Domain.Character() {Name = "Zombie"};
            zombie.OriginalStats.AC = new AC() { ACForType = new List<ACForType>(new[] { new ACForType(ACBonusTypes.NaturalArmor, 4) }) };
            zombie.OriginalStats.HP = 30;
            zombie.OriginalStats.Initiative = 2;
            zombie.OriginalStats.Throws = new Domain.Throw(5, 1, 6);
            zombie.Attacks = new List<Domain.Attack>(new [] { new Domain.Attack() { Name = "£apy", ToHit = 6, NumberOfAttacks = 1, Damage = new Damage(new [] { new DamageBone(6,1), new DamageBone(1,3) })}});
            zombie.OriginalStats.Speed = 6;

            testGroup.Characters.AddRange(new [] { hero, zombie });
            characterRepo.Elements.Add(testGroup);
            Register(characterRepo);
        }


        private void RegisterEffects()
        {
            var effectRepo = new Repository<EffectDefinition>();
            var effect = new EffectDefinition
                             {
                                 Name = "Swoboda dzia³ania"
                             };
            effectRepo.Elements.Add(effect);
            effect = new EffectDefinition
                         {
                             Name = "Sza³"
                         };
            effect.Bonuses.Add(new MainSkillBonus("Sza³", 4) {Attribute = Domain.BaseAttribute.Strength});
            effect.Bonuses.Add(new MainSkillBonus("Sza³", 4) {Attribute = Domain.BaseAttribute.Constitution});
            effect.Bonuses.Add(new ACBonus("Sza³", ACBonusTypes.Other, -2));
            effectRepo.Elements.Add(effect);

            effect = new EffectDefinition
                         {
                             Name = "B³ogos³awieñstwo"
                         };
            effect.Bonuses.Add(new ThrowBonus("B³ogos³awieñstwo", new Domain.Throw(1, 1, 1)));
            effect.Bonuses.Add(new AttackBonus("B³ogos³awieñstwo", 1));
            effectRepo.Elements.Add(effect);

            effect = new EffectDefinition
            {
                Name = "Spowolnienie"
            };
            effect.Bonuses.Add(new SpeedBonus("Spowolnienie", -2, true));
            effectRepo.Elements.Add(effect);
            Register(effectRepo);
        }

        private void RegisterItems()
        {
            var itemRepo = new Repository<Domain.ItemDefinition>();
            var item = new ItemDefinition
                           {
                               BaseType = Domain.BaseTypes.TwoHandedWeapon,
                               Cost = 500,
                               Name = "Miecz dwurêczny+2, Zguba Paj¹ka",
                               PrototypeName = ItemPrototypes.TwoHandedSword
                           };
            item.Bonuses.Add(new DamageBonus(item.Name, new Damage(new [] { new DamageBone(6, 2), new DamageBone(1, 2)})));
            item.Bonuses.Add(new AttackBonus(item.Name, 2));
            item.Bonuses.Add(new AppendEffectBonus(item.Name, "Swoboda dzia³ania"));
            itemRepo.Elements.Add(item);

            item = new ItemDefinition
                       {
                           BaseType = Domain.BaseTypes.Helmet,
                           Cost = 1000,
                           Name = "He³m Baldurana",
                           PrototypeName = ItemPrototypes.Helmet
                       };
            item.Bonuses.Add(new ACBonus(item.Name, ACBonusTypes.MagicShield, 1));
            item.Bonuses.Add(new ThrowBonus(item.Name, new Domain.Throw { EnduranceThrow = 1, ReflexThrow = 1, WillThrow = 1}));
            item.Bonuses.Add(new HPBonus(item.Name, 12));
            itemRepo.Elements.Add(item);

            item = new ItemDefinition
                       {
                           BaseType = Domain.BaseTypes.Armor,
                           Cost = 300,
                           Name = "Skórzana æwiekowana+2",
                           PrototypeName = ItemPrototypes.StuddedLeatherArmor
                       };
            item.Bonuses.Add(new ACBonus(item.Name, ACBonusTypes.Armor, 3));
            item.Bonuses.Add(new ACBonus(item.Name, ACBonusTypes.MagicShield, 2));
            item.Bonuses.Add(new MaxDexterityBonus(item.Name, 4));
            item.Bonuses.Add(new PanaltyBonus(item.Name, -1));
            item.Bonuses.Add(new SecondarySkillBonus(item.Name, 2) { SkillName = "Ciche chodzenie" });
            itemRepo.Elements.Add(item);

            item = new ItemDefinition
                       {
                           BaseType = Domain.BaseTypes.Belt,
                           Cost = 400,
                           Name = "Pas si³y+1",
                           PrototypeName = ItemPrototypes.Belt
                       };
            item.Bonuses.Add(new MainSkillBonus(item.Name, 1) { Attribute = Domain.BaseAttribute.Strength});
            itemRepo.Elements.Add(item);

            item = new ItemDefinition
                       {
                           BaseType = Domain.BaseTypes.TwoHandedWeapon,
                           Cost = 800,
                           Name = "Ciê¿ka kusza celnoœci",
                           PrototypeName = ItemPrototypes.HeavyCrossbow
                       };
            item.Bonuses.Add(new DamageBonus(item.Name, new Damage(new[] {new DamageBone(12, 1), new DamageBone(1, 1)})));
            item.Bonuses.Add(new RangeBonus(item.Name, 200));
            itemRepo.Elements.Add(item);

            item = new ItemDefinition
                       {
                           BaseType = Domain.BaseTypes.Arrow,
                           Name = "Be³t",
                           PrototypeName = ItemPrototypes.Bolt,
                           IsPrototype = true
                       };
            itemRepo.Elements.Add(item);

            item = new ItemDefinition
            {
                BaseType = Domain.BaseTypes.Arrow,
                Name = "Be³t+1",
                PrototypeName = ItemPrototypes.Bolt
            };
            item.Bonuses.Add(new DamageBonus(item.Name, new Damage(new[] { new DamageBone(1, 1) })));
            item.Bonuses.Add(new AttackBonus(item.Name, 1));
            itemRepo.Elements.Add(item);

            item = new ItemDefinition
                       {
                           BaseType = Domain.BaseTypes.OneHandWeapon,
                           Name = "Miecz krótki",
                           PrototypeName = ItemPrototypes.ShortSword
                       };
            item.Bonuses.Add(new DamageBonus(item.Name, new Damage(new[] { new DamageBone(6, 1) })));
            itemRepo.Elements.Add(item);

            Register(itemRepo);
        }

        private void RegisterSpells()
        {
            var spellRepo = new Repository<Domain.SpellDefinition>();
            spellRepo.Elements.Add(new Domain.SpellDefinition()
            {
                Name = "Magiczny pocisk",
                SpellShool = Domain.SpellShool.Conjuration,
                Range = Domain.SpellRange.Long
            });
            spellRepo.Elements.Add(new Domain.SpellDefinition()
            {
                Name = "Cisza",
                SpellShool = Domain.SpellShool.Abjuration
            });
            Register(spellRepo);
        }

        private void RegisterAtutes()
        {
            var atutRepo = new Repository<Domain.AtutDefinition>();
            atutRepo.Elements.Add(new AtutDefinition
            {
                Name = "Odpornoœæ na zauroczenia"
            });
            atutRepo.Elements.Add(new AtutDefinition
            {
                Name = "Sza³"
            });
            atutRepo.Elements.Add(new AtutDefinition
                                      {
                                          Name = AtutDefinition.CommonAtutes.TwoWeaponsCombat
                                      });
            atutRepo.Elements.Add(new AtutDefinition
            {
                Name = "Nieœwiadomy unik"
            });
            atutRepo.Elements.Add(new AtutDefinition
            {
                Name = "Wyczucie pu³apek"
            });
            atutRepo.Elements.Add(new AtutDefinition
            {
                Name = "Redukcja obra¿eñ"
            });



            Register(atutRepo);
        }

        private void RegisterMainSkills()
        {
            var skillRepository = new Repository<Domain.SkillDefinition>();
            skillRepository.Elements.Add(new SkillDefinition
            {
                Name = "Nas³uchiwanie",
                BonusProperty = Domain.BaseAttribute.Wisdom
            });
            skillRepository.Elements.Add(new SkillDefinition
            {
                 Name = "Ciche chodzenie",
                 BonusProperty = Domain.BaseAttribute.Dexterity
            });
            skillRepository.Elements.Add(new SkillDefinition
                                             {
                                                 Name = "P³ywanie",
                                                 BonusProperty = Domain.BaseAttribute.Constitution
                                             });
            skillRepository.Elements.Add(new SkillDefinition
            {
                Name = "Skakanie",
                BonusProperty = Domain.BaseAttribute.Constitution
            });
            Register(skillRepository);
        }

        private void RegisterClasses()
        {
            var barbarian = new Domain.ClassDefinition
                                {
                                    AttackSkill = Domain.SkillRate.High,
                                    HighEnduranceThrow = true,
                                    HighReflexThrow = true,
                                    HighWillThrow = false,
                                    Script = "CLASS_Barbarian",
                                    Name = "Barbarzyñca",
                                    PW = 12,
                                    SkillsPoints = 3
                                };
            barbarian.SkillsPreference = new List<SkillPreference>(new [] 
            { 
                new SkillPreference { Name = "Nas³uchiwanie", Wage = 5 },
                new SkillPreference { Name = "P³ywanie", Wage = 3},
                new SkillPreference { Name = "Skakanie", Wage = 2}
            });
            var classRepo = new Repository<Domain.ClassDefinition>();
            classRepo.Elements.Add(barbarian);

            //var script = new Script(ScriptContext.Race)
            //                 {
            //                     Name = "CLASS_Barbarian",
            //                     Content = ScriptEngineTests.GetScriptContent("classBarbarian.py")
            //                 };
            //classRepo.Scripts.Add(script);
            Register(classRepo);

            var thief = new ClassDefinition
                            {
                                AttackSkill = Domain.SkillRate.Medium,
                                HighEnduranceThrow = false,
                                HighReflexThrow = true,
                                HighWillThrow = false,
                                Name = "Z³odziej",
                                PW = 6,
                                SkillsPoints = 10
                            };
            thief.SkillsPreference = new List<SkillPreference>(new []
                                                                   {
                                                                       new SkillPreference { Name = "Nas³uchiwanie", Wage = 5},
                                                                       new SkillPreference { Name = "Ciche chodzenie", Wage = 5}
                                                                    });
            classRepo.Elements.Add(thief);
        }

        private void RegisterRaces()
        {
            var raceRepo = new Repository<Domain.RaceDefinition>();

            var raceElf = new Domain.RaceDefinition
                              {
                                  Name = "Elf",
                                  Bonuses = new List<BaseBonus>(new[]
                                                                    {
                                                                        new MainSkillBonus(CommonSources.Race, 2) { Attribute = Domain.BaseAttribute.Dexterity },
                                                                        new MainSkillBonus(CommonSources.Race, -2) { Attribute = Domain.BaseAttribute.Constitution }
                                                                    }),
                                                                    BaseSpeed = 9
                              };
            raceElf.Bonuses.Add(new SecondarySkillBonus(CommonSources.Race, 2) { SkillName = "Nas³uchiwanie"} );
            raceElf.Bonuses.Add(new AtutBonus(CommonSources.Race, 0) { AtutName = "Odpornoœæ na zauroczenia" });

            var raceOrc = new Domain.RaceDefinition
                              {
                                  Name = "Pó³ork",
                                  Bonuses = new List<BaseBonus>(new[]
                                                                    {
                                                                        new MainSkillBonus(CommonSources.Race, 2) { Attribute = Domain.BaseAttribute.Strength },
                                                                        new MainSkillBonus(CommonSources.Race, -2) { Attribute = Domain.BaseAttribute.Inteligence },
                                                                        new MainSkillBonus(CommonSources.Race, -2) { Attribute = Domain.BaseAttribute.Charisma }
                                                                    }),
                                                                    BaseSpeed = 9
                              };

            raceRepo.Elements.AddRange(new[]
                                           {
                                               new Domain.RaceDefinition {Name = "Cz³owiek"},
                                               raceElf,
                                               raceOrc
                                           });
            Register(raceRepo);
        }
    }
}