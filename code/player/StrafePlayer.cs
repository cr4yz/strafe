using Sandbox;
using Strafe.Timer;

namespace Strafe.Ply
{
    public partial class StrafePlayer : Player
    {

        private Replay _replay;
        //private StrafeWalkController _controller;
        private TimeSince _timeSinceLastFootstep = 0;

        public bool SuppressPickupNotices { get; private set; }

        [ClientVar("cl_yawspeed", Saved = true)]
        public static int YawSpeed { get; set; } = 160;
        
        public StrafePlayer()
        {
            Inventory = new StrafeInventory(this);
            _replay = new Replay(this);
            _replay.Mode = ReplayMode.Record;
		}

		public override void BuildInput(InputBuilder input)
		{
			base.BuildInput(input);

			if (input.Down(InputButton.Alt1))
			{
				input.ViewAngles = input.ViewAngles.WithYaw(input.ViewAngles.yaw + YawSpeed * Time.Delta);
			}

			if (input.Down(InputButton.Alt2))
			{
				input.ViewAngles = input.ViewAngles.WithYaw(input.ViewAngles.yaw - YawSpeed * Time.Delta);
			}
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

        public override void OnAnimEventFootstep(Vector3 pos, int foot, float volume)
        {
            if (LifeState != LifeState.Alive
                || !IsClient
                || _timeSinceLastFootstep < 0.1f)
            {
                return;
            }

            _timeSinceLastFootstep = 0;

            PlayFootstep();
        }

        public void PlayFootstep(int foot = 0, float volume = 1)
        {
            using (Prediction.Off())
            {
                var trBegin = new Transform(Position.WithZ(Position.z + 5), Rotation.Identity);
                var trEnd = new Transform(Position.WithZ(Position.z - 10), Rotation.Identity);
                var tr = Trace.Sweep(PhysicsBody, trBegin, trEnd)
                    .Ignore(this)
                    .Run();

                if (tr.Hit)
                {
                    tr.Surface.DoFootstep(this, tr, foot, volume);
                }
            }
        }

    }
}
