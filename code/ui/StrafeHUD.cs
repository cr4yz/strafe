using Sandbox;
using Sandbox.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strafe.UI
{
    [Library]
    public partial class StrafeHUD : Hud
    {
        public StrafeHUD()
        {
            if (!IsClient) return;

            RootPanel.StyleSheet.Load("/ui/hud.scss");
            RootPanel.AddChild<StrafeChatBox>();
			RootPanel.AddChild<VoiceList>();
			RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
            RootPanel.AddChild<NameTags>().MaxDrawDistance = 800;
		}
    }
}
