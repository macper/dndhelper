using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DnDHelper.Domain;

namespace DnDHelpers.GUI.SPA2
{
    public class WebPathProvider : IGenericFilePathProvider
    {
        public string GetFilePath(string basePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, basePath);
        }
    }
}