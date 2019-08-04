using UnityEngine;

namespace helper
{
	namespace debug
	{
		public class Debug
		{
			protected MonoBehaviour _instance;
			public draw.Draw draw;

			public bool debuging
			{
					get {
						var a = _instance as chibi.Chibi_behaviour;
						if ( a )
							return a.debug_mode;
						var b = _instance as chibi.Chibi_behaviour;
						if ( b )
							return b.debug_mode;
						return false;
					}
			}

			public Debug( chibi.Chibi_behaviour instance )
			{
				_instance = instance;
				draw = new draw.Draw( _instance );
			}

			public void info( string msg )
			{
			}

			public void log( string msg )
			{
				info( msg );
			}

			public void warning( string msg )
			{
			}

			public void error( string msg )
			{
			}

			protected string full_name
			{
				get { return helper.game_object.name.full( _instance ); }
			}

			protected string type_name
			{
				get { return _instance.GetType().Name; }
			}
		}
	}
}
