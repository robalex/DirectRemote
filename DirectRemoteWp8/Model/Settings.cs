using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DirectRemotePCL;
using DirectRemoteWp8.ViewModel;

namespace DirectRemoteWp8.Model {

	public class Settings {

		private static Settings						_instance;

		private static volatile object				_instanceLock		= new object();

		private string								_direcTVUrl			= string.Empty;

		private List<DirecTVBoxViewModel>			_direcTVBoxes		= new List<DirecTVBoxViewModel>();

		private List<ChannelStatus>					_favorites			= new List<ChannelStatus>();

		private Dictionary<string, ChannelStatus>	_majorChanToStatus	= new Dictionary<string, ChannelStatus>();

		private bool								_updatingChannels;

		private bool								_locationEnabled = true;

		private object								_locationLock = new object();


		public bool UpdatingChannels {
			get {
				lock( _majorChanToStatus ) {
					return _updatingChannels;
				}
			}

			set {
				lock( _majorChanToStatus ) {
					_updatingChannels = value;
				}
			}
		}

		public int ChannelCount {
			get {
				lock( _majorChanToStatus ) {
					return _majorChanToStatus.Count;
				}
			}
		}

		public List<ChannelStatus> GetChannels() {
			lock( _majorChanToStatus ) {
				return new List<ChannelStatus>( _majorChanToStatus.Values );
			}
		}

		public void SetChannels( ChannelStatus[] channels ) {
			if( channels != null ) {
				lock( _majorChanToStatus ) {
					foreach( ChannelStatus channel in channels ) {
						_majorChanToStatus[channel.Major + "-" + channel.Minor] = channel;
					}
				}
			}
		}

		public string DirecTVUrl {
			get {
				lock( _direcTVUrl ) {
					return _direcTVUrl;
				}
			}

			set {
				if( _direcTVUrl != value ) {
					_direcTVUrl = value;
					SaveConfiguration();
				}
			}
		}

		public List<ChannelStatus> Favorites {
			get {
				lock( _favorites ) {
					return _favorites;
				}
			}

			set {
				lock( _favorites ) {
					_favorites = value;
				}
			}
		}

		public List<DirecTVBoxViewModel> DirecTVUrls {
			get {
				lock( _direcTVUrl ) {
					return _direcTVBoxes;
				}
			}

			set {
				if( _direcTVBoxes != null ) {
					_direcTVBoxes = value;
					SaveConfiguration();
				}
			}
		}

		public bool LocationEnabled {
			get {
				lock( _locationLock ) {
					return _locationEnabled;
				}
			}

			set {
				lock( _locationLock ) {
					_locationEnabled = value;
					SaveConfiguration();
				}
			}
		}

		public static Settings Instance {
			get {
				if( _instance == null ) {
					lock( _instanceLock ) {
						if( _instance == null ) {
							_instance = new Settings();
						}
					}
				}

				return _instance;
			}
		}

		private Settings() {
			LoadConfiguration();
		}

		private string GetElementInnerXml( XElement element, string name ) {
			XElement temp = element.Element( name );
			XmlReader reader = temp.CreateReader();
			reader.MoveToContent();

			return reader.ReadInnerXml();
		}


		/// <summary>
		/// Checks if the persisted <see cref="Settings">settings</see> file exists on the client
		/// </summary>
		/// <returns></returns>
		private void LoadConfiguration() {
			LoadDirecTVBoxes();
			LoadSelectedUrl();
			LoadFavorites();
			//LoadChannels();
		}

		private bool LoadSelectedUrl() {
			lock( _direcTVUrl ) {
				using( IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication() ) {
					if( file.FileExists( Path.Combine( "Settings", "settings.xml" ) ) ) {

						using(
							IsolatedStorageFileStream stream = file.OpenFile( Path.Combine( "Settings", "settings.xml" ), FileMode.Open ) ) {
							XDocument doc = XDocument.Load( stream );

							foreach( XElement element in doc.Elements( "Settings" ) ) {
								_direcTVUrl = GetElementInnerXml( element, "DirecTVUrl" );
								try {
									_locationEnabled = bool.Parse( GetElementInnerXml( element, "LocationEnabled" ) );
								}
								catch {
									_locationEnabled = true;
									// no locationenabled setting. use the default
								}
							}

							return true;
						}
					}
				}
			}

			return false;
		}

		//private bool LoadChannels() {
		//    lock( _majorChanToStatus ) {
		//        _majorChanToStatus = new Dictionary<string, ChannelStatus>();

		//        using( IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication() ) {

