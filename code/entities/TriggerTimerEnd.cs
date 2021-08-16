using Sandbox;
using Strafe.Ply;
using Strafe.UI;

namespace Strafe.Entities
{

	[Library( "trigger_timer_end" )]
	public partial class TriggerTimerEnd : BaseTrigger
	{

		public override void StartTouch( Entity other )
		{
			base.StartTouch( other );

			if ( other is not StrafePlayer player || player.TimerState != TimerState.Running )
			{
				return;
			}

			player.FinishTimer();
		}

	}

}
