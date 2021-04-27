using Sandbox;
using System.Collections.Generic;

namespace Strafe.Timer
{
    
    public enum ReplayMode
    {
        Record,
        Playback
    }

    public class Replay
    {

        public readonly Entity Entity;
        public ReplayMode Mode;

        private int _playbackIndex;
        private List<ReplayFrame> _frames = new List<ReplayFrame>();
        private RealTimeSince _pause = 0;

        public Replay(Entity entity)
        {
            Entity = entity;
        }

        public Replay Clone(Entity entity)
        {
            var result = new Replay(entity);
            result._frames.AddRange(_frames);
            return result;
        }

        public void Clear() 
        {
            _frames.Clear();
            _playbackIndex = 0;
        }

        public void Tick()
        {
            if (!Entity.IsValid())
            {
                return;
            }

            switch (Mode)
            {
                case ReplayMode.Playback:
                    TickPlayback();
                    break;
                case ReplayMode.Record:
                    TickRecord();
                    break;
            }
        }

        private void TickPlayback()
        {
            if(_frames.Count == 0
                || _pause <= 3)
            {
                return;
            }

            if (_playbackIndex >= _frames.Count)
            {
                _playbackIndex = 0;
                _pause = 0;
                return;
            }

            var frame = _frames[_playbackIndex];
            Entity.WorldPos = frame.Position;
            Entity.WorldAng = frame.Angles;
            _playbackIndex++;
        }

        private void TickRecord()
        {
            var frame = new ReplayFrame()
            {
                Position = Entity.WorldPos,
                Angles = Entity.WorldAng
            };
            _frames.Add(frame);
        }

    }

    public struct ReplayFrame
    {
        public Vector3 Position;
        public Angles Angles;
    }

}