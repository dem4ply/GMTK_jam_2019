using UnityEngine;
using chibi.animator;
using System;
using UnityEngine.Events;

namespace platformer.animator
{
	public class Platformer_checkpoint : chibi.Chibi_behaviour
	{
		public Platformer_checkpoint_master master;
		public Transform respawn_point;

		private void OnTriggerEnter( Collider other )
		{
			if ( master.player.gameObject != other.gameObject )
				return;
			debug.log( "entro en el checkpoiint" );
			master.change( this );
		}
	}
}
