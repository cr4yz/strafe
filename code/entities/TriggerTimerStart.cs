using Sandbox;

namespace Strafe.Entities
{

	[Library("trigger_timer_start", Title = "Timer Start")]
	public class TriggerTimerStart : ModelEntity
	{

		public override void StartTouch( Entity other )
		{
			base.StartTouch( other );


		}

		public override void EndTouch( Entity other )
		{
			base.EndTouch( other );

			Log.Info( "Exited start zone!!!" );
		}

	}

}
