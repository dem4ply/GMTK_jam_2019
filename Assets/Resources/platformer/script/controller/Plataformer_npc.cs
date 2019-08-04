﻿using UnityEngine;
using System.Collections.Generic;
using platformer.motor.npc;

namespace platformer.controller.npc
{
	public class Plataformer_npc : chibi.controller.Controller_motor
	{

		[Header( "Wall manager" )]
		public Vector3 angle_vector_for_floor = Vector3.left;
		public float min_angle_for_floor = 20f;
		public float max_angle_for_floor = 160;

		public Vector3 angle_vector_for_wall = Vector3.up;
		public float min_angle_for_wall = 70f;
		public float max_angle_for_wall = 110;

		public static string STR_WALL = "wall";
		public static string STR_WALL_left = "wall left";
		public static string STR_WALL_right = "wall right";
		public static string STR_FLOOR = "floor";

		public chibi.damage.motor.HP_engine hp;

		protected manager.Collision manager_collisions;

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

		protected override void _init_cache()
		{
			manager_collisions = new manager.Collision();
			base._init_cache();
			hp = GetComponent<chibi.damage.motor.HP_engine>();
		}

		public virtual void victory()
		{
			var motor_npc = motor as Platformer_motor;
			motor_npc.animator.victory = true;
		}

		#region manejo de salto
		public virtual void jump()
		{
			var motor_npc = motor as Platformer_motor;
			motor_npc.jump();
		}

		public virtual void stop_jump()
		{
			var motor_npc = motor as Platformer_motor;
			motor_npc.stop_jump();
		}
		#endregion

		#region manejo de coliciones
		protected virtual void _proccess_collision( Collision collision )
		{
			if ( chibi.tag.consts.is_scenary( collision ) )
			{
				__validate_normal_points( collision );
				_process_collision_scenary( collision );
			}
		}

		protected virtual void _process_collision_scenary( Collision collision )
		{
			_check_is_collision_is_a_floor( collision );
			_check_is_collision_is_a_wall( collision );
		}

		protected virtual void _check_is_collision_is_a_floor(
			Collision collision )
		{
			foreach ( ContactPoint contact in collision.contacts )
			{
				float angle = Vector3.Angle(
					angle_vector_for_floor, contact.normal );
				if ( helper.math.between(
					angle, min_angle_for_floor, max_angle_for_floor ) )
				{
					manager_collisions.add( new manager.Collision_info(
						STR_FLOOR, collision ) );
					break;
				}
			}
		}

		protected virtual void _check_is_collision_is_a_wall(
			Collision collision )
		{
			foreach ( ContactPoint contact in collision.contacts )
			{
				float angle = Vector2.Angle(
					angle_vector_for_wall, contact.normal );
				if ( helper.math.between(
						angle, min_angle_for_wall, max_angle_for_wall )
						|| contact.normal == Vector3.forward
						|| contact.normal == Vector3.back )
				{
					manager_collisions.add(
						new manager.Collision_info( STR_WALL, collision ) );

					if ( contact.normal.z > 0 )
					{
						manager_collisions.add(
							new manager.Collision_info( STR_WALL_left, collision ) );
						debug.log( "left" );
					}
					else if ( contact.normal.z < 0 )
					{
						manager_collisions.add(
							new manager.Collision_info( STR_WALL_right, collision ) );
						debug.log( "righ" );
					}
				}
			}
		}

		protected virtual void OnCollisionEnter( Collision collision )
		{
			_proccess_collision( collision );
		}

		protected virtual void OnCollisionExit( Collision collision )
		{
			manager_collisions.remove( collision.gameObject );
		}
		#endregion

		#region debug functions
		protected virtual void __validate_normal_points( Collision collision )
		{
			List<Vector3> normal_points = new List<Vector3>();
			foreach ( ContactPoint contact in collision.contacts )
			{
				normal_points.Add( contact.normal );
			}
			Vector3 first = normal_points[ 0 ];
			for ( int i = 1; i < normal_points.Count; ++i )
				if ( first != normal_points[ i ] )
				{
					string msg = string.Format(
						"se encontro una colision en la que los normal points " +
						"no son iguales con {0} y {1}, lista de nomral" +
						"points {2}", this, collision.gameObject, normal_points );
					Debug.LogWarning( msg );
				}
		}
		#endregion

		protected override void load_motors()
		{
			base.load_motors();
			motor.manager_collisions = manager_collisions;
		}

		#region hp
		public virtual void died()
		{
			if ( !hp )
			{
				debug.error( "no tiene un HP_engine" );
			}
			else
				hp.died();
		}
		#endregion
	}
}
