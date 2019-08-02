using UnityEngine;
using chibi.controller.npc;

namespace platformer.controller.player
{
	public class Platformer_player_controller : chibi.controller.Controller
	{
		public  Controller_npc player;

		public override Vector3 desire_direction
		{
			get
			{
				return player.desire_direction;
			}
			set
			{
				player.desire_direction = value;
			}
		}

		public override float speed
		{
			get
			{
				return player.speed;
			}
			set
			{
				if ( value >= 1f )
					player.speed = player.max_speed;
				else
					player.speed = 0f;
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !player )
				debug.error( "no esta asignado el player controller" );
		}

		public override void action( string name, string e )
		{
			switch ( name )
			{
				case "fire1":
					switch ( e )
					{
						case chibi.joystick.events.down:
							break;
						case chibi.joystick.events.up:
							break;
					}
					break;
			}
		}
	}
}
