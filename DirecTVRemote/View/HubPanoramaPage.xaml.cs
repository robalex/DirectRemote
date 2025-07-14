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
using Microsoft.Phone.Tasks;
using System.Net.NetworkInformation;
using DirecTVRemote.Model;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Shell;
using DirecTVRemote.ViewModel;

namespace DirecTVRemote.View {
	public partial class HubPanoramaPage : PhoneApplicationPage {
		public HubPanoramaPage() {
			InitializeComponent();

			SetMenuItemVisibility();

			this.LayoutUpdated += new EventHandler( HubPanoramaPage_LayoutUpdated );
			NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler( NetworkChange_NetworkAddressChanged );
		}

		protected override void OnNavigatedTo( System.Windows.Navigation.NavigationEventArgs e ) {
			base.OnNavigatedTo( e );

			SetMenuItemVisibility();
		}

		void HubPanoramaPage_LayoutUpdated( object sender, EventArgs e ) {
			this.LayoutUpdated -= new EventHandler( HubPanoramaPage_LayoutUpdated );

			SetMenuItemVisibility();

			if( !InternetConnectionAvailable() ) {
				DisableAll();
			}
		}

		private bool InternetConnectionAvailable() {
			if( !NetworkInterface.GetIsNetworkAvailable() ) {
				MessageBox.Show( "No wifi connection is available. Try again later." );
				return false;
			}

			return true;
		}

		void NetworkChange_NetworkAddressChanged( object sender, EventArgs e ) {
			if( !InternetConnectionAvailable() ) {
				MessageBox.Show( "Your internet connection has been lost. Try again later." );
				DisableAll();
			}
			else {
				EnableAll();
			}
		}

		private void DisableAll() {
			DisableTile( RemoteTile );
			DisableTile( CompactTile );
			DisableTile( SettingsTile );
			DisableTile( WhatsOnTile );

			//RemoteTile.IsEnabled		= false;
			//CompactTile.IsEnabled		= false;
			//SettingsTile.IsEnabled		= false;
			//HelpTile.IsEnabled			= false;
			//PrivacyTile.IsEnabled		= false;
			//FeedbackTile.IsEnabled		= false;
			//AdsTile.IsEnabled			= false;
			//WhatsOnTile.IsEnabled		= false;
		}

		private void EnableAll() {
			EnableTile( RemoteTile );
			EnableTile( CompactTile );
			EnableTile( SettingsTile );
			EnableTile( WhatsOnTile );
			//RemoteTile.IsEnabled		= true;
			//CompactTile.IsEnabled		= true;
			//SettingsTile.IsEnabled		= true;
			////HelpTile.IsEnabled			= true;
			//PrivacyTile.IsEnabled		= true;
			//FeedbackTile.IsEnabled		= true;
			//AdsTile.IsEnabled			= true;
			//WhatsOnTile.IsEnabled		= true;
		}

		private void EnableTile( HubTile tile ) {
			HubTileService.FreezeGroup( "HideableTile" );
			tile.Visibility = Visibility.Visible;
			tile.Opacity = 1;

			if( tile == RemoteTile ) {
				RemoteTile.Tap += Remote_Tap;
			}
			else if( tile == CompactTile ) {
				CompactTile.Tap += Compact_Tap;
			}
			else if( tile == SettingsTile ) {
				SettingsTile.Tap += Settings_Tap;
			}
			else if( tile == WhatsOnTile ) {
				WhatsOnTile.Tap += WhatsOn_Tap;
			}

			tile.Visibility = Visibility.Visible;
		}

		private void DisableTile( HubTile tile ) {
			HubTileService.FreezeGroup( "HideableTile" );
			tile.Visibility = Visibility.Collapsed;
			tile.Opacity = 0.5;

			if( tile == RemoteTile ) {
				RemoteTile.Tap -= Remote_Tap;
			}
			else if( tile == CompactTile ) {
				CompactTile.Tap -= Compact_Tap;
			}
			else if( tile == SettingsTile ) {
				SettingsTile.Tap -= Settings_Tap;
			}
			else if( tile == WhatsOnTile ) {
				WhatsOnTile.Tap -= WhatsOn_Tap;
			}

			//tile.Visibility = Visibility.Visible;
		}

		private void SetMenuItemVisibility() {
			bool urlExists = !string.IsNullOrEmpty( Settings.Instance.DirecTVUrl );

			if( !urlExists ) {
				DisableTile( RemoteTile );
				DisableTile( CompactTile );
				DisableTile( WhatsOnTile );
			}
			else {
				EnableTile( RemoteTile );
				EnableTile( CompactTile );
				EnableTile( WhatsOnTile );
			}

		}

		private void Settings_Tap( object sender, System.Windows.Input.GestureEventArgs e ) {
			NavigationService.Navigate( new Uri( "/View/SettingsView.xaml", UriKind.Relative ) );
		}

		private void WhatsOn_Tap( object sender, System.Windows.Input.GestureEventArgs e ) {
			if( WhatsOnTile.Opacity != 1 ) {
				return;
			}

			NavigationService.Navigate( new Uri( "/View/WhatsOnView.xaml", UriKind.Relative ) );
		}

		private void Compact_Tap( object sender, System.Windows.Input.GestureEventArgs e ) {
			if( CompactTile.Opacity != 1 ) {
				return;
			}

			NavigationService.Navigate( new Uri( "/View/CompactRemoteView.xaml", UriKind.Relative ) );
		}

