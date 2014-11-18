using DnDHelper.UpdateServiceAPI;
using System.ServiceModel;

namespace DnDHelper.UpdateServiceAPI
{
    public class UpdateServiceProxy : ClientBase<IUpdateService>, IUpdateService
    {
        public SyncResponse Synchronize( SyncRequest request )
        {
            return Channel.Synchronize( request );
        }
    }
}
