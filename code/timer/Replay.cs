using Sandbox;
using Strafe.Ply;
using System;
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

        public ReplayFrame CurrentFrame { get; private set; } = default;

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

            CurrentFrame = _frames[_playbackIndex];
            _playbackIndex++;
        }

        private void TickRecord()
        {
            var frame = new ReplayFrame()
            {
                Position = Entity.WorldPos,
                Angles = Entity.WorldAng,
                Velocity = Entity.Velocity
            };
            if(Entity is StrafePlayer player)
            {
                frame.Buttons = GetButtons(player);
                frame.JustJumped = (player.Controller as StrafeWalkController).JustJumped;
                frame.Grounded = player.Controller.GroundEntity != null;
            }
            _frames.Add(frame);
        }

        private static Array _inputBtns;
        private InputButton GetButtons(Player player)
        {
            var result = InputButton.Alt1; // InputButton.None maybe

            if(_inputBtns == null)
            {
                _inputBtns = Enum.GetValues(typeof(InputButton));
            }

            foreach(InputButton btn in _inputBtns)
            {
                if(player.Input.Down(btn))
                {
                    result |= btn;
                }
                else
                {
                    result &= ~btn;
                }
            }

            return result;
        }

    }

    public struct ReplayFrame
    {
        public Vector3 Position;
        public Angles Angles;
        public Vector3 Velocity;
        public InputButton Buttons;
        public bool JustJumped;
        public bool Grounded;
    }

}