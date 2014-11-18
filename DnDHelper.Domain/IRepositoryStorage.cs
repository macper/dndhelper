using System;
namespace DnDHelper.Domain
{
    public interface IRepositoryStorage
    {
        void SaveRepository<T>(Repository<T> repository) where T:BaseEntity;
        Repository<T> LoadRepository<T>() where T:BaseEntity;
    }
}