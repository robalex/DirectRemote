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
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Net.NetworkInformation;
using System.Net.NetworkInformation;
using NetworkInterface = Microsoft.Phone.Net.NetworkInformation.NetworkInterface;
using System.Device.Location;

namespace DirecTVRemote.View {
	public partial class MainPage : PhoneApplicationPage {

		public MainPage() {
			InitializeComponent();

			this.LayoutUpdated += new EventHandler( MainPage_LayoutUpdated );
			NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler( NetworkChange_NetworkAddressChanged );
		}

		private void SetMenuItemVisibility() {
			bool urlExists = !string.IsNullOrEmpty( Settings.Instance.DirecTVUrl );

			_remoteItem.IsEnabled			= urlExists;
			_compactRemoteItem.IsEnabled	= urlExists;
			_whatsOnItem.IsEnabled			= urlExists;
		}

		void NetworkChange_NetworkAddressChanged( object sender, EventArgs e ) {
			if( !InternetConnectionAvailable() ) {
				MessageBox.Show( "Your internet connection has been lost. Try again later." );
				_mainMenuBox.IsEnabled = false;
			}
			else {
				_mainMenuBox.IsEnabled = true;
			}
		}

		void MainPage_LayoutUpdated( object sender, EventArgs e ) {
			this.LayoutUpdated -= new EventHandler( MainPage_LayoutUpdated );

			SetMenuItemVisibility();

			if( !InternetConnectionAvailable() ) {
				_mainMenuBox.IsEnabled = false;
			}
		}

		protected override void OnNavigatedTo( System.Windows.Navigation.NavigationEventArgs e ) {
			base.OnNavigatedTo( e );

			SetMenuItemVisibility();

			_mainMenuBox.SelectedIndex = -1;
		}

		private void _mainMenuBox_SelectionChanged( object sender, SelectionChangedEventArgs e ) {
			if( _mainMenuBox.SelectedIndex == -1 ) {
				return;
			}

			if( ((ListBoxItem)_mainMenuBox.SelectedItem).Content.ToString() == "Settings" ) {
				NavigationService.Navigate( new Uri( "/View/SettingsView.xaml", UriKind.Relative ) );
			}
			else if( ((ListBoxItem)_mainMenuBox.SelectedItem).Content.ToString() == "Remote" ) {
				NavigationService.Navigate( new Uri( "/View/RemoteView.xaml", UriKind.Relative ) );
			}
			else if( ((ListBoxItem)_mainMenuBox.SelectedItem).Content.ToString() == "Compact Remote" ) {
				NavigationService.Navigate( new Uri( "/View/CompactRemoteView.xaml", UriKind.Relative ) );
			}
			else if( ((ListBoxItem)_mainMenuBox.SelectedItem).Content.ToString() == "What's On" ) {
				NavigationService.Navigate( new Uri( "/View/WhatsOnView.xaml", UriKind.Relative ) );
			}
			else if( ((ListBoxItem)_mainMenuBox.SelectedItem).Content.ToString() == "Help" ) {
				NavigationService.Navigate( new Uri( "/View/HelpView.xaml", UriKind.Relative ) );
			}
			else if( ((ListBoxItem)_mainMenuBox.SelectedItem).Content.ToString() == "Privacy" ) {
				NavigationService.Navigate( new Uri( "/View/PrivacyView.xaml", UriKind.Relative ) );
			}
			else if( ((ListBoxItem)_mainMenuBox.SelectedItem).Content.ToString() == "Feedback" ) {
				EmailComposeTask emailComposeTask = new EmailComposeTask();

				emailComposeTask.To			= "robalex@gmail.com";
				emailComposeTask.Body		= string.Empty;
				emailComposeTask.Subject	= "Direct Remote Feedback";
				emailComposeTask.Show();
			}
			else if( ((ListBoxItem)_mainMenuBox.SelectedItem).Content.ToString() == "I Hate Ads" ) {
				MarketplaceDetailTask task = new MarketplaceDetailTask();
				task.ContentIdentifier = "a808b0ff-4486-e011-986b-78e7d1fa76f8 ";
				task.ContentType = MarketplaceContentType.Applications;
				task.Show();
			}
		}

		private bool InternetConnectionAvailable() {
			if( !NetworkInterface.GetIsNetworkAvailable() ) {
				MessageBox.Show( "No wifi connection is available. Try again later." );
				return false;
			}

			return true;
		}
	}
}