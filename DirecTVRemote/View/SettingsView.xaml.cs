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
using System.Windows.Controls.Primitives;
using DirecTVRemote.Controls;
using System.Collections.ObjectModel;
using System.Device.Location;

namespace DirecTVRemote.View {
	public partial class SettingsView : PhoneApplicationPage {

		protected	Popup				_popup;
		private		AddBoxControl		_abc;
		private		BoxPickerViewModel	_bpvm;

		public SettingsView() {
			InitializeComponent();
			string currentUrl = Settings.Instance.DirecTVUrl;

			_bpvm = new BoxPickerViewModel();
			ContentPanel.DataContext = _bpvm;
			SetPickerViewstate();

			_popup	= new Popup();
			_abc	= new AddBoxControl();

			_popup.VerticalOffset	= 0;
			_popup.Child			= _abc;
			_popup.Width			= 480;
			_abc.Width				= 480;
			_abc.Height				= 800;
			_popup.Height			= 800;

			_abc.BoxAdded += Box_Added;

			_boxPicker.SelectionChanged += _boxPicker_SelectionChanged;
		}

		protected override void OnNavigatedFrom( System.Windows.Navigation.NavigationEventArgs e ) {
			base.OnNavigatedFrom( e );

			this.State[ "PreservingPageState" ]	= true;
			this.State[ "Name" ]				= _abc._nameBox.Text;
			this.State[ "Url" ]					= _abc._urlBox.Text;
			this.State[ "PopupVisible" ]		= _popup.IsOpen;
		}

		protected override void OnNavigatedTo( System.Windows.Navigation.NavigationEventArgs e ) {
			base.OnNavigatedTo( e );

			if( this.State.ContainsKey( "PreservingPageState" ) && ((bool)this.State[ "PreservingPageState" ]) ) {
				_abc._nameBox.Text	= this.State["Name"].ToString();
				_abc._urlBox.Text	= this.State["Url"].ToString();
				_popup.IsOpen		= (bool)this.State[ "PopupVisible" ];
			}

			this.State["PreservingPageState"] = false;
		}

		protected void Box_Added( object sender, BoxAddEventArgs e ) {
			if( e.BoxAdded ) {
				Settings.Instance.DirecTVUrl = _abc._urlBox.Text;

				_popup.IsOpen		= false;
				_abc._nameBox.Text	= string.Empty;
				_abc._urlBox.Text	= string.Empty;

				Dispatcher.BeginInvoke( () => SetPickerViewstate() );
			}
		}

		protected override void OnBackKeyPress( System.ComponentModel.CancelEventArgs e ) {
			if( _popup.IsOpen ) {
				_popup.IsOpen = false;
				e.Cancel = true;
			}
			else {
				base.OnBackKeyPress( e );
			}
		}

		private void Add_Box_Click( object sender, RoutedEventArgs e ) {
			_popup.IsOpen = true;
		}

		private void Delete_Box_Click( object sender, RoutedEventArgs e ) {
			DirecTVBoxViewModel dtvvm = _boxPicker.SelectedItem as DirecTVBoxViewModel;

			MessageBoxResult result = MessageBox.Show( "Are you sure you want to delete " + dtvvm.Name + "?", "Delete DirecTV Box",
			                                           MessageBoxButton.OKCancel );

			if( result == MessageBoxResult.OK ) {
				if( dtvvm != null ) {
					Settings.Instance.RemoveDirecTVBox( dtvvm.Name, dtvvm.Url );
					Dispatcher.BeginInvoke( () => SetPickerViewstate() );
				}
			}
		}

		private void SetPickerViewstate() {
			List<DirecTVBoxViewModel>					boxes			= Settings.Instance.DirecTVUrls;
			ObservableCollection<DirecTVBoxViewModel>	observableBoxes	= new ObservableCollection<DirecTVBoxViewModel>();
			string										url				= Settings.Instance.DirecTVUrl;

			_bpvm.Boxes.Clear();
			foreach( DirecTVBoxViewModel box in boxes ) {
				_bpvm.Boxes.Add( new DirecTVBoxViewModel( box.Name, box.Url ) );
			}

			if( boxes == null || boxes.Count == 0 ) {
				_deleteButton.IsEnabled = false;
				_boxPicker.IsEnabled	= false;
			}
			else {
				_deleteButton.IsEnabled = true;
				_boxPicker.IsEnabled	= true;

				 foreach( object item in _boxPicker.Items ) {
			        DirecTVBoxViewModel dtvvm = item as DirecTVBoxViewModel;
					if( dtvvm.Url == url ) {
			            _boxPicker.SelectedItem = item;
			        }
			    }
			}
		}

		private void _boxPicker_SelectionChanged( object sender, SelectionChangedEventArgs e ) {
			DirecTVBoxViewModel dtvvm = _boxPicker.SelectedItem as DirecTVBoxViewModel;
			if( dtvvm != null ) {
				Settings.Instance.DirecTVUrl = dtvvm.Url;
			}
		}

		private void Scan_Click( object sender, RoutedEventArgs e ) {
			TCPScanner scanner = new TCPScanner();
			scanner.ScanFinished += new EventHandler<PortScanEventArgs>( scanner_ScanFinished );
			scanner.ScanPorts();
		}

		void scanner_ScanFinished( object sender, PortScanEventArgs e ) {
			foreach( string url in e.Urls ) {
				Settings.Instance.AddDirecTVBox( Guid.NewGuid().ToString(), url );
			}

			SetPickerViewstate();
		}

	}
}