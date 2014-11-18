using System;
using System.Xml.Serialization;

namespace DnDHelper
{
    [Serializable]
    public class Effect
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public bool IsPermanent { get { return Duration == null; } }
    }
}