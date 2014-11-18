using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DnDHelper.Domain
{
    public class ChangeTracker
    {
        private static ChangeTracker _instance;
        private bool _needSave;

        private static readonly ILogger Logger = ServiceContainer.GetInstance<ILogger>( new KeyValuePair<string, object>( Const.LoggerName, typeof( ChangeTracker ).Name ) );

        private long _lastSync;
        public long LastSync
        {
            get
            {
                return _lastSync;
            }
            set
            {
                _lastSync = value; _needSave = true;
            }
        }

        public List<RepositoryChange> RepositoryChanges { get; private set; }

        private const string XmlFilePath = "ChangeTracker.xml";

        public static ChangeTracker Instance
        {
            get
            {
                var fileProvider = ServiceContainer.GetInstance<IGenericFilePathProvider>();
                if( _instance == null )
                {
                    if( !File.Exists( fileProvider.GetFilePath( XmlFilePath ) ) )
                    {
                        _instance = new ChangeTracker();
                        Logger.Debug("File ChangeTracker.xml not found, creating new one");
                    }
                    else
                    {
                        try
                        {
                            var serializer = new XmlSerializer( typeof( ChangeTracker ) );
                            using( var fs = new FileStream( fileProvider.GetFilePath( XmlFilePath ), FileMode.Open ) )
                            {
                                _instance = (ChangeTracker)serializer.Deserialize( fs );
                            }
                        }
                        catch( Exception exc )
                        {
                            Logger.Error( "Error during deserialization", exc );
                            _instance = new ChangeTracker();
                        }
                    }
                }
                return _instance;
            }
        }

        public ChangeTracker()
        {
            RepositoryChanges = new List<RepositoryChange>();
        }

        public void SaveChanges()
        {
            try
            {
                if( !_needSave )   
                    return;

                var serializer = new XmlSerializer( typeof( ChangeTracker ) );
                var fileProvider = ServiceContainer.GetInstance<IGenericFilePathProvider>();
                using( var fs = new FileStream( fileProvider.GetFilePath( XmlFilePath ), FileMode.Create ) )
                {
                    serializer.Serialize( fs, Instance );  
                }

                _needSave = false;
                Logger.Info( "All changes saved" );
            }
            catch( Exception exc )
            {
                Logger.Error( "Error during serialization", exc );
            }
        }

        public void AddChange<T>( T entity, bool updateInsert ) where T : BaseEntity
        {
            var repo = ServiceContainer.GetInstance<RepositorySet>().GetByName( entity.GetType().Name );
            var repoChange = RepositoryChanges.SingleOrDefault( r => r.Name == repo.Name );
            if( repoChange == null )
            {
                repoChange = new RepositoryChange { Name = repo.Name };
                RepositoryChanges.Add( repoChange );
            }
            var existingEntity = repoChange.EntityChanges.SingleOrDefault( e => e.Id == entity.Id );
            {
                if( existingEntity == null )
                {
                    existingEntity = new EntityChange
                    {
                        Id = entity.Id.Value
                    };
                    repoChange.EntityChanges.Add( existingEntity );
                }
                existingEntity.IsUpdateInsert = updateInsert;
                existingEntity.TimeStamp = DateTime.Now.Ticks;
            }
            
            _needSave = true;
        }
    }

    public class EntityChange
    {
        [XmlAttribute]
        public bool IsUpdateInsert { get; set; }

        [XmlAttribute]
        public Guid Id { get; set; }

        [XmlAttribute]
        public long TimeStamp { get; set; }
    }

    public class RepositoryChange
    {
        public string Name { get; set; }

        public List<EntityChange> EntityChanges { get; private set; }

        public RepositoryChange()
        {
            EntityChanges = new List<EntityChange>();
        }
    }
}
