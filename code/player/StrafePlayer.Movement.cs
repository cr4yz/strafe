using Sandbox;
using Strafe.Entities;
using Strafe.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strafe.Ply
{
	public partial class StrafePlayer
	{

		[Net]
		public float AirAcceleration
        {
			get => (Controller as StrafeWalkController).AirAcceleration;
			set => (Controller as StrafeWalkController).AirAcceleration = value;
		}

		[Net]
		public bool AutoBhop
		{
			get => (Controller as StrafeWalkController).AutoJump;
			set => (Controller as StrafeWalkController).AutoJump = value;
		}

	}
}


