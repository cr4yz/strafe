using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;


namespace Strafe.UI
{
	public partial class StrafeChatEntry : Panel
	{
		public Label NameLabel { get; internal set; }
		public Label Message { get; internal set; }
		public Image Avatar { get; internal set; }

		public RealTimeSince TimeSinceBorn = 0;

		public bool Faded => TimeSinceBorn > 12;

		public StrafeChatEntry()
		{
			Avatar = Add.Image();
			NameLabel = Add.Label( "Name", "name" );
			Message = Add.Label( "Message", "message" );
		}
	}
}
