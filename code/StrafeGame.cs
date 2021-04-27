using Sandbox;
using Strafe.UI;
using Strafe.Ply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strafe
{
    [Library("strafe", Title = "Strafe Speedruns")]
    partial class StrafeGame : Game
    {
        public StrafeGame()
        {
            Log.Info( "Strafe Speedruns Started" );

            if (IsServer)
			{
				new StrafeHUD();
			}
        }

		public override Player CreatePlayer() => new StrafePlayer();

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
			}
		}

    }
}
