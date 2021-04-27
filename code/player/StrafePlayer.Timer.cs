using Sandbox;
using Strafe.Weapons;
using System;

namespace Strafe.Ply
{
	public enum TimerState 
	{
		InStartZone,
		Running,
		Ended
	}

	public partial class StrafePlayer
	{

		[Net]
		public TimerState TimerState { get; set; }
		[Net]
		public int TimerJumps { get; set; }
		[Net]
		public int TimerStrafes { get; set; }
		[Net]
		public float TimerTime { get; set; }

		public string FormattedTimerTime => TimeSpan.FromSeconds( TimerTime ).ToString( @"mm\:ss\:fff" );

		protected void TickTimer()
		{
			if(!Game.Current.IsAuthority)
			{
				DebugOverlay.ScreenText( 0, $"             Timer: {TimerState}", Sandbox.Time.Delta * 3f );
				DebugOverlay.ScreenText( 1, $"             Time: {FormattedTimerTime}", Sandbox.Time.Delta * 3f );
				DebugOverlay.ScreenText( 2, $"             Jumps: {TimerJumps}", Sandbox.Time.Delta * 3f );

				return;
			}

			if ( TimerState != TimerState.Running )
			{
				return;
			}

			if(DevController != null)
            {
				TimerState = TimerState.Ended;
				return;
            }

			var walk = Controller as StrafeWalkController;
			if(walk.JustJumped)
			{
				TimerJumps++;
			}

			TimerTime += Time.Delta;
		}

		public int HorizontalSpeed()
        {
			var result = Velocity;
			result.z = 0;
			return (int)result.Length;
        }

		public void ClampHorizontalVelocity(int speed)
        {
			var vel = Velocity;
			vel.z = 0;
			vel = vel.ClampLength(speed);
			vel.z = Velocity.z;
			Velocity = vel;
        }

	}
}


