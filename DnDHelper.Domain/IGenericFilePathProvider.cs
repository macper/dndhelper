using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper.Domain
{
    public interface IGenericFilePathProvider
    {
        string GetFilePath(string basePath);
    }

    public class SimplePathProvider : IGenericFilePathProvider
    {
        public string GetFilePath(string basePath)
        {
            return basePath;
        }
    }
}
