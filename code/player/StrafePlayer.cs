using Sandbox;
using Strafe.Timer;
using Strafe.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Strafe.Ply
{
    public partial class StrafePlayer : BasePlayer
    {

        public bool SuppressPickupNotices { get; private set; }
        public StrafeTimer Timer { get; private set; }

        private float _timeSinceLastFootstep;

        public StrafePlayer()
        {
            Inventory = new StrafeInventory(this);
            Timer = new StrafeTimer(this);
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

            Dress();

            var strafeController = Controller as StrafeWalkController;
            strafeController.AutoJump = true;
            strafeController.AirAcceleration = 1000;
            strafeController.Acceleration = 5;
            strafeController.GroundFriction = 4;
            strafeController.AirControl = 30;
            strafeController.StopSpeed = 75;
            strafeController.DefaultSpeed = 273.0f;

            //Inventory.Add( new Smg(), true );
            //GiveAmmo( AmmoType.Pistol, 900 );

            base.Respawn();

            RemoveCollisionLayer(CollisionLayer.Solid);
        }

        public void PlayFootstep()
        {
            using (Prediction.Off())
            {
                var tr = Trace.Ray(WorldPos, WorldPos + Vector3.Down * 20)
                    .Radius(1)
                    .Ignore(this)
                    .Run();

                if (!tr.Hit) return;

                tr.Surface.DoFootstep(this, tr, 0, 1);
            }
        }

        protected override void Tick()
        {
            base.Tick();

            TickTimer();
        }

    }
}
