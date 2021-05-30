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
    public partial class StrafeHud : HudEntity<RootPanel>
    {
        public StrafeHud()
        {
			if ( !IsClient )
			{
				return;
			}

            RootPanel.StyleSheet.Load("/ui/StrafeHud.scss");
            RootPanel.AddChild<StrafeChatBox>();
            RootPanel.AddChild<TimerHud>();
            RootPanel.AddChild<WrHud>();
            RootPanel.AddChild<VoiceList>();
			RootPanel.AddChild<StrafeScoreboard<StrafeScoreboardEntry>>();
            RootPanel.AddChild<NameTags>().MaxDrawDistance = 800;
		}
    }
}
