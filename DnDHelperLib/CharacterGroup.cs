using System;
using System.Collections.Generic;

namespace DnDHelper
{
    [Serializable]
    public class CharacterGroup
    {
        public string GroupName { get; set; }
        public List<Character> Members { get; set; }

        public CharacterGroup()
        {
            Members = new List<Character>();
        }

        public void Serialize()
        {
            foreach (var character in Members)
            {
                character.Serialize();
            }
        }

        public void Deserialize()
        {
            foreach (var character in Members)
            {
                character.Deserialize();
            }
        }
    }
}