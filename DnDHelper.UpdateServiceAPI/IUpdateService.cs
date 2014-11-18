using System.Collections;
using System.ServiceModel;

namespace DnDHelper.UpdateServiceAPI
{
    [ServiceContract]
    public interface IUpdateService
    {
        [OperationContract]
        SyncResponse Synchronize( SyncRequest request );
    }
}