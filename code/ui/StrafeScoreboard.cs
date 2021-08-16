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

			foreach (var client in Client.All)
			{
				AddPlayer(client);
			}

            (Game.Current as StrafeGame).OnPlayerJoined += client => AddPlayer(client);
			(Game.Current as StrafeGame).OnPlayerDisconnected += client => RemovePlayer(client);
		}

        public override void Tick()
		{
			base.Tick();

			var open = Input.Down(InputButton.Score);

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

		private void AddPlayer(Client client)
		{
			var p = Canvas.AddChild<T>();
			p.Client = client;
		}

		private void RemovePlayer(Client client)
		{
			foreach(var child in Canvas.Children)
            {
				if(child is StrafeScoreboardEntry ss && ss.Client == client)
                {
					child.Delete();
                }
            }
		}
	}
}
