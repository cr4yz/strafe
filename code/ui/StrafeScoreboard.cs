using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Strafe.Ply;

namespace Strafe.UI
{
	public partial class StrafeScoreboard<T> : Panel where T : StrafeScoreboardEntry, new()
	{
		public Panel Canvas { get; protected set; }
		public Panel Header { get; protected set; }

		public StrafeScoreboard()
		{
			StyleSheet.Load("/ui/StrafeScoreboard.scss");
			AddClass("scoreboard");

			AddHeader();

			Canvas = Add.Panel("canvas");

			foreach (var player in Player.All)
			{
				AddPlayer(player as StrafePlayer);
			}

            (Game.Current as StrafeGame).OnPlayerJoined += ply => AddPlayer(ply);
			(Game.Current as StrafeGame).OnPlayerDisconnected += ply => RemovePlayer(ply);
		}

        public override void Tick()
		{
			base.Tick();

			var open = Player.Local?.Input.Down(InputButton.Score) ?? false;

			SetClass("open", open);

            if (!open)
            {
				return;
            }

			foreach(var child in Canvas.Children)
            {
				if(child is StrafeScoreboardEntry ss)
                {
					ss.UpdateText();
                }
            }
		}

		protected virtual void AddHeader()
		{
			Header = Add.Panel("header");
			Header.Add.Label("Name", "name");
			Header.Add.Label("Timer", "timer");
			Header.Add.Label("Ping", "ping");
		}

		private void AddPlayer(StrafePlayer player)
		{
			var p = Canvas.AddChild<T>();
			p.Player = player;
		}

		private void RemovePlayer(StrafePlayer player)
		{
			foreach(var child in Canvas.Children)
            {
				if(child is StrafeScoreboardEntry ss && ss.Player == player)
                {
					child.Delete();
                }
            }
		}
	}
}