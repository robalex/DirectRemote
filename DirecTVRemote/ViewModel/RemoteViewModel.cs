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

namespace DirecTVRemote.ViewModel {
	public class RemoteViewModel {

		private const string _LIGHT_UP_BUTTON = "/Images/appbar.upload.rest.light.png";

		private const string _DARK_UP_BUTTON = "/Images/appbar.upload.rest.png";

		private const string _LIGHT_DOWN_BUTTON = "/Images/appbar.download.rest.light.png";

		private const string _DARK_DOWN_BUTTON = "/Images/appbar.download.rest.png";

		private const string _LIGHT_BACK_BUTTON = "/Images/appbar.back.rest.light.png";

		private const string _DARK_BACK_BUTTON = "/Images/appbar.back.rest.png";

		private const string _LIGHT_NEXT_BUTTON = "/Images/appbar.next.rest.light.png";

		private const string _DARK_NEXT_BUTTON = "/Images/appbar.next.rest.png";

		private const string _LIGHT_ADVANCE_BUTTON = "/Images/appbar.advance.rest.light.png";

		private const string _DARK_ADVANCE_BUTTON = "/Images/appbar.advance.rest.png";

		private const string _LIGHT_REPLAY_BUTTON = "/Images/appbar.replay.rest.light.png";

		private const string _DARK_REPLAY_BUTTON = "/Images/appbar.replay.rest.png";

		public string ReplayButtonPath {
			get {
				bool isLightTheme = (Visibility)Application.Current.Resources["PhoneLightThemeVisibility"] == Visibility.Visible;

				if( isLightTheme ) {
					return _LIGHT_REPLAY_BUTTON;
				}

				return _DARK_REPLAY_BUTTON;
			}
		}

		public string AdvanceButtonPath {
			get {
				bool isLightTheme = (Visibility)Application.Current.Resources["PhoneLightThemeVisibility"] == Visibility.Visible;

				if( isLightTheme ) {
					return _LIGHT_ADVANCE_BUTTON;
				}

				return _DARK_ADVANCE_BUTTON;
			}
		}

		public string UpButtonPath {
			get {
				bool isLightTheme = (Visibility)Application.Current.Resources["PhoneLightThemeVisibility"] == Visibility.Visible;
				
				if( isLightTheme ) {
					return _LIGHT_UP_BUTTON;
				}

				return _DARK_UP_BUTTON;
			}
		}

		public string DownButtonPath {
			get {
				bool isLightTheme = (Visibility)Application.Current.Resources["PhoneLightThemeVisibility"] == Visibility.Visible;

				if( isLightTheme ) {
					return _LIGHT_DOWN_BUTTON;
				}

				return _DARK_DOWN_BUTTON;
			}
		}

		public string LeftButtonPath {
			get {
				bool isLightTheme = (Visibility)Application.Current.Resources["PhoneLightThemeVisibility"] == Visibility.Visible;

				if( isLightTheme ) {
					return _LIGHT_BACK_BUTTON;
				}

				return _DARK_BACK_BUTTON;
			}
		}

		public string RightButtonPath {
			get {
				bool isLightTheme = (Visibility)Application.Current.Resources["PhoneLightThemeVisibility"] == Visibility.Visible;

				if( isLightTheme ) {
					return _LIGHT_NEXT_BUTTON;
				}

				return _DARK_NEXT_BUTTON;
			}
		}

	}
}
