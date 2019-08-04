using UnityEngine;
using chibi.animator;
using System;

namespace platformer.animator
{
	public class Scenary_behaviour : chibi.Chibi_behaviour
	{
		public bool respawn_control = true;
		public int current_phase = 0;
		public Transform phase_1;
		public Transform phase_2;
		public Transform phase_3;

		public chibi.controller.steering.Steering steering;

		public Color gizmo_color = Color.blue;

		public void phase( int p )
		{
			current_phase = p;
		}

		protected void Update()
		{
			if ( current_phase == 0 )
				steering.target = phase_1;
			if ( current_phase == 1 )
				steering.target = phase_2;
		}

		protected override void _init_cache()
		{
			base._init_cache();
			steering = GetComponent< chibi.controller.steering.Steering >();
			if ( !steering )
				steering = GetComponentInChildren< chibi.controller.steering.Steering >();
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = gizmo_color;
			if ( phase_1 && phase_2 )
				Gizmos.DrawLine( phase_1.position, phase_2.position );
		}
	}
}
