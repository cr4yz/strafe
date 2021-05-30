using Sandbox;
using Sandbox.Hammer;
using Strafe.Ply;

namespace Strafe.Entities
{

	[Library( "trigger_timer_cp" )]
	public partial class TriggerTimerCp : BaseTrigger
	{

		public override void StartTouch( Entity other )
		{
			base.StartTouch( other );

			if ( other is not StrafePlayer player )
			{
				return;
			}

			player.SetCheckpoint(this);
		}

	}

}
