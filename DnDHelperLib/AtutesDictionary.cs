using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DnDHelper
{
    public class AtutesDictionary
    {
        private Dictionary<int, Atut> _dictionary;
        public List<Atut> List { get { return _dictionary.Values.ToList(); } }

        public AtutesDictionary(string filePath)
        {
            _dictionary = new Dictionary<int, Atut>();
            XDocument doc = XDocument.Load(filePath);

            var atutes = from xml in doc.Descendants("Atute") select xml;
            foreach (var atute in atutes)
            {
                int key = int.Parse(atute.Attribute("ID").Value);
                string name = atute.Attribute("Name").Value;
                string description = atute.Descendants("Description").First().Value;
                string req = atute.Descendants("Requirements").First().Value;
                _dictionary.Add(key, new Atut() { ID = key, Name = name, Description = description, Requirements = req });

            }
        }

        public Atut this[int key]
        {
            get
            {
                return _dictionary[key];
            }
        }
    }
}