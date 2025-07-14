using System.Runtime.Serialization;

namespace RemoteCommunication
{
    [DataContract]
    public class Status
    {

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "msg")]
        public string Msg { get; set; }

        [DataMember(Name = "query")]
        public string Query { get; set; }
    }
}
