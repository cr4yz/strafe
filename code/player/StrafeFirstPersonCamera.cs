using Sandbox;

namespace Strafe.Ply
{
	public class StrafeFirstPersonCamera : BaseCamera
	{

		public static Entity Target;

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
