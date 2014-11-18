using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DnDHelper.Domain;

namespace DnDHelper.Domain
{
    public interface IRepositorySerializer
    {
        string Serialize<T>(Repository<T> repository) where T : BaseEntity;
        Repository<T> Deserialize<T>(string content) where T:BaseEntity;
    }
}
