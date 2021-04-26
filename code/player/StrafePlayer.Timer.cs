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

		public string FormattedTime => TimeSpan.FromSeconds( TimerTime ).ToString( @"mm\:ss\:fff" );

		protected void TickTimer()
		{
			if(!Game.Current.IsAuthority)
			{
				DebugOverlay.ScreenText( 0, $"             Timer: {TimerState}", Sandbox.Time.Delta * 3f );
				DebugOverlay.ScreenText( 1, $"             Time: {FormattedTime}", Sandbox.Time.Delta * 3f );
				DebugOverlay.ScreenText( 2, $"             Jumps: {TimerJumps}", Sandbox.Time.Delta * 3f );

				return;
			}

			if ( TimerState != TimerState.Running )
			{
				return;
			}

			var walk = Controller as StrafeWalkController;
			if(walk.JustJumped)
			{
				TimerJumps++;
			}

			TimerTime += Time.Delta;
		}

	}
}


