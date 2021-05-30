using Sandbox;

namespace Strafe.Ply
{
	public class StrafeFirstPersonCamera : Camera
	{

		public override void Activated()
		{
			MoveToTarget();
		}

		public override void Update()
		{
			MoveToTarget();

			FieldOfView = 80;
		}

		private void MoveToTarget()
        {
			Viewer = Local.Pawn;

			if (Viewer == null)
            {
				return;
            }

			if (Viewer is Player player)
            {
				Pos = player.EyePos;
				Rot = player.EyeRot;
			}
		}

	}
}
