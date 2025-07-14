using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectRemotePCL;

namespace DirectRemoteWp8.RemoteEventArgs {

	public class ButtonPressEventArgs : EventArgs {

		public RemoteButton Button { get; set; }

		public ButtonPressEventArgs( RemoteButton button ) {
			Button = button;
		}

	}

}
