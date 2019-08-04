using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using chibi.animator;
using System;

namespace platformer.animator
{
	public class Chibi_event : chibi.Chibi_behaviour
	{
		public bool want_to_invoke = false;
		public UnityEvent e;

		public void invoke()
		{
			e.Invoke();
		}

		private void Update()
		{
			if ( want_to_invoke )
			{
				invoke();
				want_to_invoke = false;
			}
		}
	}
}
