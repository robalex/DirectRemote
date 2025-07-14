using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectRemoteWp8.ViewModel {

	public class NotifyingObject : INotifyPropertyChanged {

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler  PropertyChanged;

		#endregion

		#region Private Methods

		protected void OnPropertyChanged( string property ) {
			if( PropertyChanged != null ) {
				PropertyChanged( this, new PropertyChangedEventArgs( property ) );
			}
		}

		#endregion

	}

}
