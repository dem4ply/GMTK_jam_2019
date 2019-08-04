using chibi.animator;

namespace platformer.animator
{
	public class Animator_intro : Animator_base
	{
		public void start( bool value )
		{
			animator.SetBool( "start", value );
		}
	}
}
