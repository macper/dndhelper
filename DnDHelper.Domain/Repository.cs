using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DnDHelper.Domain
{
    public abstract class Repository
    {
        public event EventHandler CommitOccured;

        public virtual void Commit( BaseEntity entity, bool updateInsert )
        {
            if( CommitOccured != null )
                CommitOccured( this, null );
        }

        public abstract string Name { get; }
        public abstract void DeserializeAndUpdate( string content, Type repoType );
        public abstract string Serialize();
        public abstract IEnumerable<BaseEntity> GetElements();
        public abstract void RemoveElement( BaseEntity entity );
        public abstract void AddElement( BaseEntity entity );
    }

    public class Repository<T> : Repository where T : BaseEntity
    {
        public List<T> Elements { get; set; }
        [XmlIgnore]
        public IDefaultEntityProvider<T> DefaultEntityProvider { get; set; }

        public Repository()
        {
            Elements = new List<T>();
        }

        public T SingleOrDefault( Func<T, bool> expression )
        {
            return Elements.SingleOrDefault( expression );
        }

        public T GetElementByName( string name )
        {
            try
            {
                var element = Elements.SingleOrDefault(e => e.Name == name);
                if (element == null && DefaultEntityProvider != null)
                {
                    element = DefaultEntityProvider.GetDefaultValue(name);
                    Elements.Add(element);
                }
                return element;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public T GetElementById( Guid id )
        {
            return Elements.SingleOrDefault( e => e.Id == id );
        }

        public override string Name
        {
            get { return typeof( T ).Name; }
        }



        public override void DeserializeAndUpdate( string content, Type repoType )
        {
            var newRepo = ServiceContainer.GetInstance<IRepositorySerializer>().Deserialize<T>( content );
            Elements = newRepo.Elements;
        }

        public override string Serialize()
        {
            return ServiceContainer.GetInstance<IRepositorySerializer>().Serialize( this );
        }

        public override IEnumerable<BaseEntity> GetElements()
        {
            return Elements.Cast<BaseEntity>().AsEnumerable();
        }

        public override void Commit( BaseEntity entity, bool updateInsert )
        {
            Elements.ForEach( e => e.Serialize() );
            if( entity != null )
            {
                ChangeTracker.Instance.AddChange( entity, updateInsert );
            }

            base.Commit( entity, updateInsert );
        }

        public override void RemoveElement( BaseEntity entity )
        {
            Elements.Remove( (T)entity );
        }

        public override void AddElement( BaseEntity entity )
        {
            Elements.Add( (T)entity );
        }
    }
}