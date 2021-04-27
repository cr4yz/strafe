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
    public partial class WrHud : Panel
	{

		public Panel Canvas { get; protected set; }

		private Label _label;

		public WrHud()
		{
			StyleSheet.Load( "/ui/WrHud.scss" );

			Canvas = Add.Panel( "wr_hud_canvas" );
			_label = Add.Label(string.Empty, "label");
		}

		public override void Tick()
		{
			base.Tick();

			_label.Text = "WR: Crayz (00:06.426s)\nYour rank: #1 (00:06.426s)";
		}

	}
}
