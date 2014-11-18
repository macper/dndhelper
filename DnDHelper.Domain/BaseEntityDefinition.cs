using System;

namespace DnDHelper.Domain
{
    public abstract class BaseEntityDefinition : BaseEntity
    {
    }

    public abstract class BaseEntityDefinition<T> : BaseEntityDefinition where T:BaseEntityItem, new()
    {
        public virtual T CreateItem()
        {
            var newItem = new T();
            newItem.Name = Name;
            return newItem;
        }
    }
}