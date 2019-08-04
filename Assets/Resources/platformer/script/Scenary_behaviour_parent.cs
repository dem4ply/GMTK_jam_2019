using System.Collections.Generic;
using UnityEngine;
using chibi.animator;
using System;

namespace platformer.animator
{
	public class Scenary_behaviour_parent : chibi.Chibi_behaviour
	{
		public bool respawn_control = true;
		public int current_phase = 0;
		public List<Scenary_behaviour> b;

		public void phase( int p )
		{
			current_phase = p;
		}

		protected void Update()
		{
			foreach ( var s in b )
			{
				s.phase( current_phase );
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			b = new List<Scenary_behaviour>( GetComponentsInChildren<Scenary_behaviour>() );
		}
	}
}
