using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DirectRemotePCL {
	
	public class CommunicationManager {

		private string	_direcTVUrl;
		private int		_index;

		public CommunicationManager( string direcTVUrl ) {
			_direcTVUrl = direcTVUrl;
			_index		= 0;
		}

		private async Task<HttpResponseMessage> SendGetRequest( string url ) {
			HttpClient client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
			client.DefaultRequestHeaders.Add( "user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)" );

			HttpResponseMessage response = await client.GetAsync( url );
			response.EnsureSuccessStatusCode();

			return response;
		}

		public async void SetChannel( string majorChannel ) {
			try {
				HttpResponseMessage response = await SendGetRequest( "http://" + _direcTVUrl + ":8080/tv/tune" + "?major=" + majorChannel );
			}
			catch( Exception e ) {

			}

			_index++;
		}

		public async void SetChannel( string majorChannel, string minorChannel ) {
			try {
				HttpResponseMessage response = await SendGetRequest( "http://" + _direcTVUrl + ":8080/tv/tune" + "?major=" + majorChannel + "&minor=" + minorChannel );
			}
			catch( Exception e ) {

			}

			_index++;
		}

		public async void BoxExists( string url ) {
			try {
				HttpResponseMessage response = await SendGetRequest( "http://" + url + ":8080/tv/getTuned" );

				string result = response.Content.ReadAsStringAsync().Result;
				ChannelStatus status = Newtonsoft.Json.JsonConvert.DeserializeObject<ChannelStatus>( result );

				if( status.Status.Code != "200" ) {
					//BoxExistsCompleted( null, new BoxAddEventArgs( false, string.Empty, string.Empty ) );
				}
				else {
					//BoxExistsCompleted( null, new BoxAddEventArgs( true, string.Empty, string.Empty ) );
				}
			}
			catch( Exception e ) {
				//BoxExistsCompleted( null, new BoxAddEventArgs( false, string.Empty, string.Empty ) );
			}

			_index++;
		}

		public async Task<ChannelStatus> GetTuned() {
			try {
				HttpResponseMessage response = await SendGetRequest( "http://" + _direcTVUrl + ":8080/tv/getTuned?callback=dummy" + _index );

				string result = response.Content.ReadAsStringAsync().Result;
				int index = result.IndexOf( '(' );
				result = result.Substring( index + 1 ).TrimEnd( ';' ).TrimEnd( ')' );

				ChannelStatus status = Newtonsoft.Json.JsonConvert.DeserializeObject<ChannelStatus>( result );
				return status;
			}
			catch( Exception e ) {
				return null;
			}

			_index++;
		}

		public async void GetProgInfo( string major ) {
			try {
				HttpResponseMessage response = await SendGetRequest( "http://" + _direcTVUrl + ":8080/tv/getProgInfo?major=" + major + "&callback=dummy" + _index );

				string result = response.Content.ReadAsStringAsync().Result;
				int index = result.IndexOf( '(' );
				result = result.Substring( index + 1 ).TrimEnd( ';' ).TrimEnd( ')' );

				ChannelStatus status = JsonConvert.DeserializeObject<ChannelStatus>( result );
				if( status.Status.Code != "200" ) {
					//GetTunedCompleted( this, new GetTunedEventArgs() );
				}
				else {
					//GetTunedCompleted( this, new GetTunedEventArgs( status ) );
				}
			}
			catch( Exception e ) {

			}

			_index++;
		}

		/// <summary>
		/// Windows phone caches responses. There's no way to turn this off. Therefore, we add a dummy parameter
		/// that changes on each request.
		/// </summary>
		/// <param name="button">The button that was pressed</param>
		public async void SimulateRemoteButton( RemoteButton button ) {
			try {
				string buttonText = StringValueAttribute.GetStringValue( button );

				string url = "http://" + _direcTVUrl + ":8080/remote/processKey" + "?key=" + buttonText +
							 "&hold=keyPress&callback=dummy" + _index;

				HttpResponseMessage response = await SendGetRequest( url );
			}
			catch( Exception e ) {

			}

			_index++;
		}

	

		//GetTunedEventArgs GetBadChannelStatus() {
		//	ChannelStatus channelStatus = new ChannelStatus();
		//	Status status = new Status();
		//	status.Code = "400";
		//	status.Msg = "Unable to connect to the specified DirecTV box. Please check your settings.";
		//	channelStatus.Status = status;

		//	return new GetTunedEventArgs( channelStatus );
		//}

	}

}
