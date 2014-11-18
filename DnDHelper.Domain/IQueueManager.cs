namespace DnDHelper.Domain
{
    public interface IQueueManager
    {
        void RepositorySaveRequest<T>(Repository<T> repository) where T:BaseEntity;
    }
}