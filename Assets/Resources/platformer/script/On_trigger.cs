using UnityEngine;
using chibi.animator;
using System;
using UnityEngine.Events;

namespace platformer.animator
{
	public class On_trigger : chibi.Chibi_behaviour
	{
		public UnityEvent on_trigger_enter;
		public UnityEvent on_trigger_exit;

		private void OnTriggerEnter( Collider other )
		{
			on_trigger_enter.Invoke();
		}

		private void OnTriggerExit( Collider other )
		{
			on_trigger_exit.Invoke();
		}
	}
}
