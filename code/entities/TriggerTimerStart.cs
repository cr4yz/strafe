using Sandbox;
using Sandbox.Hammer;
using Strafe.Ply;

namespace Strafe.Entities
{

	[Library( "trigger_timer_start" )]
	public partial class TriggerTimerStart : BaseTrigger
	{

		public override void StartTouch( Entity other )
		{
			base.StartTouch( other );

			if ( other is not StrafePlayer player )
			{
				return;
			}

			player.TimerState = TimerState.InStartZone;
		}

		public override void EndTouch( Entity other )
		{
			base.EndTouch( other );

			if ( other is not StrafePlayer player )
			{
				return;
			}

			player.StartTimer();
		}

	}

}
