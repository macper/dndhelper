using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DnDHelper.Domain;
using ServiceStack.Text;

namespace DnDHelper.Infrastructure
{
    public class JsonRepositoryStorage : FileRepositoryStorage, IRepositoryStorage
    {

        public JsonRepositoryStorage(IGenericFilePathProvider filePathProvider)
            : base(filePathProvider, new JsonSerializer(), "JsonRepositoryPath", "json")
        { }
    }
}
