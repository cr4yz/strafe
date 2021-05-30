using Sandbox;
using Strafe.UI;
using Strafe.Ply;
using System;
using Strafe.Entities;

namespace Strafe
{
	[Library("strafe", Title = "Strafe")]
	partial class StrafeGame : Game
	{

		public event Action<Client> OnPlayerJoined;
		public event Action<Client> OnPlayerDisconnected;

		public StrafeGame()
		{
			if (IsServer)
			{
				new StrafeHud();
			}
		}

		public override void ClientJoined(Client cl)
		{
			base.ClientJoined(cl);

			var player = new StrafePlayer();
			player.Respawn();

			cl.Pawn = player;

			OnPlayerJoined?.Invoke(cl);
		}

		public override void ClientDisconnect(Client cl, NetworkDisconnectionReason reason)
		{
			base.ClientDisconnect(cl, reason);

			OnPlayerDisconnected?.Invoke(cl);
		}

		public static void OnChatCommand(Client cl, string command)
		{
			if (!Game.Current.IsAuthority)
			{
				return;
			}

			var cmd = command.Remove(0, 1);
			switch (cmd.ToLower())
			{
				case "respawn":
				case "r":
				case "spawn":
					if (cl.Pawn is Player pl)
					{
						pl.Respawn();
					}
					break;
				case "removebots":
					foreach (var ent in Entity.All)
					{
						if (ent is ReplayBot && ent.IsValid())
						{
							ent.Delete();
						}
					}
					break;
			}
		}

	}
}
