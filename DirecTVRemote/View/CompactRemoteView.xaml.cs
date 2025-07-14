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
using DirecTVRemote.Controls;
using DirecTVRemote.Model;
using Microsoft.Phone.Controls;
using System.Windows.Controls.Primitives;
using DirecTVRemote.ViewModel;
using System.Device.Location;

namespace DirecTVRemote.View {
	public partial class CompactRemoteView : PhoneApplicationPage {

		private Tuner				_tuner;

		protected Popup				_popup					= new Popup();

		protected Popup				_favoritesPopup			= new Popup();

		NumpadPopupControl			_npc					= new NumpadPopupControl();

		FavoritesPopupControl		_fpc					= new FavoritesPopupControl();

		// Constructor
		public CompactRemoteView() {
			InitializeComponent();

			_tuner					= new Tuner();
			_npc.NumpadDismissed	+= new EventHandler<EventArgs>( npc_NumpadDismissed );
			_npc.ButtonPressed		+= new EventHandler<ButtonPressEventArgs>( npc_ButtonPressed );
			_fpc.FavoritesDismissed += new EventHandler<EventArgs>( fpc_FavoritesDismissed );
			_fpc.FavoriteSelected	+= new EventHandler<FavoriteEventArgs>( fpc_FavoriteSelected );

			ApplicationBar.StateChanged += new EventHandler<Microsoft.Phone.Shell.ApplicationBarStateChangedEventArgs>( ApplicationBar_StateChanged );

			_popup.VerticalOffset	= 355;
			_popup.Child			= _npc;
			_popup.Width			= 480;
			_npc.Width				= 480;

			_favoritesPopup.VerticalOffset	= 10;
			_favoritesPopup.Child			= _fpc;
			_favoritesPopup.Width			= 480;
			_fpc.Width						= 480;
			_favoritesPopup.Height			= 720;
			_fpc.Height						= 720;

			this.DataContext		= new RemoteViewModel();
		}

		void ApplicationBar_StateChanged( object sender, Microsoft.Phone.Shell.ApplicationBarStateChangedEventArgs e ) {
			if( e.IsMenuVisible ) {
				_popup.IsOpen = false;
				_favoritesPopup.IsOpen = false;
				EnableControls();
			}
		}

		protected override void OnBackKeyPress( System.ComponentModel.CancelEventArgs e ) {
			if( _popup.IsOpen ) {
				_popup.IsOpen	= false;
				e.Cancel		= true;
			}
			else if( _favoritesPopup.IsOpen ) {
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

		private void Advance_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Advance );
		}

		private void Replay_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Replay );
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

		private void Repaly_Click( object sender, EventArgs e ) {
			_tuner.SimulateRemoteButton( RemoteButton.Replay );
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

		private void Numpad_Click( object sender, EventArgs e ) {
			DisableControls();
			_popup.IsOpen = true;
		}

		void npc_ButtonPressed( object sender, ButtonPressEventArgs e ) {
			_tuner.SimulateRemoteButton( e.Button );
		}

		void npc_NumpadDismissed( object sender, EventArgs e ) {
			_popup.IsOpen = false;
			EnableControls();
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

		private void DisableControls() {
			_infoButton.IsEnabled		= false;
			_guideButton.IsEnabled		= false;
			_listButton.IsEnabled		= false;
			_exitButton.IsEnabled		= false;
			_activeButton.IsEnabled		= false;
			_menuButton.IsEnabled		= false;
			_recordButton.IsEnabled		= false;
			_previousButton.IsEnabled	= false;
		}

		private void EnableControls() {
			_infoButton.IsEnabled		= true;
			_guideButton.IsEnabled		= true;
			_listButton.IsEnabled		= true;
			_exitButton.IsEnabled		= true;
			_activeButton.IsEnabled		= true;
			_menuButton.IsEnabled		= true;
			_recordButton.IsEnabled		= true;
			_previousButton.IsEnabled	= true;
		}
	}
}