using Sandbox;
using Strafe.Ply;
using Strafe.UI;

namespace Strafe.Entities
{

	[Library( "trigger_timer_end" )]
	public partial class TriggerTimerEnd : ModelEntity
	{

		public override void Spawn()
		{
			base.Spawn();

			SetupPhysicsFromModel( PhysicsMotionType.Static );
			CollisionGroup = CollisionGroup.Trigger;
			EnableSolidCollisions = false;
			EnableTouch = true;
			EnableTouchPersists = true;

			Transmit = TransmitType.Never;
		}

		public override void StartTouch( Entity other )
		{
			base.StartTouch( other );

			if ( other is not StrafePlayer player || !Game.Current.IsServer )
			{
				return;
			}

			StrafeChatBox.AddChatEntry( Player.All, player.Name, "Timer end touched: " + player.Time.ToString(), $"avatar:{player.SteamId}" );
		}

		public override void EndTouch( Entity other )
		{
			base.EndTouch( other );

			if ( other is not StrafePlayer player )
			{
				return;
			}

			player.Time = 0;
		}

	}

}