		//            if( file.FileExists( Path.Combine( "Channels", "channels.xml" ) ) ) {
		//                using(
		//                    IsolatedStorageFileStream stream = file.OpenFile( Path.Combine( "Channels", "channels.xml" ), FileMode.Open ) ) {
		//                    XDocument doc = XDocument.Load( stream );

		//                    foreach( XElement element in doc.Elements( "Channels" ).Elements( "Channel" ) ) {
		//                        ChannelStatus status = new ChannelStatus();
		//                        status.Callsign = GetElementInnerXml( element, "Callsign" );
		//                        status.IsPclocked = GetElementInnerXml( element, "IsPclocked" );
		//                        status.IsPpv = GetElementInnerXml( element, "IsPpv" );
		//                        status.IsVod = GetElementInnerXml( element, "IsVod" );
		//                        status.Major = GetElementInnerXml( element, "Major" );
		//                        status.Minor = GetElementInnerXml( element, "Minor" );
		//                        status.StationId = GetElementInnerXml( element, "StationId" );

		//                        _majorChanToStatus[status.Major + "-" + status.Minor] = status;
		//                    }

		//                    return true;
		//                }
		//            }
		//        }
		//    }

		//    return false;
		//}

		private bool LoadDirecTVBoxes() {
			lock( _direcTVBoxes ) {
				_direcTVBoxes = new List<DirecTVBoxViewModel>();

				using( IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication() ) {

					if( file.FileExists( Path.Combine( "DirecTVBoxes", "boxes.xml" ) ) ) {

						using(
							IsolatedStorageFileStream stream = file.OpenFile( Path.Combine( "DirecTVBoxes", "boxes.xml" ), FileMode.Open ) ) {
							try {
								XDocument doc = XDocument.Load( stream );

								foreach( XElement element in doc.Element( "Boxes" ).Elements( "Box" ) ) {
									DirecTVBoxViewModel box = new DirecTVBoxViewModel();
									box.Url = GetElementInnerXml( element, "Url" );
									box.Name = GetElementInnerXml( element, "Name" );

									_direcTVBoxes.Add( box );
								}

								return true;
							}
							catch( Exception e ) {
								return false;
							}
						}
					}
				}
			}

			return false;
		}

		public AddDirecTVBoxResult AddDirecTVBox( string name, string url ) {
			lock( _direcTVBoxes ) {
				foreach( DirecTVBoxViewModel savedBox in _direcTVBoxes ) {
					if( savedBox.Name == name ) {
						return new AddDirecTVBoxResult( false, "A DirecTV box of the same name has already been added." );
					}

					if( savedBox.Url == url ) {
						return new AddDirecTVBoxResult( false, "A DirecTV box with the same Url has already been added." );
					}
				}

				_direcTVBoxes.Add( new DirecTVBoxViewModel( name, url ) );
				SaveDirecTVBoxes();
			}

			return new AddDirecTVBoxResult( true, string.Empty );
		}

		public void RemoveFavorite( string major ) {
			lock( _favorites ) {
				for( int i = 0; i < _favorites.Count; i++ ) {
					if( _favorites[i].Major == major ) {
						_favorites.RemoveAt( i );
						break;
					}
				}

				SaveFavorites();
			}
		}

		public bool AddFavorite( ChannelStatus channelStatus ) {
			lock( _favorites ) {
				foreach( ChannelStatus status in _favorites ) {
					if( status.Major == channelStatus.Major && status.Minor == channelStatus.Minor ) {
						return false;
					}
				}

				_favorites.Add( channelStatus );
				SaveFavorites();
			}

			return true;
		}

		public void RemoveDirecTVBox( string name, string url ) {
			lock( _direcTVBoxes ) {
				for( int i = _direcTVBoxes.Count - 1; i >= 0; i-- ) {
					if( _direcTVBoxes[i].Name == name && _direcTVBoxes[i].Url == url ) {
						_direcTVBoxes.RemoveAt( i );
					}
				}

				SaveDirecTVBoxes();
			}
		}

		private bool LoadFavorites() {
			lock( _favorites ) {
				_favorites = new List<ChannelStatus>();

				using( IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication() ) {

					if( file.FileExists( Path.Combine( "Favorites", "favorites.xml" ) ) ) {

						using(
							IsolatedStorageFileStream stream = file.OpenFile( Path.Combine( "Favorites", "favorites.xml" ), FileMode.Open ) ) {
							try {
								XDocument doc = XDocument.Load( stream );

								foreach( XElement element in doc.Element( "Favorites" ).Elements( "Favorite" ) ) {
									ChannelStatus status = new ChannelStatus();
									status.Major = GetElementInnerXml( element, "Major" );
									status.Minor = GetElementInnerXml( element, "Minor" );
									status.Callsign = GetElementInnerXml( element, "Callsign" );

									_favorites.Add( status );
								}

								return true;
							}
							catch( Exception e ) {
								return false;
							}
						}
					}
				}
			}

			return false;
		}

