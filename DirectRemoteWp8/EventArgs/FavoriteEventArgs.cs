using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectRemoteWp8.RemoteEventArgs {

	public class FavoriteEventArgs : EventArgs {

		public string Major { get; set; }

		public FavoriteEventArgs( string major ) {
			Major = major;
		}

	}

}
