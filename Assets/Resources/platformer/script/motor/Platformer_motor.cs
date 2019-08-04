using UnityEngine;
using System.Collections.Generic;
using chibi.motor;
using UnityEngine;
using System.Collections;
using System;
using platformer.animator;

namespace platformer.motor.npc
{
	public class Platformer_motor: chibi.motor.Motor
	{
		[Header( "animator" )]
		public Platformer_animator_npc animator;

		[Header( "jump" )]
		public float max_jump_heigh = 4f;
		public float min_jump_heigh = 1f;
		public float jump_time = 0.4f;

		public float max_jump_velocity;
		public float min_jump_velocity;
		public float gravity = -9.8f;

		public float multiplier_velocity_wall_slice = 0.8f;

		[Header( "wall jump" )]
		public Vector3 wall_jump_climp = new Vector3( 0, 14, 14 );
		public Vector3 wall_jump_off = new Vector3( 0, 5, 8 );
		public Vector3 wall_jump_leap = new Vector3( 0, 20, 14 );

		public float acceleration_time_in_ground = 0.1f;
		public float acceleration_time_in_air = 0.2f;

		public static string STR_WALL = "wall";
		public static string STR_WALL_left = "wall left";
		public static string STR_WALL_right = "wall right";
		public static string STR_FLOOR = "floor";

		protected bool _is_running = false;
		protected bool try_to_jump_the_next_update = false;
		protected bool _is_grounded = false;

		protected float horizontal_velocity_smooth;

		#region propiedades publicas
		public virtual bool is_grounded
		{
			get {
				return manager_collisions[ STR_FLOOR ];
			}
		}

		public virtual bool is_not_grounded
		{
			get {
				return !is_grounded;
			}
		}

		public virtual bool is_walled
		{
			get {
				return manager_collisions[ STR_WALL ];
			}
		}

		public virtual bool is_not_walled
		{
			get {
				return !is_walled;
			}
		}

		public virtual bool is_walled_left
		{
			get {
				return manager_collisions[ STR_WALL_left ];
			}
		}

		public virtual bool is_walled_right
		{
			get {
				return manager_collisions[ STR_WALL_right ];
			}
		}

		public virtual bool no_is_walled_left
		{
			get {
				return !is_walled_left;
			}
		}

		public virtual bool no_is_walled_right
		{
			get {
				return !is_walled_right;
			}
		}

		public virtual bool is_jumping
		{
			get; set;
		}
		#endregion

		public override Vector3 desire_direction
		{
			set {
				base.desire_direction = new Vector3( 0, 0, value.z );
			}
		}

		protected override void update_motion()
		{
			current_speed = desire_velocity;
			Vector3 velocity_vector = Vector3.zero;
			if ( is_grounded )
				velocity_vector = new Vector3(
					current_speed.x, ridgetbody.velocity.y,
					current_speed.z );
			else
				velocity_vector = ridgetbody.velocity;

			if ( is_grounded )
			{
				_proccess_ground_horizontal_velocity( ref velocity_vector );
				animator.speed = velocity_vector.z;
				animator.speed_up = 0f;
			}
			else
			{
				_proccess_air_horizontal_velocity( ref velocity_vector );
				animator.speed = 0f;
				animator.speed_up = velocity_vector.y;
			}
			_proccess_gravity( ref velocity_vector );
			_process_jump( ref velocity_vector );

			animator.direction = velocity_vector;
			if ( is_walled )
				if ( is_walled_left )
					animator.direction = Vector3.back;
				else
					animator.direction = Vector3.forward;
			ridgetbody.velocity = velocity_vector;
		}

		private void Update()
		{
			animator.is_walled = is_walled;
			animator.is_grounded = is_grounded;
		}

		protected virtual void _proccess_ground_horizontal_velocity(
			ref Vector3 velocity_vector )
		{
			float desire_horizontal_velocity = desire_direction.z * max_speed;
			float current_horizontal_velocity = velocity_vector.z;

			// suavizado de la velocidad horizontal
			float final_horizontal_velocity = Mathf.SmoothDamp(
				current_horizontal_velocity, desire_horizontal_velocity,
				ref horizontal_velocity_smooth, acceleration_time_in_ground );

			velocity_vector.z = final_horizontal_velocity;
		}

		protected virtual void _proccess_air_horizontal_velocity(
			ref Vector3 velocity_vector )
		{
			// int current_direction = Math.Sign( velocity_vector.x );
			int i_desire_direction = Math.Sign( desire_direction.z );
			int wall_direction = is_walled_left ? -1 : 1;
			// no hace nada porque no hay actualizacion en la direcion
			if ( i_desire_direction == 0 )
				return;
			velocity_vector = new Vector3(
				current_speed.x, velocity_vector.y,
				current_speed.z );
			if ( is_walled )
			{
				if ( i_desire_direction == wall_direction )
				{
					velocity_vector.z = 0;
				}
			}
			else
			{

				float desire_horizontal_velocity = desire_direction.z * max_speed;

				float current_horizontal_velocity = velocity_vector.z;

				// suavizado de la velocidad horizontal
				float final_horizontal_velocity = Mathf.SmoothDamp(
					current_horizontal_velocity, desire_horizontal_velocity,
					ref horizontal_velocity_smooth, acceleration_time_in_air );

				velocity_vector.z = final_horizontal_velocity;
			}
			current_speed = velocity_vector;
		}

		protected virtual void _process_jump( ref Vector3 speed_vector )
		{
			if ( try_to_jump_the_next_update )
			{
				if ( is_walled && is_not_grounded )
				{
					int jump_direction = is_walled_left ? -1 : 1;
					if ( Math.Sign( desire_direction.z ) == jump_direction )
					{
						speed_vector.z = -jump_direction * wall_jump_climp.z;
						speed_vector.y = wall_jump_climp.y;
					}
					else if ( desire_direction.z == 0 )
					{
						speed_vector.z = -jump_direction * wall_jump_off.z;
						speed_vector.y = wall_jump_off.y;
					}
					else
					{
						speed_vector.z = -jump_direction * wall_jump_leap.z;
						speed_vector.y = wall_jump_leap.y;
					}
				}
				else if ( is_grounded )
				{
					speed_vector.y = max_jump_velocity;
				}
			}
			else if ( speed_vector.y > min_jump_velocity )
				speed_vector.y = min_jump_velocity;
		}

		protected virtual void _proccess_gravity(
				ref Vector3 velocity_vector )
		{
			velocity_vector.y += ( gravity * Time.deltaTime );

			if ( is_not_grounded && is_walled )
				velocity_vector.y *= multiplier_velocity_wall_slice;
		}

		public void jump()
		{
			try_to_jump_the_next_update = true;
		}
		public void stop_jump()
		{
			try_to_jump_the_next_update = false;
		}

		protected override void Start()
		{
			base.Start();
			gravity = -( 2 * max_jump_heigh ) / ( jump_time * jump_time );
			max_jump_velocity = Math.Abs( gravity ) * jump_time;
			min_jump_velocity = ( float )Math.Sqrt(
				2.0 * Math.Abs( gravity ) * min_jump_heigh );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			animator = GetComponent<Platformer_animator_npc>();
			if ( !animator )
				debug.error( "no se encontro platformer animator npc" );
		}
	}
}
