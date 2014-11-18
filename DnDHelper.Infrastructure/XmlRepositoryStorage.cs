using System;
using System.Configuration;
using System.IO;
using System.Text;
using DnDHelper.Domain;

namespace DnDHelper.Infrastructure
{
    public class XmlRepositoryStorage : FileRepositoryStorage, IRepositoryStorage
    {
        public XmlRepositoryStorage(IGenericFilePathProvider filePathProvider)
            : base(filePathProvider, new DnDHelper.Infrastructure.XmlRepositorySerializer(), "XmlRepositoryPath", "xml")
        { }
    }

    
}