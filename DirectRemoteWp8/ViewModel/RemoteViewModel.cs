using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectRemoteWp8.ViewModel {

	public class RemoteViewModel {

		private const string _LIGHT_UP_BUTTON = "/Images/appbar.upload.rest.light.png";

		private const string _DARK_UP_BUTTON = "/Images/appbar.upload.rest.png";

		private const string _LIGHT_DOWN_BUTTON = "/Images/appbar.download.rest.light.png";

		private const string _DARK_DOWN_BUTTON = "/Images/appbar.download.rest.png";

		private const string _LIGHT_BACK_BUTTON = "/Images/appbar.back.rest.light.png";

		private const string _DARK_BACK_BUTTON = "/Images/appbar.back.rest.png";

		private const string _LIGHT_NEXT_BUTTON = "/Images/appbar.next.rest.light.png";

		private const string _DARK_NEXT_BUTTON = "/Images/appbar.next.rest.png";

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
