using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace DnDHelper
{
    public class Atut
    {
        public string Name { get; set; }
        public string AdditionalInfo { get; set; }
        public int ID { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; } 
    }
}
