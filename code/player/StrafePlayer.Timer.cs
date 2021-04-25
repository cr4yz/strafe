using Sandbox;
using Strafe.Weapons;
using System;

namespace Strafe.Ply
{
	public partial class StrafePlayer
	{

		[Net]
		public int Jumps { get; set; }
		[Net]
		public int Strafes { get; set; }

		public RealTimeSince Time { get; set; } = 0;

		protected void TickTimer()
		{
			if(!Game.Current.IsAuthority)
			{
				return;
			}

			var walk = Controller as StrafeWalkController;
			if(walk.JustJumped)
			{
				Jumps++;
			}
		}

	}
}


