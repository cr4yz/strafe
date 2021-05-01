using Sandbox;
using Strafe.Timer;

namespace Strafe.Ply
{
    public partial class StrafePlayer : BasePlayer
    {

        public bool SuppressPickupNotices { get; private set; }

        private Replay _replay;
        private StrafeWalkController _controller;
        private TimeSince _timeSinceLastFootstep = 0;

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

        public override void OnAnimEventFootstep(Vector3 pos, int foot, float volume)
        {
            if (LifeState != LifeState.Alive)
                return;

            if (!IsClient)
                return;

            if (_timeSinceLastFootstep < 0.1f)
                return;

            _timeSinceLastFootstep = 0;

            //DebugOverlay.Box( 1, pos, -1, 1, Color.Red );
            //DebugOverlay.Text( pos, $"{volume}", Color.White, 5 );

            var trBegin = new Transform(WorldPos.WithZ(WorldPos.z + 5), Rotation.Identity);
            var trEnd = new Transform(WorldPos.WithZ(WorldPos.z - 10), Rotation.Identity);
            var tr = Trace.Sweep(PhysicsBody, trBegin, trEnd)
                .Ignore(this)
                .Run();

            if (!tr.Hit) return;

            tr.Surface.DoFootstep(this, tr, foot, volume);
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
