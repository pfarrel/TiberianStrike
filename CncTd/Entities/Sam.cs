using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    class Sam : BaseEntity
    {
        private const int FrameRepeat = 6;
        private const float TimeToBuild = 5000;
        private const float Range = 200;
        private readonly int TicksToClose = 14 * FrameRepeat;
        private readonly int TicksToOpen = 16 * FrameRepeat;
        private const float TicksBetweenShots = 180;

        private SamState State { get; set; }
        private int CreatedTicks { get; set; }
        private int StateChangedTicks { get; set; }
        private int LastShotTicks { get; set; }
        public override int MaxHealth => 10;
        private float Rotation { get; set; }
        protected override int HealthBarOffset => -5;

        public Sam(World world, Player player, Point position) : base(world, player, position)
        {
            State = SamState.Closed;
            CreatedTicks = World.Ticks;
        }

        public override void Update(GameTime gameTime)
        {
            A10 a10 = World.GetEntities<A10>().First();
            float a10Distance = Vector2.Distance(a10.PositionVector, PositionVector);

            if (State == SamState.Closed)
            {
                if (a10Distance < Range)
                {
                    State = SamState.Opening;
                    StateChangedTicks = World.Ticks;
                }
            }
            else if (State == SamState.Opening)
            {
                if (World.Ticks - StateChangedTicks >= TicksToOpen)
                {
                    State = SamState.Open;
                    StateChangedTicks = World.Ticks;
                }
            }
            else if (State == SamState.Open)
            {
                if (a10Distance < Range)
                {
                    Rotation = VectorHelpers.GetRotationToFace(a10.PositionVector, PositionVector) ?? Rotation;

                    if (World.Ticks - LastShotTicks > TicksBetweenShots)
                    {
                        World.AddProjectile(new Patriot(World, Player.Two, Position, a10.Position));
                        World.AddExplosion(new SamMuzzle(World, this, Rotation));
                        Sounds.Rocket1.Play();
                        LastShotTicks = World.Ticks;
                    }
                }
                else
                {
                    State = SamState.Closing;
                    StateChangedTicks = World.Ticks;
                }
            }
            else if (State == SamState.Closing)
            {
                if (World.Ticks - StateChangedTicks >= TicksToClose)
                {
                    State = SamState.Closed;
                    StateChangedTicks = World.Ticks;
                }
            }
            else
            {
                throw new Exception("what");
            }


            base.Update(gameTime);
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            String namePrefix = Health < MaxHealth / 2 ? "damaged-" : "";
            int ticksSinceChange = World.Ticks - StateChangedTicks;
            string name;

            if (State == SamState.Closed)
            {
                name = namePrefix + "closed-idle";
            }
            else if (State == SamState.Opening)
            {
                name = namePrefix + "opening";
            }
            else if (State == SamState.Open)
            {
                name = namePrefix + "idle";
            }
            else if (State == SamState.Closing)
            {
                name = namePrefix + "closing";
            }
            else
            {
                throw new Exception("what");
            }

            return Sprites.Sam.GetFrameForAnimationAndRotation(name, Rotation, ticksSinceChange, FrameRepeat);
        }

        enum SamState
        {
            Closed,
            Opening,
            Open,
            Closing
        }
    }
}
