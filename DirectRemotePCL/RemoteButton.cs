using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DirectRemotePCL {
	public class StringValueAttribute : Attribute {

		public string StringValue { get; protected set; }

		public StringValueAttribute( string value ) {
			StringValue = value;
		}

		public static string GetStringValue( Enum value ) {
			Type type = value.GetType();

			FieldInfo fieldInfo = type.GetField( value.ToString() );

			StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
				typeof( StringValueAttribute ), false ) as StringValueAttribute[];

			return attribs.Length > 0 ? attribs[0].StringValue : null;
		}
	}

	public enum RemoteButton {
		[StringValueAttribute( "power" )]
		Power,

		[StringValueAttribute( "poweron" )]
		PowerOn,

		[StringValueAttribute( "poweroff" )]
		PowerOff,

		[StringValueAttribute( "format" )]
		Format,

		[StringValueAttribute( "pause" )]
		Pause,

		[StringValueAttribute( "rew" )]
		Rewind,

		[StringValueAttribute( "replay" )]
		Replay,

		[StringValueAttribute( "stop" )]
		Stop,

		[StringValueAttribute( "advance" )]
		Advance,

		[StringValueAttribute( "ffwd" )]
		FastForward,

		[StringValueAttribute( "record" )]
		Record,

		[StringValueAttribute( "play" )]
		Play,

		[StringValueAttribute( "guide" )]
		Guide,

		[StringValueAttribute( "active" )]
		Active,

		[StringValueAttribute( "list" )]
		List,

		[StringValueAttribute( "exit" )]
		Exit,

		[StringValueAttribute( "back" )]
		Back,

		[StringValueAttribute( "menu" )]
		Menu,

		[StringValueAttribute( "info" )]
		Info,

		[StringValueAttribute( "up" )]
		Up,

		[StringValueAttribute( "down" )]
		Down,

		[StringValueAttribute( "left" )]
		Left,

		[StringValueAttribute( "right" )]
		Right,

		[StringValueAttribute( "select" )]
		Select,

		[StringValueAttribute( "red" )]
		Red,

		[StringValueAttribute( "green" )]
		Green,

		[StringValueAttribute( "yellow" )]
		Yellow,

		[StringValueAttribute( "blue" )]
		Blue,

		[StringValueAttribute( "chanup" )]
		ChannelUp,

		[StringValueAttribute( "chandown" )]
		ChannelDown,

		[StringValueAttribute( "prev" )]
		Previous,

		[StringValueAttribute( "0" )]
		Zero,

		[StringValueAttribute( "1" )]
		One,

		[StringValueAttribute( "2" )]
		Two,

		[StringValueAttribute( "3" )]
		Three,

		[StringValueAttribute( "4" )]
		Four,

		[StringValueAttribute( "5" )]
		Five,

		[StringValueAttribute( "6" )]
		Six,

		[StringValueAttribute( "7" )]
		Seven,

		[StringValueAttribute( "8" )]
		Eight,

		[StringValueAttribute( "9" )]
		Nine,

		[StringValueAttribute( "dash" )]
		Dash,

		[StringValueAttribute( "enter" )]
		Enter,

		[StringValueAttribute( "none" )]
		None
	}
}
