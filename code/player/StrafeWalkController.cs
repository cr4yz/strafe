using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strafe.Ply
{
    class StrafeWalkController : WalkController
	{

		public StrafeWalkController()
			: base()
		{
			AirAcceleration = 1500;
			AutoJump = true;
		}

		public override void Accelerate( Vector3 wishdir, float wishspeed, float speedLimit, float acceleration )
		{
			// This gets overridden because some games (CSPort) want to allow dead (observer) players
			// to be able to move around.
			// if ( !CanAccelerate() )
			//     return;

			if ( speedLimit > 0 && wishspeed > speedLimit )
				wishspeed = speedLimit;

			// See if we are changing direction a bit
			var currentspeed = Velocity.Dot( wishdir );

			// Reduce wishspeed by the amount of veer.
			var addspeed = wishspeed - currentspeed;

			// If not going to add any speed, done.
			if ( addspeed <= 0 )
				return;

			// Determine amount of acceleration.
			var accelspeed = (GroundEntity != null ? Acceleration : AirAcceleration) * Time.Delta * wishspeed * SurfaceFriction;

			// Cap at addspeed
			if ( accelspeed > addspeed )
				accelspeed = addspeed;

			Velocity += wishdir * accelspeed;
		}

	}
}
