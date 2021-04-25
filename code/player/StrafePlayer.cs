using Sandbox;
using Strafe.Timer;
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
		public StrafeTimer Timer { get; private set; }

        public StrafePlayer()
        {
			Inventory = new StrafeInventory( this );
			Timer = new StrafeTimer( this );
		}

        public override void Respawn()
        {
            SetModel("models/citizen/citizen.vmdl"); // If you have your own model, you can place it here instead.
            Controller = new StrafeWalkController();
            Animator = new StandardPlayerAnimator();
            Camera = new StrafeFirstPersonCamera();
            //EnableAllCollisions = true;
            EnableDrawing = true;
            EnableHideInFirstPerson = true;
            EnableShadowInFirstPerson = true;

			ClearCollisionLayers();
			RemoveCollisionLayer( CollisionLayer.Player );
			CollisionGroup = CollisionGroup.ConditionallySolid;

			var strafeController = Controller as StrafeWalkController;
			strafeController.AutoJump = true;
			strafeController.AirAcceleration = 1500;
			strafeController.AirControl = 30;

			Dress();

			//Inventory.Add( new Smg(), true );
			//GiveAmmo( AmmoType.Pistol, 900 );

			base.Respawn();
		}

		protected override void Tick()
		{
			base.Tick();

			TickTimer();
		}

	}
}
