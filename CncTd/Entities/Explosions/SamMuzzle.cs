using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class SamMuzzle : Explosion
    {
        protected float Rotation { get; }

        public SamMuzzle(World world, Point position, float rotation) : base(world, position, Sprites.SamFire)
        {
            Rotation = rotation;
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            int ticksSinceCreated = World.Ticks - CreatedTicks;
            return Sprite.GetFrameForAnimationAndRotation("default", Rotation, ticksSinceCreated, 4);
        }
    }
}
