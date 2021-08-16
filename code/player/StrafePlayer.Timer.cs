using Sandbox;
using Strafe.Entities;
using Strafe.UI;
using System;
using System.Collections.Generic;
using System.Linq;
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
		[Net]
		public int TimerCheckpoint { get; set; }
		[Net]
		public int TimerStage { get; set; }

		public string FormattedTimerTime => TimeSpan.FromSeconds(TimerTime).ToString(@"mm\:ss\.fff");
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
		private HashSet<TriggerTimerCp> _touchedCheckpoints = new HashSet<TriggerTimerCp>();

		public void StartTimer()
		{
			_replay.Clear();

			TimerState = TimerState.Running;
			TimerTime = 0;
			TimerJumps = 0;
			TimerStrafes = 0;
			TimerCheckpoint = 0;
			TimerStage = 0;

			_touchedCheckpoints.Clear();

			// Don't let the player prespeed
			// If they do prespeed drop to below prestrafe velocity so it can't be abused.
			if (HorizontalSpeed >= 290)
			{
				HorizontalSpeed = 280;
			}
		}

		public void FinishTimer()
		{
			if (!IsServer
				|| TimerState != TimerState.Running)
			{
				return;
			}

			TimerState = TimerState.Ended;

			ReplayBot.WithClonedReplay(_replay);
			_replay.Clear();

			var cl = Client.All.FirstOrDefault(x => x.Pawn == this);
			if (cl == null)
			{
				return;
			}

			StrafeChatBox.AddChatEntry(To.Everyone, cl.Name, $"Map completed in {FormattedTimerTime}s", $"avatar:{cl.SteamId}");
		}

		public void SetCheckpoint(TriggerTimerCp cp)
		{
			if (!IsServer
				|| TimerState != TimerState.Running
				|| _touchedCheckpoints.Contains(cp))
			{
				return;
			}

			_touchedCheckpoints.Add(cp);
			TimerCheckpoint++;

			var cl = Client.All.FirstOrDefault(x => x.Pawn == this);
			if (cl == null)
			{
				return;
			}

			StrafeChatBox.AddChatEntry(To.Single(cl), cl.Name, $"Checkpoint #{TimerCheckpoint} in {FormattedTimerTime}s", $"avatar:{cl.SteamId}");
		}

		[Event.Tick]
		private void TickTimer()
		{
			if (IsClient
				|| TimerState != TimerState.Running)
			{
				return;
			}

			if (DevController != null)
			{
				TimerState = TimerState.Ended;
				return;
			}

			var cl = Client.All.FirstOrDefault(x => x.Pawn == this);

			if (cl is not StrafePlayer)
			{
			}

			var walk = Controller as StrafeWalkController;
			if (walk.JustJumped)
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

			_replay.Tick();
		}

	}
}


