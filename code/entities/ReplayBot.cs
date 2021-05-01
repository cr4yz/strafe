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
            WorldPos = Replay.CurrentFrame.Position;
            WorldAng = Replay.CurrentFrame.Angles;

            DoWalk(Replay.CurrentFrame);
            SetAnimParam("b_jump", Replay.CurrentFrame.JustJumped);
            SetAnimParam("b_grounded", Replay.CurrentFrame.Grounded);
            SetAnimParam("b_ducked", Replay.CurrentFrame.Buttons.HasFlag(InputButton.Duck));
        }

        public override void Spawn()
        {
            base.Spawn();

            SetModel("models/citizen/citizen.vmdl");

            RenderColorAndAlpha = new Color32(255, 255, 255, 85);

            var spawn = Entity.All.FirstOrDefault(x => x.EntityName == "spawn");
            if(spawn != null)
            {
                WorldPos = spawn.WorldPos;
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
            SetAnimParam("walkspeed_scale", 2.0f / 260.0f);
            SetAnimParam("runspeed_scale", 2.0f / 260.0f);
            SetAnimParam("duckspeed_scale", 2.0f / 80.0f);

            var rot = Rotation.From(frame.Angles);
            var moveDir = frame.Velocity.Normal * 100.0f;
            var forward = rot.Forward.Dot(moveDir);
            var sideward = rot.Left.Dot(moveDir);
            var speedScale = WorldScale;

            SetAnimParam("forward", forward);
            SetAnimParam("sideward", sideward);
            SetAnimParam("wishspeed", speedScale > 0.0f ? frame.Velocity.Length / speedScale : 0.0f);
        }

    }

}
