using UnityEngine;
using chibi.animator;
using System;
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
