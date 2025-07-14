using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectRemotePCL;

namespace DirectRemoteWp8.ViewModel {

	public class WhatsOnViewModel : NotifyingObject {

		private DateTime _unixEpochDate = new DateTime( 1970, 1, 1 );

		private string _boxIp;

		private string _boxName;

		private string _callsign;

		private string _channelNumber;

		private string _duration;

		private string _rating;

		private string _startTime;

		private string _title;


		public string BoxIp {
			get {
				return _boxIp;
			}

			set {
				_boxIp = value;
				OnPropertyChanged( "BoxIp" );
			}
		}

		public string BoxName {
			get {
				return _boxName;
			}

			set {
				_boxName = value;
				OnPropertyChanged( "BoxName" );
			}
		}

		public string Callsign {
			get {
				return _callsign;
			}

			set {
				_callsign = value;
				OnPropertyChanged( "Callsign" );
			}
		}

		public string ChannelNumber {
			get {
				return _channelNumber;
			}

			set {
				_channelNumber = value;
				OnPropertyChanged( "ChannelNumber" );
			}
		}

		public string Duration {
			get {
				return _duration;
			}

			set {
				_duration = value;
				OnPropertyChanged( "Duration" );
			}
		}

		public string Rating {
			get {
				return _rating;
			}

			set {
				_rating = value;
				OnPropertyChanged( "Rating" );
			}
		}

		public string StartTime {
			get {
				return _startTime;
			}

			set {
				_startTime = value;
				OnPropertyChanged( "StartTime" );
			}
		}

		public string Title {
			get {
				return _title;
			}

			set {
				_title = value;
				OnPropertyChanged( "Title" );
			}
		}

		public WhatsOnViewModel( ChannelStatus channelStatus ) {
			ChannelNumber = channelStatus.Major;
			Callsign = channelStatus.Callsign;
		}

		public WhatsOnViewModel( DirecTVBoxViewModel boxVm, ChannelStatus channelStatus ) {
			long duration;
			long startIntSecondsUtc;

			BoxIp = boxVm.Url;
			BoxName = boxVm.Name;
			Callsign = channelStatus.Callsign;
			ChannelNumber = channelStatus.Major;

			Rating = channelStatus.Rating;
			Title = channelStatus.Title;

			if( long.TryParse( channelStatus.Duration, out duration ) ) {
				Duration = (duration / 60).ToString() + " minutes";
			}
			else {
				Duration = "Unknown";
			}

			if( long.TryParse( channelStatus.StartTime, out startIntSecondsUtc ) ) {
				StartTime = _unixEpochDate.AddSeconds( startIntSecondsUtc ).ToLocalTime().ToShortTimeString();
			}
			else {
				StartTime = "Unknown";
			}
		}

	}

}
