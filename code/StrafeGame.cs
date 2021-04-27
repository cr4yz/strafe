using Sandbox;
using Strafe.UI;
using Strafe.Ply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strafe.Entities;

namespace Strafe
{
    [Library("strafe", Title = "Strafe")]
    partial class StrafeGame : Game
    {

		public event Action<StrafePlayer> OnPlayerJoined;
		public event Action<StrafePlayer> OnPlayerDisconnected;

		public StrafeGame()
        {
            if (IsServer)
			{
				new StrafeHUD();
			}
        }

		public override Player CreatePlayer() => new StrafePlayer();

        public override void PlayerJoined(Player player)
        {
            base.PlayerJoined(player);

			OnPlayerJoined?.Invoke(player as StrafePlayer);
        }

        public override void PlayerDisconnected(Player player, NetworkDisconnectionReason reason)
        {
            base.PlayerDisconnected(player, reason);

			OnPlayerDisconnected?.Invoke(player as StrafePlayer);
		}

        public static void OnChatCommand(Player player, string command)
		{
			if ( !Game.Current.IsAuthority )
			{
				return;
			}

			var cmd = command.Remove( 0, 1 );
			switch ( cmd.ToLower() )
			{
				case "respawn":
				case "r":
				case "spawn":
					player.Respawn();
					break;
				case "rall":
					foreach(var p in Player.All )
					{
						p.Respawn();
					}
					break;
				case "removebots":
					foreach(var ent in Entity.All)
                    {
						if(ent is ReplayBot && ent.IsValid())
                        {
							ent.Delete();
                        }
                    }
					break;
			}
		}

    }
}
