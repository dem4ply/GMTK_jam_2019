using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace chibi.motor
{
	[ RequireComponent( typeof( Rigidbody ) ) ]
	public class Motor : chibi.Chibi_behaviour
	{
		public Vector3 current_speed = Vector3.zero;
		public float desire_speed;
		public float max_speed = 4f;
		public unsigned_vector3 period_to_desice_direction;

		public bool is_steering = false;

		public float steering_mass = 1f;
		protected Vector3 smooth_velocity = Vector3.zero;
		public Vector3 velocity_acceleration = Vector3.zero;

		private Vector3 _desire_direction;
		public manager.Collision manager_collisions;

		protected Rigidbody ridgetbody;

		public Vector3 velocity
		{
			get {
				return ridgetbody.velocity;
			}
		}

		public virtual Vector3 desire_velocity
		{
			get {
				var desire_speed_vector = desire_direction.normalized
					* Mathf.Clamp( desire_speed, 0, max_speed );

				debug.draw.arrow( desire_speed_vector, Color.magenta );
				float final_x = Mathf.SmoothDamp(
					velocity.x, desire_speed_vector.x,
					ref smooth_velocity.x, velocity_acceleration.x );

				float final_y = Mathf.SmoothDamp(
					velocity.y, desire_speed_vector.y,
					ref smooth_velocity.y, velocity_acceleration.y );

				float final_z = Mathf.SmoothDamp(
					velocity.z, desire_speed_vector.z,
					ref smooth_velocity.z, velocity_acceleration.z );

				var final_speed = new Vector3( final_x, final_y, final_z );
				debug.draw.arrow( final_speed, Color.blue );
				if ( is_steering )
				{
					var steering = final_speed - velocity;
					steering /= steering_mass;
					debug.draw.arrow( velocity + steering, Color.yellow );
					return ( velocity + steering );
				}
				else
					return final_speed;
			}
		}

		public virtual Vector3 desire_direction
		{
			get {
				return _desire_direction;
			}

			set {
				_desire_direction = value;
			}
		}

		public void set_static_next_update()
		{
			StartCoroutine( "_set_static_next_update" );
		}

		protected IEnumerator _set_static_next_update()
		{
			yield return new WaitForFixedUpdate();
			enabled = false;
		}

		protected override void _init_cache()
		{
			base._init_cache();
			ridgetbody = GetComponent<Rigidbody>();
			if ( !ridgetbody )
				Debug.Log( string.Format(
					"no se encontro un ridgetbody en el en el motor '{0}'",
					helper.game_object.name.full( this ) ) );
		}

		protected virtual void update_motion()
		{
			ridgetbody.velocity = desire_velocity;
			current_speed = desire_velocity;
		}

		protected virtual void FixedUpdate()
		{
			update_motion();
		}
	}
}
