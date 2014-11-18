using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;

namespace DnDHelper.Domain
{
    [Serializable]
    public class CharacterGroup : BaseEntity
    {
        [XmlIgnore]
        public List<Character> Characters
        {
            get
            {
                return ServiceContainer.GetInstance<RepositorySet>().Get<Character>().Elements.Where(e => e.GroupName == Name).ToList();
            }
        }

        public CharacterGroup()
        {
        }

    }
}