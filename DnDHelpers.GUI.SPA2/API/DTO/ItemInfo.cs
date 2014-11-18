using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelpers.GUI.SPA2.API.DTO
{
    public class ItemInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int Price { get; set; }
        public string Type { get; set; }
    }
}