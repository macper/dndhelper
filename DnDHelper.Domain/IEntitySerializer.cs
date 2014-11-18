using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper.Domain
{
    public interface IEntitySerializer
    {
        string Serialize<T>( T entity ) where T:BaseEntity;
        T Deserialize<T>( string content ) where T : BaseEntity;
    }
}
