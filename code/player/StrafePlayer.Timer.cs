using Sandbox;
using Strafe.Entities;
using System;
using System.Text;

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

		public string FormattedTimerTime => TimeSpan.FromSeconds( TimerTime ).ToString( @"mm\:ss\.fff" );
		public int HorizontalSpeed
        {
			get => (int)Velocity.WithZ(0).Length;
			set 
			{
				var v = Velocity.WithZ(0).ClampLength(value);
				v.z = Velocity.z;
				Velocity = v;
			}
		}

		private bool _wasLeft;
		private bool _wasRight;

		public void StartTimer()
        {
			_replay.Clear();

			TimerState = TimerState.Running;
			TimerTime = 0;
			TimerJumps = 0;
			TimerStrafes = 0;

			// Don't let the player prespeed
			// If they do prespeed drop to below prestrafe velocity so it can't be abused.
			if (HorizontalSpeed >= 290)
			{
				HorizontalSpeed = 280;
			}
        }

		public void FinishTimer()
        {
			ReplayBot.WithClonedReplay(_replay);
			_replay.Clear();
		}

		private void TickTimer()
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

			if(IsServer)
            {
				_replay.Tick();
			}
		}

	}
}


