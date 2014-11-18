using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DnDHelper.UpdateServiceAPI
{
    [DataContract]
    public class SyncRequest
    {
        [DataMember]
        public long SyncTime { get; set; }

        [DataMember]
        public IEnumerable<EntityUpdateInfo> Updates { get; set; }
    }

    [DataContract]
    public class EntityUpdateInfo
    {
        [DataMember]
        public string RepositoryName { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public bool UpdateInsert { get; set; }

        [DataMember]
        public long LastChange { get; set; }
    }
}