		private void SaveFavorites() {
			lock( _favorites ) {
				XDocument doc;
				XElement favorites = new XElement( "Favorites" );
				foreach( ChannelStatus status in _favorites ) {
					favorites.Add( new XElement( "Favorite",
						new XElement( "Major", status.Major ),
						new XElement( "Minor", status.Minor ),
						new XElement( "Callsign", status.Callsign ) ) );
				}

				doc = new XDocument( favorites );

				using( IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication() ) {
					if( !file.DirectoryExists( "Favorites" ) ) {
						file.CreateDirectory( "Favorites" );
					}

					if( file.FileExists( Path.Combine( "Favorites", "favorites.xml" ) ) ) {
						file.DeleteFile( Path.Combine( "Favorites", "favorites.xml" ) );
					}

					using( IsolatedStorageFileStream stream = file.OpenFile( Path.Combine( "Favorites", "favorites.xml" ), FileMode.Create ) ) {
						doc.Save( stream );
					}
				}
			}
		}

		private void SaveDirecTVBoxes() {
			lock( _direcTVBoxes ) {
				XDocument doc;
				XElement direcTVBoxes = new XElement( "Boxes" );
				foreach( DirecTVBoxViewModel box in _direcTVBoxes ) {
					direcTVBoxes.Add( new XElement( "Box",
						new XElement( "Url", box.Url ),
						new XElement( "Name", box.Name ) ) );
				}

				doc = new XDocument( direcTVBoxes );

				using( IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication() ) {
					if( !file.DirectoryExists( "DirecTVBoxes" ) ) {
						file.CreateDirectory( "DirecTVBoxes" );
					}

					if( file.FileExists( Path.Combine( "DirecTVBoxes", "boxes.xml" ) ) ) {
						file.DeleteFile( Path.Combine( "DirecTVBoxes", "boxes.xml" ) );
					}

					using( IsolatedStorageFileStream stream = file.OpenFile( Path.Combine( "DirecTVBoxes", "boxes.xml" ), FileMode.Create ) ) {
						doc.Save( stream );
					}
				}
			}
		}

		//private void SaveChannels() {
		//    lock( _majorChanToStatus ) {
		//        XDocument doc;
		//        XElement channels = new XElement( "Channels" );
		//        foreach( ChannelStatus channel in _majorChanToStatus.Values ) {
		//            channels.Add( new XElement( "Channel",
		//                new XElement( "Callsign", channel.Callsign ),
		//                new XElement( "IsPclocked", channel.IsPclocked ),
		//                new XElement( "IsPpv", channel.IsPpv ),
		//                new XElement( "IsVod", channel.IsVod ),
		//                new XElement( "Major", channel.Major ),
		//                new XElement( "Minor", channel.Minor ),
		//                new XElement( "StationId", channel.StationId )
		//                ) );
		//        }

		//        doc = new XDocument( channels );

		//        using( IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication() ) {
		//            if( !file.DirectoryExists( "Channels" ) ) {
		//                file.CreateDirectory( "Channels" );
		//            }

		//            if( file.FileExists( Path.Combine( "Channels", "channels.xml" ) ) ) {
		//                file.DeleteFile( Path.Combine( "Channels", "channels.xml" ) );
		//            }

		//            using( IsolatedStorageFileStream stream = file.OpenFile( Path.Combine( "Channels", "channels.xml" ), FileMode.Create ) ) {
		//                doc.Save( stream );
		//            }

		//        }
		//    }

		//}

		/// <summary>
		/// Saves the <see cref="Settings">settings</see> file to the client
		/// </summary>
		private void SaveConfiguration() {
			lock( this ) {
				XDocument doc = new XDocument(
					new XElement( "Settings",
								  new XElement( "DirecTVUrl", DirecTVUrl ),
								  new XElement( "LocationEnabled", LocationEnabled )
								   ) );

				using( IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication() ) {
					if( !file.DirectoryExists( "Settings" ) ) {
						file.CreateDirectory( "Settings" );
					}

					if( file.FileExists( Path.Combine( "Settings", "settings.xml" ) ) ) {
						file.DeleteFile( Path.Combine( "Settings", "settings.xml" ) );
					}

					using( IsolatedStorageFileStream stream = file.OpenFile( Path.Combine( "Settings", "settings.xml" ), FileMode.Create ) ) {
						doc.Save( stream );
					}

				}
			}

		}


	}

}
