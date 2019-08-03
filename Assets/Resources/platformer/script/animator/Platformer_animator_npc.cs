using UnityEngine;
using chibi.animator;

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
				animator.SetFloat( "Speed", value );
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

		public Vector3 direction
		{
			get {
				var x = animator.GetFloat( "horizontal" );
				var z = animator.GetFloat( "vertical" );
				return new Vector3( x, 0, z );
			}
			set {
				var dir = new Vector3( value.x, value.z, 0 );
				animator.SetFloat( "horizontal", dir.x );
				animator.SetFloat( "vertical", dir.y );
			}
		}
	}
}
