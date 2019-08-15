using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    class Refinery : BaseEntity
    {
        private const int TicksToBuild = 16 * 20;

        private int CreatedTicks { get; set; }

        public override int MaxHealth => 100;

        public Refinery(World world, Player player, Point position) : base(world, player, position)
        {
            CreatedTicks = world.Ticks;
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            if (World.Ticks >= CreatedTicks + TicksToBuild)
            {
                int spriteNumber = (World.Ticks / 16) % Sprites.Refinery.Frames;
                return Sprites.Refinery.GetFrameForAnimation(spriteNumber);
            }
            else
            {
                int ticksSinceChange = World.Ticks - CreatedTicks;
                int frame = ticksSinceChange / 16;
                return Sprites.RefineryConstructing.GetFrameForAnimation(frame);
            }
        }
    }
}
