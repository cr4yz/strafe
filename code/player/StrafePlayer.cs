using Sandbox;
using Strafe.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Strafe.Ply
{
	public partial class StrafePlayer : BasePlayer
    {

		public bool SuppressPickupNotices { get; private set; }

        public StrafePlayer()
        {
            Log.Info("RP Player");
			Inventory = new StrafeInventory( this );
        }

        public override void Respawn()
        {
            SetModel("models/citizen/citizen.vmdl"); // If you have your own model, you can place it here instead.
            Controller = new StrafeWalkController();
            Animator = new StandardPlayerAnimator();
            Camera = new StrafeFirstPersonCamera();
            EnableAllCollisions = true;
            EnableDrawing = true;
            EnableHideInFirstPerson = true;
            EnableShadowInFirstPerson = true;

			//Inventory.Add( new Smg(), true );
			GiveAmmo( AmmoType.Pistol, 900 );

			base.Respawn();
		}

		public override void BuildInput( ClientInput input )
		{
			base.BuildInput( input );

			//input.ViewAngles = Angles.Zero;
		}

		public static void SpawnPlayer( Player player )
		{
			player?.Respawn();
		}

	}
}
