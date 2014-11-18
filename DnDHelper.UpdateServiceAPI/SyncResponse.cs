using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DnDHelper.UpdateServiceAPI
{
    [DataContract]
    public class SyncResponse : OperationResponse
    {
        [DataMember]
        public IEnumerable<EntityUpdateInfo> Updates { get; set; }
    }
}
