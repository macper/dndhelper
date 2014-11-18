using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DnDHelper.Domain;
using log4net;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;

namespace DnDHelper.BriefService
{
    public class BriefService : Service
    {
        private static List<CharacterBrief> _characters  = new List<CharacterBrief>();
        private static List<EnemyBrief> _enemyBriefs = new List<EnemyBrief>();
        private static int _changeId;
        private static ILog Logger = LogManager.GetLogger(typeof (BriefService));

        public BriefInfoResponse Post(BriefInfoRequest request)
        {
            Logger.Debug("BriefInfoRequest called");
            try
            {
                if (!request.PartyMembers.Any() && !request.EnemyMembers.Any())
                {
                    Logger.Debug("Return all members");
                    return new BriefInfoResponse
                    {
                        Characters = _characters,
                        Enemies = _enemyBriefs
                    };
                }
                var chList = new List<CharacterBrief>();
                foreach (var character in request.PartyMembers)
                {
                    var existingChar = _characters.FirstOrDefault(c => c.Name == character.Name);
                    if (existingChar == null)
                    {
                        chList.Add(new CharacterBrief
                        {
                            Name = character.Name,
                            TypeOfChange = TypeOfChange.Delete
                        });
                        continue;
                    }
                    if (character.ChangeId != existingChar.ChangeId)
                    {
                        var clone = DarkTemplar.DeepClone(existingChar, true);
                        clone.TypeOfChange = TypeOfChange.Update;
                        chList.Add(clone);
                    }

                }
                chList.AddRange(_characters.Where(c => !request.PartyMembers.Select(m => m.Name).Contains(c.Name)));

                var enList = new List<EnemyBrief>();
                foreach (var enemy in request.EnemyMembers)
                {
                    var existingChar = _enemyBriefs.FirstOrDefault(c => c.Name == enemy.Name);
                    if (existingChar == null)
                    {
                        enList.Add(new EnemyBrief
                        {
                            Name = enemy.Name,
                            TypeOfChange = TypeOfChange.Delete
                        });
                        continue;
                    }
                    if (enemy.ChangeId != existingChar.ChangeId)
                    {
                        var clone = DarkTemplar.DeepClone(existingChar, true);
                        clone.TypeOfChange = TypeOfChange.Update;
                        enList.Add(clone);
                    }
                }
                enList.AddRange(_enemyBriefs.Where(c => !request.EnemyMembers.Select(m => m.Name).Contains(c.Name)));
                Logger.Debug("BriefInfoRequest - OK");

                return new BriefInfoResponse
                {
                    Characters = chList,
                    Enemies = enList
                };
            }
            catch (Exception exc)
            {
                Logger.Error(exc.Message, exc);
                throw;
            }
            
        }

        public void Post(BriefUpdateRequest request)
        {
            try
            {
                _changeId++;
                Logger.Debug("BriefUpdate request - " + _changeId);
                Logger.Debug(JsonSerializer.SerializeToString(request));
                foreach (var chBrief in request.Characters)
                {
                    chBrief.ChangeId = _changeId;
                    if (chBrief.TypeOfChange == TypeOfChange.Insert)
                    {
                        _characters.Add(chBrief);
                    }
                    else if (chBrief.TypeOfChange == TypeOfChange.Delete)
                    {
                        _characters.RemoveAll(c => c.Id == chBrief.Id);
                    }
                    else
                    {
                        var existing = _characters.FirstOrDefault(c => c.Id == chBrief.Id);
                        if (existing == null)
                        {
                            Logger.Warn("Character of ID: " + chBrief.Id + " not exists, something went terrible wrong!");
                            continue;
                        }
                        _characters.Remove(existing);
                        chBrief.TypeOfChange = TypeOfChange.Insert;
                        _characters.Add(chBrief);
                    }
                    _characters = _characters.OrderBy(o => o.Name).ToList();
                }
                foreach (var enemyCharacter in request.EnemyCharacters)
                {
                    enemyCharacter.ChangeId = _changeId;
                    if (enemyCharacter.TypeOfChange == TypeOfChange.Insert)
                    {
                        _enemyBriefs.Add(enemyCharacter);
                    }
                    else if (enemyCharacter.TypeOfChange == TypeOfChange.Delete)
                    {
                        _enemyBriefs.RemoveAll(e => e.Name == enemyCharacter.Name);
                    }
                    else
                    {
                        var existing = _enemyBriefs.FirstOrDefault(c => c.Name == enemyCharacter.Name);
                        if (existing == null)
                        {
                            Logger.Warn("Enemy of name: " + enemyCharacter.Name + " not exists, something went terrible wrong!");
                            continue;
                        }
                        _enemyBriefs.Remove(existing);
                        enemyCharacter.TypeOfChange = TypeOfChange.Insert;
                        _enemyBriefs.Add(enemyCharacter);
                    }
                    _enemyBriefs = _enemyBriefs.OrderBy(o => o.Name).ToList();
                }
                if (request.RemoveAllEnemies)
                {
                    _enemyBriefs.Clear();
                }
                Logger.Debug("BriefUpdateRequest - OK");
            }
            catch (Exception exc)
            {
                Logger.Error(exc.Message, exc);
                throw;
            }
            
        }
    }

}