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
using DirecTVRemote.Model;
using Microsoft.Phone.Controls;
using DirecTVRemote.ViewModel;
using DirecTVRemote.Controls;
using System.Windows.Controls.Primitives;
using System.Device.Location;

namespace DirecTVRemote {
	public partial class RemoteView : PhoneApplicationPage {
		private Tuner	_tuner;

		protected Popup				_favoritesPopup			= new Popup();

		FavoritesPopupControl		_fpc					= new FavoritesPopupControl();

		// Constructor
		public RemoteView() {
			InitializeComponent();

			ApplicationBar.StateChanged += new EventHandler<Microsoft.Phone.Shell.ApplicationBarStateChangedEventArgs>( ApplicationBar_StateChanged );
		
			_fpc.FavoritesDismissed	+= new EventHandler<EventArgs>( fpc_FavoritesDismissed );
			_fpc.FavoriteSelected	+= new EventHandler<FavoriteEventArgs>( fpc_FavoriteSelected );

			_favoritesPopup.VerticalOffset = 10;
			_favoritesPopup.Child = _fpc;
			_favoritesPopup.Width = 480;
			_fpc.Width = 480;
			_favoritesPopup.Height = 720;
			_fpc.Height = 720;

			_tuner = new Tuner();
			_numPadControl.ButtonPressed += new EventHandler<ButtonPressEventArgs>( Button_Pressed );
			this.DataContext = new RemoteViewModel();
		}

		void ApplicationBar_StateChanged( object sender, Microsoft.Phone.Shell.ApplicationBarStateChangedEventArgs e ) {
			if( e.IsMenuVisible ) {
				_favoritesPopup.IsOpen = false;
			}
		}

		protected override void OnBackKeyPress( System.ComponentModel.CancelEventArgs e ) {
			if( _favoritesPopup.IsOpen ) {
				_favoritesPopup.IsOpen = false;
				e.Cancel = true;
			}
			else {
				base.OnBackKeyPress( e );
			}
		}

		private void Button_Pressed( object sender, ButtonPressEventArgs e ) {
			_tuner.SimulateRemoteButton( e.Button );
		}

		private void ChannelUp_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.ChannelUp );
		}

		private void ChannelDown_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.ChannelDown );
		}

		private void Down_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Down );
		}

		private void Up_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Up );
		}

		private void Left_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Left );
		}

		private void Right_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Right );
		}

		private void Select_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Select );
		}

		private void Blue_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Blue );
		}

		private void Advance_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Advance );
		}

		private void Replay_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Replay );
		}

		private void Green_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Green );
		}

		private void Yellow_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Yellow );
		}

		private void Red_Click( object sender, RoutedEventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Red );
		}

		private void Power_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Power );
		}

		private void PowerOn_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.PowerOn );
		}

		private void PowerOff_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.PowerOff );
		}


		private void Format_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Format );
		}

		private void Rewind_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Rewind );
		}

		
		private void Pause_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Pause );
		}

		
		private void Play_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Play );
		}

		
		private void Guide_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Guide );
		}

		private void Menu_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Menu );
		}

		private void Active_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Active );
		}

		private void Exit_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Exit );
		}

		private void List_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.List );
		}

		private void Info_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Info );
		}

		private void Record_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Record );
		}

		private void Previous_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Previous );
		}

		private void FastForward_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.FastForward );
		}

		void fpc_FavoriteSelected( object sender, FavoriteEventArgs e ) {
			_tuner.SetChannel( e.Major );
		}

		void fpc_FavoritesDismissed( object sender, EventArgs e ) {
			_favoritesPopup.IsOpen = false;
		}

		void Favorites_Click( object sender, RoutedEventArgs e ) {
			_fpc.StartShowing();
			_favoritesPopup.IsOpen = true;
		}

	}
}