		private void Remote_Tap( object sender, System.Windows.Input.GestureEventArgs e ) {
			if( RemoteTile.Opacity != 1 ) {
				return;
			}

			NavigationService.Navigate( new Uri( "/View/RemoteView.xaml", UriKind.Relative ) );
		}

		private void Help_Tap( object sender, System.Windows.Input.GestureEventArgs e ) {
			NavigationService.Navigate( new Uri( "/View/HelpView.xaml", UriKind.Relative ) );
		}

		private void Ads_Tap( object sender, System.Windows.Input.GestureEventArgs e ) {
			MarketplaceDetailTask task = new MarketplaceDetailTask();
			task.ContentIdentifier = "a808b0ff-4486-e011-986b-78e7d1fa76f8 ";
			task.ContentType = MarketplaceContentType.Applications;
			task.Show();
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

				emailComposeTask.To = "robalex@gmail.com";
				emailComposeTask.Body = string.Empty;
				emailComposeTask.Subject = "Direct Remote Feedback";
				emailComposeTask.Show();
			}
			else if( ((ListBoxItem)_mainMenuBox.SelectedItem).Content.ToString() == "I Hate Ads" ) {
				MarketplaceDetailTask task = new MarketplaceDetailTask();
				task.ContentIdentifier = "a808b0ff-4486-e011-986b-78e7d1fa76f8 ";
				task.ContentType = MarketplaceContentType.Applications;
				task.Show();
			}
		}

		private void Feedback_Tap( object sender, System.Windows.Input.GestureEventArgs e ) {
			EmailComposeTask emailComposeTask = new EmailComposeTask();

			emailComposeTask.To = "robalex@gmail.com";
			emailComposeTask.Body = string.Empty;
			emailComposeTask.Subject = "Direct Remote Feedback";
			emailComposeTask.Show();
		}

		private void Privacy_Tap( object sender, System.Windows.Input.GestureEventArgs e ) {
			NavigationService.Navigate( new Uri( "/View/PrivacyView.xaml", UriKind.Relative ) );
		}

        private void Remote_Hold(object sender, System.Windows.Input.GestureEventArgs e) {
            Popup p = new Popup();

            ListPicker picker = new ListPicker();
            ListPickerItem item = new ListPickerItem();
            item.Content = "Pin to Start";
            picker.Items.Add( item );
            item.Background = new SolidColorBrush( Colors.White );
            item.Foreground = new SolidColorBrush( Colors.Black );
            item.Tap += PinRemote_Tap;
            picker.Width = 400;
            picker.Height = 50;

            p.Child = picker;
            p.VerticalOffset = 250;
            p.HorizontalOffset = 10;
            p.IsOpen = true;
        }

        private void CompactRemote_Hold(object sender, System.Windows.Input.GestureEventArgs e) {
            Popup p = new Popup();

            ListPicker picker = new ListPicker();
            ListPickerItem item = new ListPickerItem();
            item.Content = "Pin to Start";
            picker.Items.Add(item);
            item.Background = new SolidColorBrush(Colors.White);
            item.Foreground = new SolidColorBrush(Colors.Black);
            item.Tap += PinCompactRemote_Tap;
            picker.Width = 400;
            picker.Height = 50;

            p.Child = picker;
            p.VerticalOffset = 250;
            p.HorizontalOffset = 10;
            p.IsOpen = true;
        }

        void PinRemote_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            string boxName = string.Empty;
            List<DirecTVBoxViewModel>					boxes			= Settings.Instance.DirecTVUrls;
            foreach( DirecTVBoxViewModel dtvbvm in boxes ) { 
                if( dtvbvm.Url == Settings.Instance.DirecTVUrl ) {
                    boxName = dtvbvm.Name;
                    break;
                }
            }

            string tileParameter = "Param=" + boxName;
            ShellTile tile = CheckIfTileExist(tileParameter);
            if (tile == null) {
                StandardTileData secondaryTile = new StandardTileData {
                    Title = boxName,
                    BackgroundImage = new Uri("remoteAppIcon.png", UriKind.Relative),
                    BackContent = boxName

                };
                ShellTile.Create(new Uri("/RemoteView.xaml?" + tileParameter, UriKind.Relative), secondaryTile);
            }
        }

        void PinCompactRemote_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            string boxName = string.Empty;
            List<DirecTVBoxViewModel> boxes = Settings.Instance.DirecTVUrls;
            foreach (DirecTVBoxViewModel dtvbvm in boxes)
            {
                if (dtvbvm.Url == Settings.Instance.DirecTVUrl)
                {
                    boxName = dtvbvm.Name;
                    break;
                }
            }

            string tileParameter = boxName;
            ShellTile tile = CheckIfTileExist(tileParameter);
            if (tile == null)
            {
                StandardTileData secondaryTile = new StandardTileData
                {
                    Title = boxName,
                    BackgroundImage = new Uri("CompactRemoteTile.png", UriKind.Relative),
                    BackContent = boxName

                };
                ShellTile.Create(new Uri("/CompactRemoteView.xaml?" + tileParameter, UriKind.Relative), secondaryTile);
            }
        }

        private ShellTile CheckIfTileExist(string tileUri) {
            ShellTile shellTile = ShellTile.ActiveTiles.FirstOrDefault(
                    tile => tile.NavigationUri.ToString().Contains(tileUri));
            return shellTile;
        }

	}
}