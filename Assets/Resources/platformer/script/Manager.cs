using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace platformer.game_manager
{
	public class Manager_game : chibi.game_manager.Manager
	{
		//public UnityEvent on_start;

		public platformer.controller.player.Platformer_player_controller player;
	}
}
