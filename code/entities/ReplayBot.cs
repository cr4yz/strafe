using Sandbox;
using Strafe.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strafe.Entities
{
    public class ReplayBot : ModelEntity
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
        }

        public override void Spawn()
        {
            base.Spawn();

            SetModel("models/citizen/clothes/ghost.vmdl");

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

    }

}
