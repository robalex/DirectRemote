using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectRemoteWp8.Model {

	public class AddDirecTVBoxResult {

		public bool Result { get; set; }

		public string Message { get; set; }

		public AddDirecTVBoxResult( bool result, string message ) {
			Result = result;
			Message = message;
		}

	}

}
