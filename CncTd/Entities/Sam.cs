using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class Sam : BaseEntity
    {
        private const float TimeToBuild = 5000;
        private const float Range = 200;
        private const int TicksToClose = 16 * 14;
        private const int TicksToOpen = 16 * 16;
        private const float TicksBetweenShots = 60;

        private SamState State { get; set; }
        private int CreatedTicks { get; set; }
        private int StateChangedTicks { get; set; }
        private int LastShotTicks { get; set; }
        public override int MaxHealth => 100;
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
                    Point target = a10.Position;
                    Point diff = target - Position;
                    Vector2 diffV = new Vector2(diff.X, diff.Y);
                    diffV.Normalize();
                    Rotation = (float)Math.Atan2(diffV.X, -diffV.Y);
                    if (Rotation == float.NaN)
                    {
                        Rotation = 0;
                    }

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

        protected override SpriteFrame GetSpriteFrame(GameTime gameTime)
        {
            String namePrefix = Health < MaxHealth / 2 ? "damaged-" : "";
            if (State == SamState.Closed)
            {
                return Sprites.Sam.GetFrameForAnimation(namePrefix + "closed-idle", 0);
            }
            else if (State == SamState.Opening)
            {
                int ticksSinceChange = World.Ticks - StateChangedTicks;
                int frame = ticksSinceChange / 16;
                return Sprites.Sam.GetFrameForAnimation(namePrefix + "opening", frame);
            }
            else if (State == SamState.Open)
            {
                return Sprites.Sam.GetFrameForRotation(namePrefix + "idle", Rotation);
            }
            else if (State == SamState.Closing)
            {
                int ticksSinceChange = World.Ticks - StateChangedTicks;
                int frame = ticksSinceChange / 16;
                return Sprites.Sam.GetFrameForAnimation(namePrefix + "closing", frame);
            }
            else
            {
                throw new Exception("what");
            }
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
