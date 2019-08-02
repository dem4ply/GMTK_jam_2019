using System.Collections.Generic;
using UnityEngine;
using weapon.ammo;
using chibi.pomodoro;

namespace chibi.motor.weapons.gun.bullet
{
	public class Bullet_step_motor : Bullet_motor
	{
		[Header( "step motor" )]
		public int index_step = 0;
		public List< Bullet_step > steps;

		protected bool stop_steps = false;

		protected virtual void step()
		{
			var step = steps[ index_step ];
			step.update( this );
		}

		protected override void update_motion()
		{
			fixed_update_life_span();
			if ( !stop_steps )
				step();
			base.update_motion();
		}

		protected virtual void fixed_update_life_span()
		{
			if ( stop_steps )
				return;
			var step = steps[ index_step ];
			if ( step.tick( Time.fixedDeltaTime ) )
			{
				if ( ++index_step < steps.Count )
				{
					step = steps[ index_step ];
					step.start( this );
				}
				else
				{
					stop_steps = true;
					set_static_next_update();
				}
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			reset();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
		}

		public override void reset()
		{
			index_step = 0;
			stop_steps = false;
			foreach ( var step in steps )
			{
				step.reset();
				step.prepare( this );
			}
			steps[ 0 ].start( this );
			base.reset();
		}
	}
}
