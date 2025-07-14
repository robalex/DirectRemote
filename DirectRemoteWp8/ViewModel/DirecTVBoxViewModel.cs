using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectRemoteWp8.ViewModel {

	public class DirecTVBoxViewModel : NotifyingObject {

		private string _name;

		private string _url;

		public string Name {
			get {
				return _name;
			}

			set {
				_name = value;
				OnPropertyChanged( "Name" );
			}
		}

		public string Url {
			get {
				return _url;
			}

			set {
				_url = value;
				OnPropertyChanged( "Url" );
			}
		}

		public DirecTVBoxViewModel() {

		}

		public DirecTVBoxViewModel( string name, string url ) {
			Name = name;
			Url = url;
		}

	}

}
