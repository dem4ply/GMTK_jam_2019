using UnityEngine;
using UnityEngine.Events;

namespace platformer.animator
{
	public class On_trigger : chibi.Chibi_behaviour
	{
		public GameObject player;
		public UnityEvent on_trigger_enter;
		public UnityEvent on_trigger_exit;

		private void OnTriggerEnter( Collider other )
		{
			if ( player == other.gameObject )
				on_trigger_enter.Invoke();
		}

		private void OnTriggerExit( Collider other )
		{
			if ( player == other.gameObject )
				on_trigger_exit.Invoke();
		}
	}
}
