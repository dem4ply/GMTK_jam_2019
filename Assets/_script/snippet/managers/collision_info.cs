using UnityEngine;

namespace manager
{
	public class Collision_info {
		public string name;
		public GameObject game_object;
		public UnityEngine.Collision collision;

		public Collision_info( string name, UnityEngine.Collision collision )
			: this( name, collision, collision.gameObject )
		{}

		public Collision_info(
			string name, UnityEngine.Collision collision, GameObject game_object )
		{
			this.name = name;
			this.collision = collision;
			this.game_object = game_object;
		}
	}
}
