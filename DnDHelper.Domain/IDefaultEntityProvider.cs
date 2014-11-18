using System;

namespace DnDHelper.Domain
{
    public interface IDefaultEntityProvider<out T> where T:BaseEntity
    {
        T GetDefaultValue(string name);
    }
}