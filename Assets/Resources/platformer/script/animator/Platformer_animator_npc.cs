using UnityEngine;
using chibi.animator;
using System;

namespace platformer.animator
{
	public class Platformer_animator_npc : Animator_base
	{
		public float speed
		{
			get{
				return animator.GetFloat( "Speed" );
			}
			set {
				animator.SetFloat( "Speed", Math.Abs( value ) );
			}
		}

		public float speed_up
		{
			get{
				return animator.GetFloat( "SpeedUp" );
			}
			set {
				animator.SetFloat( "SpeedUp", value );
			}
		}

		public bool is_walled
		{
			get{
				return animator.GetBool( "IsWalled" );
			}
			set {
				animator.SetBool( "IsWalled", value );
			}
		}

		public bool is_grounded
		{
			get{
				return animator.GetBool( "IsGrounded" );
			}
			set {
				animator.SetBool( "IsGrounded", value );
			}
		}

		public bool victory
		{
			get{
				return animator.GetBool( "IsWin" );
			}
			set {
				animator.SetBool( "IsWin", value );
			}
		}

		public Vector3 direction
		{
			get {
				var x = animator.GetFloat( "horizontal" );
				//var z = animator.GetFloat( "vertical" );
				return new Vector3( 0, 0, x );
			}
			set {
				animator.SetFloat( "horizontal", Math.Sign( value.z ) );
				//animator.SetFloat( "vertical", dir.y );
			}
		}
	}
}
