using System;

namespace DnDHelper.Domain
{
    public class SimpleQueueManager : IQueueManager
    {
        public void RepositorySaveRequest<T>(Repository<T> repository) where T : BaseEntity
        {
            ServiceContainer.GetInstance<IRepositoryStorage>().SaveRepository(repository);
            ChangeTracker.Instance.SaveChanges();
        }
    }
}