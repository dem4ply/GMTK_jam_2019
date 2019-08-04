using UnityEngine;
using chibi.animator;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

namespace platformer.animator
{
	public class Platformer_checkpoint_master : chibi.Chibi_behaviour
	{
		public List<Platformer_checkpoint> chekcpoints;
		public Platformer_checkpoint current_checkpoint;
		public Transform player;

		public Scenary_time_behaviour curtains;
		public chibi.joystick.Joystick joystick;
		public float time_to_close_curtains = 2f;
		public float time_to_open_curtains = 2f;
		public float time_to_restore_scenary = 2f;

		public List<Scenary_behaviour> b;
		public List<Scenary_behaviour_parent> c;

		public List<(Scenary_behaviour, int)> save_b;
		public List<(Scenary_behaviour_parent, int)> save_c;

		public IEnumerator __timer;

		public void start_respawn()
		{
			__timer = _spawn();
			StartCoroutine( __timer );
		}

		protected IEnumerator _spawn()
		{
			curtains.move( 0 );
			joystick.enabled = false;
			joystick.reset();
			yield return new WaitForSeconds( time_to_close_curtains );
			player.position = current_checkpoint.respawn_point.position;
			reset_scenary();
			yield return new WaitForSeconds( time_to_restore_scenary );

			curtains.move( 1 );
			yield return new WaitForSeconds( time_to_open_curtains );
			joystick.enabled = true;
		}

		public void change( Platformer_checkpoint check )
		{
			Debug.Log( check );
			if ( check == current_checkpoint )
				return;
			if ( current_checkpoint )
				Destroy( current_checkpoint.gameObject );
			current_checkpoint = check;
			save_scenary();
		}

		protected override void _init_cache()
		{
			base._init_cache();
			chekcpoints = new List<Platformer_checkpoint>(
				FindObjectsOfType<Platformer_checkpoint>() );
			foreach ( var c in chekcpoints )
				c.master = this;

			b = new List<Scenary_behaviour>(
				FindObjectsOfType<Scenary_behaviour>() );

			c = new List<Scenary_behaviour_parent>(
				FindObjectsOfType<Scenary_behaviour_parent>() );

			if ( save_b == null )
				save_b = new List<(Scenary_behaviour, int)>();

			if ( save_c == null )
				save_c = new List<(Scenary_behaviour_parent, int)>();

			save_scenary();
		}

		public void save_scenary()
		{
			save_b.Clear();
			save_c.Clear();
			foreach ( var a in b )
				save_b.Add( ( a, a.current_phase ) );

			foreach ( var a in c )
				save_c.Add( ( a, a.current_phase ) );
		}

		public void reset_scenary( )
		{
			foreach ( var a in save_b )
				if ( a.Item1.respawn_control )
					a.Item1.phase( a.Item2 );
			foreach ( var a in save_c )
				if ( a.Item1.respawn_control )
					a.Item1.phase( a.Item2 );
		}
	}
}
