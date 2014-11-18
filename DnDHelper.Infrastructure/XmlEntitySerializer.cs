using DnDHelper.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DnDHelper.Infrastructure
{
    public class XmlEntitySerializer : IEntitySerializer
    {
        public T Deserialize<T>( string content ) where T : BaseEntity
        {
            using( var stream = new MemoryStream() )
            {
                var buffer = Encoding.UTF8.GetBytes( content );
                stream.Write( buffer, 0, buffer.Length );
                stream.Position = 0;
                var serializer = new System.Xml.Serialization.XmlSerializer( typeof( T ) );
                return (T)serializer.Deserialize( stream );
            }
        }

        public string Serialize<T>( T entity ) where T : BaseEntity
        {
            using( var stream = new MemoryStream() )
            {
                var serializer = new System.Xml.Serialization.XmlSerializer( typeof( T ) );
                serializer.Serialize( stream, entity );
                stream.Position = 0;
                return Encoding.UTF8.GetString( stream.ToArray() );
            }
        }
    }
}
