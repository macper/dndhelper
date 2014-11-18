using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelpers.GUI.SPA2.API.DTO.Spells
{
    public class SpellsInfoResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int Level { get; set; }

        public string Type { get; set; }
    }
}