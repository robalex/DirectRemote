using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using DirecTVRemote.Model;
using DirecTVRemote.ViewModel;
using System.Device.Location;

namespace DirecTVRemote.View {
	public partial class WhatsOnView : PhoneApplicationPage {

		public WhatsOnView() {
			InitializeComponent();
			this.LayoutUpdated += new EventHandler( WhatsOnView_LayoutUpdated );
		}

		void WhatsOnView_LayoutUpdated( object sender, EventArgs e ) {
			this.LayoutUpdated -= WhatsOnView_LayoutUpdated;

			_progressBar.Visibility = Visibility.Visible; 
			
			Tuner tuner = new Tuner();
			tuner.GetTunedCompleted += GetTuned_Completed;
			tuner.GetTuned();
		}

		void GetTuned_Completed( object sender, GetTunedEventArgs e ) {
			if( e.ChannelStatus.Status.Code != "200" ) {
				MessageBox.Show( "Unable to connect to the specified DirecTV Box. Please check your settings." );
				NavigationService.GoBack();
			}

			_progressBar.Visibility = Visibility.Collapsed; 
			
			DirecTVBoxViewModel dtvvm = null;
			string url = Settings.Instance.DirecTVUrl;

			foreach( DirecTVBoxViewModel vm in Settings.Instance.DirecTVUrls ) {
				if( url == vm.Url ) {
					dtvvm = vm;
					break;
				}
			}

			if( dtvvm != null ) {
				WhatsOnViewModel wovm = new WhatsOnViewModel( dtvvm, e.ChannelStatus );
				this.DataContext = wovm;
			}
		}

	}
}