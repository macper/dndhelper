using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain.Bonuses;

namespace DnDHelpers.GUI.SPA2.API.DTO
{

    public class BonusInfo
    {
        public string Type { get; set; }

        public BaseBonus Prototype { get; set; }

        public int AdditionalRequest { get; set; }
    }

    public static class AdditionalDataLoadRequest
    {
        public static readonly int ACTypes = 1;
        public static int Effects = 2;
        public static int Atutes = 3;
        public static int Damages = 4;
        public static int MainSkills = 5;
        public static int Skills = 6;
    }
}