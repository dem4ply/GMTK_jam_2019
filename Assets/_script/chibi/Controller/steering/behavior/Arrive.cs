using UnityEngine;

namespace chibi.controller.steering.behavior
{
	[CreateAssetMenu( menuName = "chibi/steering/behavior/arrive" )]
	public class Arrive : chibi.controller.steering.behavior.Behavior
	{
		public float stop_distance = 0.1f;
		public float mult_unacelarate = 1f;
		public float dist_unacelarate = 2f;

		public float remap( float value, float from1, float to1, float from2, float to2 )
		{
			return ( value - from1 ) / ( to1 - from1 ) * ( to2 - from2 ) + from2;
		}

		public override Vector3 desire_direction(
			Steering controller, Transform target,
			Steering_properties properties )
		{
			if ( !target )
				return Vector3.zero;
			controller.controller.speed = controller.controller.max_speed;
			var direction = seek( controller, target.position );
			direction.Normalize();
			var distance_to_target = Vector3.Distance( controller.transform.position, target.position );
			if ( distance_to_target < dist_unacelarate )
			{
				float q = remap( distance_to_target, 0, dist_unacelarate, 0, controller.controller.max_speed );
				controller.controller.speed = q;
			}
			if ( distance_to_target <= stop_distance )
				controller.controller.speed = 0f;
			return direction.normalized;
		}

		public override float desire_speed(
			Steering controller, Transform target,
			Steering_properties properties )
		{
			return 1f;
		}

		public virtual void debug(
			Controller controller, Transform target, Vector3 direction )
		{
			controller.debug.draw.arrow( direction, seek_color );
		}
	}
}
