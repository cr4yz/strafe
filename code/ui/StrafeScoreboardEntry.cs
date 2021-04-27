
using Sandbox;
using Sandbox.Hooks;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Strafe.Ply;
using System;
using System.Collections.Generic;

namespace Strafe.UI
{
	public partial class StrafeScoreboardEntry : Panel
	{
		public StrafePlayer Player;

		public Label PlayerName;
		public Label Timer;
		public Label Ping;

		public StrafeScoreboardEntry()
		{
			AddClass("entry");

			PlayerName = Add.Label("PlayerName", "name");
			Timer = Add.Label("", "timer");
			Ping = Add.Label("", "ping");
		}

		public void UpdateText()
        {
			base.Tick();

			var timerText = Player.TimerState == TimerState.Running
				? Player.FormattedTimerTime
				: Player.TimerState.ToString();

			PlayerName.Text = Player.Name;
			Timer.Text = timerText;
			Ping.Text = "n/a";

			SetClass("me", Player.IsLocalPlayer);
		}

	}
}
