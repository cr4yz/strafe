using Strafe.Ply;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strafe.UI
{
    public partial class TimerHud : Panel
	{

		public Panel Canvas { get; protected set; }

		private Label _label;

		public TimerHud()
		{
			StyleSheet.Load( "/ui/TimerHud.scss" );

			Canvas = Add.Panel( "timer_hud_canvas" );
			_label = Add.Label(string.Empty, "label");
		}

		public override void Tick()
		{
			base.Tick();

			var player = Player.Local as StrafePlayer;
			if( player == null || player.Controller == null )
			{
				if(HasClass( "open" ) )
				{
					RemoveClass( "open" );
				}
				return;
			}

			if(!HasClass("open"))
			{
				AddClass( "open" );
			}

			var vel = player.Controller.Velocity;
			vel.z = 0;
			var spd = (int)vel.Length;
			string hud;

			if(player.TimerState != TimerState.Running)
            {
				hud = $"{player.TimerState}\n{spd} u/s";
            }
            else
            {
				hud = $"{player.FormattedTimerTime}s\n{spd} u/s\n{player.TimerJumps} jumps\n{player.TimerStrafes} strafes";
			}

			_label.Text = hud;
		}

	}
}
