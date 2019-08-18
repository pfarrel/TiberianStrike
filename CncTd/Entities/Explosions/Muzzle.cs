using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Entities.Explosions
{
    abstract class Muzzle : Explosion
    {
        protected IEntity Source { get; }
        protected float Rotation { get; }
        protected float OffsetDistance { get; }

        public override Point Position
        {
            get
            {
                Vector2 offsetDirection = new Vector2(
                    (float)(Math.Cos(Rotation - MathHelper.PiOver2)),
                    (float)((Math.Sin(Rotation - MathHelper.PiOver2)))
                );
                offsetDirection.Normalize();
                Vector2 offset = OffsetDistance * offsetDirection;
                return Source.Position - new Point((int)offset.X, (int)offset.Y);
            }
        }

        public Muzzle(World world, IEntity source, float rotation, SpriteSheet sprite, float offsetDistance) : base(world, source.Position, sprite, source is A10 ? ExplosionHeight.Air : ExplosionHeight.Ground)
        {
            Source = source;
            Rotation = rotation;
            OffsetDistance = offsetDistance;
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            int ticksSinceCreated = World.Ticks - CreatedTicks;
            return Sprite.GetFrameForAnimationAndRotation("default", Rotation, ticksSinceCreated, 4);
        }
    }
}
