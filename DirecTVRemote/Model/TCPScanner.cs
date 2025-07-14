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
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Phone.Net.NetworkInformation;

namespace DirecTVRemote.Model {

	public class TCPScanner {

		public event EventHandler<PortScanEventArgs> ScanFinished;

		private List<string> _potentialBoxes = new List<string>();
		private int count = 0;
		private int total = 256;
		static ManualResetEvent _clientDone = new ManualResetEvent( false );

		private void ScanPort( object asdf ) {
			int i = (int)asdf;

			Socket socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

			string address = "192.168.40." + i.ToString();
			string request = "GET / HTTP/1.1\r\nHost: " + "192.168.40." + i.ToString() + "\r\nConnection: Close\r\n\r\n";
			int port = 8080;

			Byte[] bytesSent = Encoding.UTF8.GetBytes( request );
			Byte[] bytesReceived = new Byte[256];

			SocketAsyncEventArgs args = new SocketAsyncEventArgs();
			args.RemoteEndPoint = new DnsEndPoint( address, port );

			//args.Completed += new EventHandler<SocketAsyncEventArgs>( args_Completed );
			args.Completed += new EventHandler<SocketAsyncEventArgs>( delegate( object s, SocketAsyncEventArgs e ) {
				string result = e.SocketError.ToString();

				count++;

				if( result.ToLower() == "success" ) {
					_potentialBoxes.Add( e.RemoteEndPoint.ToString() );
				}



				//if( count >= total && ScanFinished != null ) {
				//    ScanFinished( this, new PortScanEventArgs( _potentialBoxes ) );
				//}
				_clientDone.Set();
			} );

			_clientDone.Reset();

			socket.ConnectAsync( args );
			_clientDone.WaitOne( 1000 );
			if( !socket.Connected ) {
				socket.Close();
			}
		}

		public void ScanPorts() {


			for( int i = 0; i < 256; i++ ) {
				Thread th = new Thread( ScanPort );
				th.Start( i );
			}

			while( count < 256 ) {
				Thread.Sleep( 100 );
			}

			ScanFinished( this, new PortScanEventArgs( _potentialBoxes ) );
		}

		void args_Completed( object sender, SocketAsyncEventArgs e ) {
			string result = e.SocketError.ToString();

			count++;

			if( result.ToLower() == "success" ) {
				_potentialBoxes.Add( e.RemoteEndPoint.ToString() );
			}

			if( count >= total && ScanFinished != null ) {
				ScanFinished( this, new PortScanEventArgs( _potentialBoxes ) );
			}
		}


	}

}
