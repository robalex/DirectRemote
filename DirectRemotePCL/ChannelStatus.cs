using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DirectRemotePCL {

	[DataContract]
	public class ChannelStatus {

		[DataMember( Name = "callsign" )]
		public string Callsign { get; set; }

		[DataMember( Name = "duration" )]
		public string Duration { get; set; }

		[DataMember( Name = "isOffAir" )]
		public string IsOffAir { get; set; }

		[DataMember( Name = "isPclocked" )]
		public string IsPclocked { get; set; }

		[DataMember( Name = "isPpv" )]
		public string IsPpv { get; set; }

		[DataMember( Name = "isRecording" )]
		public string IsRecording { get; set; }

		[DataMember( Name = "isVod" )]
		public string IsVod { get; set; }

		[DataMember( Name = "major" )]
		public string Major { get; set; }

		[DataMember( Name = "minor" )]
		public string Minor { get; set; }

		[DataMember( Name = "programId" )]
		public string ProgramId { get; set; }

		[DataMember( Name = "rating" )]
		public string Rating { get; set; }

		[DataMember( Name = "startTime" )]
		public string StartTime { get; set; }

		[DataMember( Name = "stationId" )]
		public string StationId { get; set; }

		[DataMember( Name = "status" )]
		public Status Status { get; set; }

		[DataMember( Name = "title" )]
		public string Title { get; set; }
	}

}
