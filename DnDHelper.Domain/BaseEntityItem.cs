using System;

namespace DnDHelper.Domain
{
    [Serializable]
    public abstract class BaseEntityItem : BaseEntity
    {

    }

    [Serializable]
    public abstract class BaseEntityItem<T> : BaseEntityItem where T:BaseEntityDefinition
    {
        public T Definition
        {
            get
            {
                return ServiceContainer.GetInstance<RepositorySet>().Get<T>().GetElementByName(Name);
            }
        }
    }
}