using UnityEngine;
using UnityEngine.Events;

namespace platformer.animator
{
	public class Any_key_event : chibi.Chibi_behaviour
	{
		public UnityEvent e;

		private void Update()
		{
			if ( Input.anyKey )
			{
				e.Invoke();
			}
		}

	}
}
