using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DnDHelper
{
    [Serializable]
    public class ClassesDefinition
    {
        public List<Class> ClassList;

        public ClassesDefinition()
        {
            ClassList = new List<Class>();
        }

        [XmlIgnore]
        public Class this[string name]
        {
            get
            {
                return ClassList.FirstOrDefault(f => f.Name == name);
            }
            set
            {
                value.Name = name;
                ClassList.Add(value);
            }
        }
    }
}
