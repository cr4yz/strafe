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

		private bool _wasLeft;
		private bool _wasRight;

		protected void TickTimer()
		{
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

			var isLeft = Input.Down(InputButton.Left);
			var isRight = Input.Down(InputButton.Right);

			if ((isLeft && !_wasLeft) || (isRight && !_wasRight))
			{
				TimerStrafes++;
			}

			_wasLeft = isLeft;
			_wasRight = isRight;

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


