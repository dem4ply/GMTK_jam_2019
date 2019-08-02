using UnityEngine;

namespace chibi.controller
{
	public class Controller : Chibi_behaviour
	{
		protected Vector3 _desire_direction;
		protected float _speed;

		public virtual Vector3 desire_direction
		{
			get {
				return _desire_direction;
			}

			set {
				_desire_direction = value;
			}
		}

		public virtual float speed {
			get {
				return _speed;
			}

			set {
				_speed = value;
			}
		}

		public virtual void action( string name, string e )
		{
			Debug.Log( string.Format(
				"[{0}] action '{1}' with the event '{2}'",
				helper.game_object.name.full( this ), name, e
			) );
		}
	}
}
