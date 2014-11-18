using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelpers.GUI.SPA2.API.DTO.Spells
{
    public class SpellDetails
    {
        public SpellInstance SpellInstance { get; set; }

        public IEnumerable<DictionaryItem> DictSchools { get; set; }

        public IEnumerable<DictionaryItem> DictTypes { get; set; }

        public IEnumerable<DictionaryItem> DictRanges { get; set; }
    }


    [Route( "/api/spell", "POST" )]
    [Route( "/api/spell", "DELETE" )]
    [Authenticate]
    [RequiredRole( "Admin" )]
    public class SpellInstance
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DictionaryItem School { get; set; }

        public int Level { get; set; }

        public DictionaryItem Range { get; set; }

        public string Description { get; set; }

        public IEnumerable<DictionaryItem> Type { get; set; }
    }
}