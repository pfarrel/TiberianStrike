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
        private const int TicksToBuild = 8 * 20;

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
                return Sprites.Refinery.GetFrameForAnimation(World.Ticks - CreatedTicks);
            }
            else
            {
                return Sprites.RefineryConstructing.GetFrameForAnimation(World.Ticks - CreatedTicks);
            }
        }
    }
}
