using UnityEngine;

namespace platformer.animator
{
	public class Start_respawn : chibi.Chibi_behaviour
	{
		public Platformer_checkpoint_master master;
		private void OnTriggerEnter( Collider other )
		{
			if ( master.player.gameObject != other.gameObject )
				return;
			debug.log( "en el respaswn" );
			master.start_respawn();
		}
	}
}
