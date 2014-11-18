using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;
using DnDHelpers.GUI.SPA2.API.DTO;
using ServiceStack.ServiceInterface;

namespace DnDHelpers.GUI.SPA2.API
{
    public class DictionaryService : Service
    {
        public RepositorySet Repository { get; set; }

        public IEnumerable<EffectDefinition> Get(EffectsRequest request)
        {
            return Repository.Get<EffectDefinition>().Elements.OrderBy(o => o.Name).ToArray();    
        }

        public IEnumerable<AtutDefinition> Get(AtutesRequest request)
        {
            return Repository.Get<AtutDefinition>().Elements.OrderBy(o => o.Name).ToArray();
        }

        public IEnumerable<EnumDictionaryEntry> Get(DamageTypesRequest request)
        {
            return EnumsDictionary.DamageTypes;
        }

        public IEnumerable<EnumDictionaryEntry> Get(MainSkillsRequest request)
        {
            return EnumsDictionary.MainSkills;
        }

        public IEnumerable<SkillDefinition> Get(SkillsRequest request)
        {
            return Repository.Get<SkillDefinition>().Elements.OrderBy(o => o.Name).ToArray();
        }

        public Damage Get(DamageParseRequest request)
        {
            Damage outDmg = null;
            if (!Damage.TryParse(request.Value.Replace('*', '+'), out outDmg))
            {
                throw new ApplicationException("Nie udało się sparsować wartości damage");
            }
            return outDmg;
        }
    }
}