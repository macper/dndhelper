using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace DnDHelper.Domain
{
    public static class DarkTemplar
    {
        public static T DeepClone<T>(T obj, bool useXmlSerializer = false)
        {
            using (var ms = new MemoryStream())
            {
                if (useXmlSerializer)
                {
                    var formatter = new XmlSerializer(typeof(T));
                    formatter.Serialize(ms, obj);
                    ms.Position = 0;
                    return (T)formatter.Deserialize(ms);
                }
                else
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, obj);
                    ms.Position = 0;
                    return (T)formatter.Deserialize(ms);
                }
            }
        }

        public static void AssertNotNull( object value, string name )
        {
            if( value == null )
                throw new ArgumentNullException( string.Format( "Variable {0} should not be null!", name ) );
        }
    }
}
