using System.Collections;
using UnityEngine;
using weapon.ammo;
using chibi.pomodoro;

namespace chibi.motor.weapons.gun.bullet
{
	public class Bullet_motor : Motor
	{
		public Ammo ammo;
		public float life_span;
		public bool alway_rotate_to_velocity_direction = true;
		public float time_to_disable = 1f;
		protected IEnumerator __life_span;

		protected override void _init_cache()
		{
			base._init_cache();
			prepare_life_span();
		}

		protected virtual void prepare_life_span()
		{
			__life_span = recicle_when_life_span_end();
			StartCoroutine( __life_span );
			StartCoroutine( "_disable_in" );
		}

		protected virtual IEnumerator recicle_when_life_span_end()
		{
			yield return new WaitForSeconds( life_span );
			recycle();
		}

		protected virtual IEnumerator _disable_in()
		{
			yield return new WaitForSeconds( time_to_disable );
			set_static_next_update();
		}

		public override void recycle()
		{
			ammo.push( this );
		}

		public override Vector3 desire_direction
		{
			get
			{
				return base.desire_direction;
			}
			set
			{
				if ( alway_rotate_to_velocity_direction && value != Vector3.zero )
					transform.rotation = Quaternion.LookRotation(
						value, transform.up );
				base.desire_direction = value;
			}
		}
	}
}
