using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DnDHelper.Domain;

namespace DnDHelper.Infrastructure
{
    public class JsonSerializer : IRepositorySerializer
    {
        public string Serialize<T>(Repository<T> repository) where T : BaseEntity
        {
            var serializer = new ServiceStack.Text.JsonSerializer<Repository<T>>();
            return serializer.SerializeToString(repository);
        }

        public Repository<T> Deserialize<T>(string content) where T : BaseEntity
        {
            var serializer = new ServiceStack.Text.JsonSerializer<Repository<T>>();
            return serializer.DeserializeFromString(content);
        }
    }
}
