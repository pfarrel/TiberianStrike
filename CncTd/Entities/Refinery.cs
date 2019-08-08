using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class Refinery : BaseEntity
    {
        private const float TimeToBuild = 5000;

        private TimeSpan TimeWhenCreated { get; set; }

        public override int MaxHealth => 100;

        public Refinery(World world, Player player, Point position, TimeSpan elapsedWhenCreated) : base(world, player, position)
        {
            TimeWhenCreated = elapsedWhenCreated;
        }

        protected override SpriteFrame GetSpriteFrame(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > TimeWhenCreated.TotalMilliseconds + TimeToBuild)
            {
                int spriteNumber = (World.Ticks / 16) % Sprites.Refinery.Frames;
                return Sprites.Refinery.GetFrameForAnimation(spriteNumber);
            }
            else
            {
                double fraction = (gameTime.TotalGameTime.TotalMilliseconds - TimeWhenCreated.TotalMilliseconds) / TimeToBuild;
                int spriteNumber = (int)(fraction * (Sprites.RefineryConstructing.Frames - 1));
                return Sprites.RefineryConstructing.GetFrameForAnimation(spriteNumber);
            }
        }
    }
}
