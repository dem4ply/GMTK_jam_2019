using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace platformer.animator
{
	public class Chibi_event : chibi.Chibi_behaviour
	{
		public bool want_to_invoke = false;
		public float delay = 2f;
		public UnityEvent e;

		public void invoke()
		{
			StartCoroutine( "__invoke" );
		}

		public IEnumerator __invoke()
		{
			yield return new WaitForSeconds( delay );
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
