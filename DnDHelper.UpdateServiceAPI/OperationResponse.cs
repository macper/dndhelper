using System.Runtime.Serialization;

namespace DnDHelper.UpdateServiceAPI
{
    [DataContract]
    public class OperationResponse
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public string Error { get; set; }
    }
}