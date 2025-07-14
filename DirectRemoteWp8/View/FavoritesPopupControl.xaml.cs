using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DirectRemotePCL;
using DirectRemoteWp8.Model;
using DirectRemoteWp8.RemoteEventArgs;
using DirectRemoteWp8.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace DirectRemoteWp8.View {
	public partial class FavoritesPopupControl : UserControl {

		CommunicationManager					_tuner		= new CommunicationManager( Settings.Instance.DirecTVUrl );
		ObservableCollection<WhatsOnViewModel>	_whatsOn	= new ObservableCollection<WhatsOnViewModel>();

		public event EventHandler<EventArgs>			FavoritesDismissed;

		public event EventHandler<FavoriteEventArgs>	FavoriteSelected;

		private bool _exiting;

		public FavoritesPopupControl() {
			InitializeComponent();
		}

		public void StartShowing() {
			_exiting = false;

			LoadFavorites();
		}

		private void LoadFavorites() {
			_whatsOn.Clear();

			foreach( ChannelStatus status in Settings.Instance.Favorites ) {
				WhatsOnViewModel wovm = new WhatsOnViewModel( status );

				_whatsOn.Add( wovm );
			}

			Dispatcher.BeginInvoke( () => GetChannelInfo() );

			this.DataContext = _whatsOn;
		}

		private void Dismiss_Click( object sender, RoutedEventArgs e ) {
			if( FavoritesDismissed != null ) {
				FavoritesDismissed( this, null );
			}
		}

		private void GetChannelInfo() {
			foreach( WhatsOnViewModel wovm in _whatsOn ) {
				_tuner.GetProgInfo( wovm.ChannelNumber );
				Thread.Sleep( 200 );
			}
		}

		private void CompleteGetProgInfo( ChannelStatus status ) {
			if( status.Status.Code != "200" ) {
				if( !_exiting ) {
					MessageBox.Show( "Unable to connect to the specified DirecTV Box. Please check your settings." );
				}

				if( FavoritesDismissed != null ) {
					FavoritesDismissed( this, null );
				}
			}

			foreach( WhatsOnViewModel wovm in _whatsOn ) {
				if( wovm.ChannelNumber == status.Major ) {
					wovm.Title = status.Title;
				}
			}
		}

		private async void _addButton_Click( object sender, RoutedEventArgs e ) {
			ChannelStatus status = await _tuner.GetTuned();

			GetTuned_Completed( status );
		}

		void GetTuned_Completed( ChannelStatus status ) {

			if( status.Status.Code != "200" ) {
				if( !_exiting ) {
					MessageBox.Show( "Unable to connect to the specified DirecTV Box. Please check your settings." );
				}

				if( FavoritesDismissed != null ) {
					FavoritesDismissed( this, null );
				}
			}
			else {
				Settings.Instance.AddFavorite( status );

				bool exists = false;
				foreach( WhatsOnViewModel wovm in _whatsOn ) {
					if( wovm.ChannelNumber == status.Major ) {
						exists = true;
						break;
					}
				}

				if( !exists ) {
					WhatsOnViewModel wovm = new WhatsOnViewModel( status );
					_whatsOn.Add( wovm );
					_tuner.GetProgInfo( wovm.ChannelNumber );
				}
			}
		}

		private void FavoriteButton_Click( object sender, RoutedEventArgs e ) {
			Button				button	= sender as Button;
			WhatsOnViewModel	wovm	= button.DataContext as WhatsOnViewModel;

			_tuner.SetChannel( wovm.ChannelNumber );
		}

		private void DeleteButton_Click( object sender, RoutedEventArgs e ) {
			Button				button	= sender as Button;
			WhatsOnViewModel	wovm	= button.DataContext as WhatsOnViewModel;

			MessageBoxResult result = MessageBox.Show( "Are you sure you want to delete " + wovm.Callsign + " from your favorites?", "Delete Favorite",
							 MessageBoxButton.OKCancel );

			if( result == MessageBoxResult.OK ) {
				Settings.Instance.RemoveFavorite( wovm.ChannelNumber );
			}

		}

	}
}
