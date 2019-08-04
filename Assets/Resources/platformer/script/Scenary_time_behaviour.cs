using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer.animator
{
	public class Scenary_time_behaviour : chibi.Chibi_behaviour
	{
		public int desire_phase = 0;
		public float time = 1f;
		public IEnumerator __timer;
		public List<Scenary_behaviour> b;
		public List<Scenary_behaviour_parent> c;

		protected override void Start()
		{
			base.Start();
			//start_timer();
		}

		public void start_timer( int phase )
		{
			__timer = timer();
			desire_phase = phase;
			StartCoroutine( __timer );
		}

		protected IEnumerator timer()
		{
			yield return new WaitForSeconds( time );
			move( desire_phase );
		}

		public void move( int i )
		{
			foreach ( var a in b )
				a.phase( i );
			foreach ( var a in c )
				a.phase( i );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			b = new List<Scenary_behaviour>( GetComponentsInChildren<Scenary_behaviour>() );
			c = new List<Scenary_behaviour_parent>( GetComponentsInChildren<Scenary_behaviour_parent>() );
		}
	}
}
