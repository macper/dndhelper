using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DnDHelper.Domain;

namespace DnDHelper.Infrastructure
{
    public class XmlRepositorySerializer : IRepositorySerializer
    {
        public string Serialize<T>(Domain.Repository<T> repository) where T : Domain.BaseEntity
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Repository<T>));
                serializer.Serialize(stream, repository);
                stream.Position = 0;
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public Domain.Repository<T> Deserialize<T>(string content) where T : Domain.BaseEntity
        {
            using (var stream = new MemoryStream())
            {
                var buffer = Encoding.UTF8.GetBytes(content);
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Repository<T>));
                return (Repository<T>)serializer.Deserialize(stream);
            }
        }
    }
}
