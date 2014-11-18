using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DnDHelper.Domain;

namespace DnDHelper.Infrastructure
{
    public abstract class FileRepositoryStorage : IRepositoryStorage
    {
        public IGenericFilePathProvider FilePathProvider { get; set; }
        public IRepositorySerializer Serializer { get; set; }

        private string _appSettingsKeyName;
        private string _extension;

        public FileRepositoryStorage(IGenericFilePathProvider filePathProvider, IRepositorySerializer serializer, string appSettingKeyName, string extension)
        {
            FilePathProvider = filePathProvider;
            Serializer = serializer;
            _appSettingsKeyName = appSettingKeyName;
            _extension = extension;
        }

        
        protected string GetFilePath(Type repositoryType)
        {
            var basePath = ConfigurationManager.AppSettings[_appSettingsKeyName];
            var realPath = FilePathProvider.GetFilePath(basePath);

            if (string.IsNullOrEmpty(realPath) || !Directory.Exists(realPath))
            {
                realPath = "Repositories";
                Directory.CreateDirectory(realPath);
            }
            return Path.Combine(realPath, repositoryType.Name + "." + _extension);
        }

        public void SaveRepository<T>(Repository<T> repository) where T : BaseEntity
        {
             File.WriteAllText(GetFilePath(typeof(T)), Serializer.Serialize(repository), Encoding.UTF8);
        }

        public Repository<T> LoadRepository<T>() where T : BaseEntity
        {
            var path = GetFilePath(typeof(T));
            if (!File.Exists(path))
            {
                return new Repository<T>();
            }

            return Serializer.Deserialize<T>(File.ReadAllText(path));
        }
    }
}
