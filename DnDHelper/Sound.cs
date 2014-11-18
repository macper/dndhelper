using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace DnDHelper
{
    public class Sound
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public static List<Sound> LoadState()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Sounds.xml"))
            {
                using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Sounds.xml", FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Sound>));
                    return (List<Sound>)xmlSerializer.Deserialize(fs);
                }
            }
            return new List<Sound>();
        }

        public static void SaveState(List<Sound> list)
        {
            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Sounds.xml", FileMode.OpenOrCreate))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Sound>));
                serializer.Serialize(fs, list);
            }
        }
    }
}
