using Sandbox;
using Strafe.Timer;

namespace Strafe.Ply
{
    public partial class StrafePlayer : BasePlayer
    {

        public bool SuppressPickupNotices { get; private set; }

        private Replay _replay;
        private StrafeWalkController _controller;

        public StrafePlayer()
        {
            Inventory = new StrafeInventory(this);
            _replay = new Replay(this);
            _replay.Mode = ReplayMode.Record;
        }

        public override void Respawn()
        {
            SetModel("models/citizen/citizen.vmdl"); // If you have your own model, you can place it here instead.
            Animator = new StandardPlayerAnimator();
            Camera = new StrafeFirstPersonCamera();
            Controller = new StrafeWalkController();
            EnableAllCollisions = true;
            EnableDrawing = true;
            EnableHideInFirstPerson = true;
            EnableShadowInFirstPerson = true;

            AirAcceleration = 1000f;
            AutoBhop = true;

            //Inventory.Add( new Smg(), true );
            //GiveAmmo( AmmoType.Pistol, 900 );

            base.Respawn();

            Dress();
            RemoveCollisionLayer(CollisionLayer.Solid);
        }

        public override void Spawn()
        {
            base.Spawn();

            StrafeFirstPersonCamera.Target = this;
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

    }
}
