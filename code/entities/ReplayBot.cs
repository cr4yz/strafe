using Sandbox;
using Strafe.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strafe.Entities
{
    public class ReplayBot : AnimEntity
    {

        public Replay Replay;

        [Event("server.tick")]
        private void Tick()
        {
            if (Replay == null)
            {
                return;
            }
            Replay.Mode = ReplayMode.Playback;
            Replay.Tick();
            Position = Replay.CurrentFrame.Position;
            WorldAng = Replay.CurrentFrame.Angles;

            DoWalk(Replay.CurrentFrame);
            SetAnimBool("b_jump", Replay.CurrentFrame.JustJumped);
			SetAnimBool("b_grounded", Replay.CurrentFrame.Grounded);
			SetAnimBool("b_ducked", Replay.CurrentFrame.Buttons.HasFlag(InputButton.Duck));
        }

        public override void Spawn()
        {
            base.Spawn();

            SetModel("models/citizen/citizen.vmdl");

            RenderColorAndAlpha = new Color32(255, 255, 255, 85);

            var spawn = Entity.All.FirstOrDefault(x => x.EntityName == "spawn");
            if(spawn != null)
            {
                Position = spawn.Position;
                WorldAng = spawn.WorldAng;
            }
        }

        public static ReplayBot WithClonedReplay(Replay replay)
        {
            if(!Game.Current.IsServer)
            {
                throw new Exception("Trying to create replay bot on client");
            }
            var result = new ReplayBot();
            var clonedReplay = replay.Clone(result);
            clonedReplay.Mode = ReplayMode.Playback;
            result.Replay = clonedReplay;
            return result;
        }

        void DoWalk(ReplayFrame frame)
        {
            SetAnimFloat("walkspeed_scale", 2.0f / 260.0f);
			SetAnimFloat("runspeed_scale", 2.0f / 260.0f);
			SetAnimFloat("duckspeed_scale", 2.0f / 80.0f);

            var rot = Rotation.From(frame.Angles);
            var moveDir = frame.Velocity.Normal * 100.0f;
            var forward = rot.Forward.Dot(moveDir);
            var sideward = rot.Left.Dot(moveDir);
            var speedScale = Scale;

			SetAnimFloat("forward", forward);
			SetAnimFloat("sideward", sideward);
			SetAnimFloat("wishspeed", speedScale > 0.0f ? frame.Velocity.Length / speedScale : 0.0f);
        }

    }

}
