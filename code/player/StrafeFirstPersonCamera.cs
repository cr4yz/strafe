using Sandbox;
using Sandbox.Rcon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strafe.Ply
{
	public class StrafeFirstPersonCamera : BaseCamera
	{
		public override void Activated()
		{
			if ( Player.Local == null )
			{
				return;
			}

			Pos = Player.Local.EyePos;
			Rot = Player.Local.EyeRot;
		}

		public override void Update()
		{
			if ( Player.Local == null )
			{
				return;
			}

			Pos = Player.Local.EyePos;
			Rot = Player.Local.EyeRot;

			FieldOfView = 80;

			Viewer = Player.Local;
		}
	}
}
