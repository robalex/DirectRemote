using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DirecTVRemote.Model {
	public class GetChannelHelper {

		private int _index = 0;
		private int _getChannelIndex = 200;

		public void GetTunedAsync() {
			string			url		= string.Format( "http://" + Settings.Instance.DirecTVUrl + ":8080/tv/getTuned?callback=dummy{0}", _getChannelIndex );

			WebClient client = new WebClient();
			client.DownloadStringCompleted += new DownloadStringCompletedEventHandler( client_DownloadStringCompleted );
			client.DownloadStringAsync( new Uri( url ) );
		}

		void client_DownloadStringCompleted( object sender, DownloadStringCompletedEventArgs e ) {
			ChannelStatus	status	= null;

			StreamReader reader = new StreamReader( e.Result );
			string response = reader.ReadToEnd();
			response = response.Remove( 0, ("dummy" + _getChannelIndex).Length ).Trim();
			response = response.Substring( 1, response.Length - 3 );

			byte[] responseBytes = Encoding.UTF8.GetBytes( response );
			MemoryStream ms = new MemoryStream( responseBytes );

			try {
				DataContractJsonSerializer serializer = new DataContractJsonSerializer( typeof( ChannelStatus ) );
				status = (ChannelStatus)serializer.ReadObject( ms );
			}
			catch( Exception exception ) {

			}
			_getChannelIndex++;
		}

        //public async Task<ChannelStatus[]> GetProgramInfoAsync( int majorChanStart, int count ) {

        //    DateTime startTime = DateTime.Now;
        //    List<ChannelStatus> statuses = new List<ChannelStatus>();

        //    WebClient client = new WebClient();

        //    for( int i = majorChanStart; i < majorChanStart + count; i++ ) {
        //        int index = _index;
        //        string url = string.Format( "http://" + Settings.Instance.DirecTVUrl + ":8080/tv/getProgInfo?major={0}&callback=dummy{1}", i, index );
        //        string response;

        //        try {
        //            response = await
        //            client.DownloadStringTaskAsync( url );
        //        }
        //        catch( Exception e ) {
        //            continue;
        //        }

        //        response = response.Remove( 0, ("dummy" + index).Length ).Trim();
        //        response = response.Substring( 1, response.Length - 3 );

        //        byte[] responseBytes = Encoding.UTF8.GetBytes( response );
        //        MemoryStream ms = new MemoryStream( responseBytes );

        //        try {
        //            DataContractJsonSerializer serializer = new DataContractJsonSerializer( typeof( ChannelStatus ) );
        //            ChannelStatus status = (ChannelStatus)serializer.ReadObject( ms );

        //            statuses.Add( status );
        //        }
        //        catch( Exception e ) {
					
        //        }

        //        _index++;
        //    }

        //    DateTime endTime = DateTime.Now;

        //    TimeSpan totalTime = endTime.Subtract( startTime );


        //    return statuses.ToArray();
        //}

	}
}
