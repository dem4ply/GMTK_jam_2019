using UnityEngine;
using System.Collections.Generic;
using chibi.controller;

namespace chibi.joystick
{
	public class Joystick : chibi.Chibi_behaviour
	{
		#region public vars
		public string key_map = "player 1";
		public Controller controller;

		public Axis desire_direction;

		public List<string> actions = new List<string>() {
			"fire1", "fire2", "fire3", };
		#endregion

		#region public properties
		#endregion

		#region public functions
		public void update_all_axis()
		{
			desire_direction.update();
		}
		#endregion

		#region funciones protegdas
		protected void Update()
		{
			update_all_axis();
			if ( desire_direction.pass_dead_zone )
			{
				controller.desire_direction = desire_direction.vector;
				controller.speed = 1f;
			}
			else
			{
				controller.desire_direction = Vector3.zero;
				controller.speed = 0f;
			}

			foreach ( string action in actions )
			{
				if ( check_action_down( action ) )
					controller.action( action, "down" );
				if ( check_action_up( action ) )
					controller.action( action, "up" );
			}
		}

		/// <summary>
		/// inicializa el chache del script
		/// </summary>
		protected override void _init_cache()
		{
			if ( controller == null )
				controller = GetComponent<Controller>();
			if ( !desire_direction )
			{
				debug.error( "no hay un axis de desire_direction" );
			}
		}

		protected virtual bool check_action_down( string action )
		{
			return Input.GetButtonDown( action );
		}

		protected virtual bool check_action_up( string action )
		{
			return Input.GetButtonUp( action );
		}
		#endregion
	}
}
