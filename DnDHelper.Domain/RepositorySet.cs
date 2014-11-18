using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;

namespace DnDHelper.Domain
{
    public class RepositorySet
    {
        private Dictionary<Type, Repository> All { get; set; }
        private static ILogger Logger = ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>(Const.LoggerName, typeof(RepositorySet).Name));
        private readonly Lazy<IQueueManager> _queueManager = new Lazy<IQueueManager>(ServiceContainer.GetInstance<IQueueManager>);

        public virtual Repository<T> Get<T>() where T:BaseEntity
        {
            var type = typeof (T);
            if (!All.ContainsKey(type))
                throw new InstanceNotFoundException("Nie zarejestrowano repozytorium: " + type);
            return All[type] as Repository<T>;
        }

        public RepositorySet()
        {
            All = new Dictionary<Type, Repository>();
        }

        public void Register<T>(IDefaultEntityProvider<T> defaultEntityProvider = null) where T:BaseEntity
        {
            var storage = ServiceContainer.GetInstance<IRepositoryStorage>();
            var repository = storage.LoadRepository<T>();
            if (repository == null)
                repository = new Repository<T>();

            repository.CommitOccured += (s, e) => _queueManager.Value.RepositorySaveRequest((Repository<T>)s);
            All.Add(typeof (T), repository);
            if (defaultEntityProvider != null)
            {
                repository.DefaultEntityProvider = defaultEntityProvider;
            }
            Logger.Info("Zarejestrowano i wczytano repozytorium: " + typeof(T));
        }

        public void Register<T>(Repository<T> repository) where T:BaseEntity
        {
            repository.CommitOccured += (s, e) => _queueManager.Value.RepositorySaveRequest((Repository<T>)s);
            All.Add(typeof(T), repository);
            Logger.Info("Zarejestrowano repozytorium: " + typeof(T));
        }

        public static RepositorySet CreateDefault()
        {
            var repo = new RepositorySet();
            repo.Register<AppSetting>(new AppSettingDefaultValueProvider());
            repo.Register<RaceDefinition>();
            repo.Register<ClassDefinition>();
            repo.Register<EffectDefinition>();
            repo.Register<SpellDefinition>();
            repo.Register<SkillDefinition>();
            repo.Register<AtutDefinition>();
            repo.Register<ItemDefinition>();
            repo.Register<CharacterGroup>();
            repo.Register<Character>();
            repo.Register<Experience>();
            repo.Register<Script>();
            return repo;
        }

        public virtual Repository GetByName(string name)
        {
            return All.Values.SingleOrDefault(s => s.Name == name);
        }

        public virtual IEnumerable<Repository> GetAll()
        {
            return All.Values;
        }
    }
}