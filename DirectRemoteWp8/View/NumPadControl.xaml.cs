using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DirectRemotePCL;
using DirectRemoteWp8.RemoteEventArgs;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace DirectRemoteWp8.View {

	public partial class NumPadControl : UserControl {

		public EventHandler<ButtonPressEventArgs> ButtonPressed;

		public NumPadControl() {
			InitializeComponent();
		}

		private void Button_Click( object sender, RoutedEventArgs e ) {
			Button button = (Button)sender;
			RemoteButton buttonEnum;

			switch( button.Content.ToString() ) {
				case "0":
					buttonEnum = RemoteButton.Zero;
					break;
				case "1":
					buttonEnum = RemoteButton.One;
					break;
				case "2":
					buttonEnum = RemoteButton.Two;
					break;
				case "3":
					buttonEnum = RemoteButton.Three;
					break;
				case "4":
					buttonEnum = RemoteButton.Four;
					break;
				case "5":
					buttonEnum = RemoteButton.Five;
					break;
				case "6":
					buttonEnum = RemoteButton.Six;
					break;
				case "7":
					buttonEnum = RemoteButton.Seven;
					break;
				case "8":
					buttonEnum = RemoteButton.Eight;
					break;
				case "9":
					buttonEnum = RemoteButton.Nine;
					break;
				case "-":
					buttonEnum = RemoteButton.Dash;
					break;
				case "Enter":
					buttonEnum = RemoteButton.Enter;
					break;
				default:
					buttonEnum = RemoteButton.None;
					break;
			}

			if( ButtonPressed != null ) {
				ButtonPressed( this, new ButtonPressEventArgs( buttonEnum ) );
			}
		}

	}


}
