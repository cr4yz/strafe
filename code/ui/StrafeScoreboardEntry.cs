using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Strafe.Ply;

namespace Strafe.UI
{
	public partial class StrafeScoreboardEntry : Panel
	{
		public Client Client;

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

			var sp = Client.Pawn as StrafePlayer;
			if(sp == null)
			{
				return;
			}

			var timerText = sp.TimerState == TimerState.Running
				? sp.FormattedTimerTime
				: sp.TimerState.ToString();

			PlayerName.Text = Client.Name;
			Timer.Text = timerText;
			Ping.Text = "n/a";

			SetClass("me", sp.IsLocalPawn);
		}

	}
}
