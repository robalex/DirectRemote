using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DirectRemotePCL {

	[DataContract]
	public class Status {

		[DataMember( Name = "code" )]
		public string Code { get; set; }

		[DataMember( Name = "msg" )]
		public string Msg { get; set; }

		[DataMember( Name = "query" )]
		public string Query { get; set; }
	}

}
