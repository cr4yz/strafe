using Sandbox;

namespace Strafe.Ply
{
	public class StrafeFirstPersonCamera : BaseCamera
	{

		public static Entity Target;

		public override void Activated()
		{
			MoveToViewer();
		}

		public override void Update()
		{
			MoveToViewer();

			FieldOfView = 80;
		}

		private void MoveToViewer()
        {
			Viewer = Player.Local;

			if (Player.Local == null)
            {
				return;
            }

			if (Player.Local is Player player)
            {
				
				Pos = player.EyePos;
				Rot = player.EyeRot;
			}
			else
            {
				Pos = Target.WorldPos;
				Rot = Target.WorldRot;
			}
		}

	}
}
