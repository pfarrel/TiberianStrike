using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    abstract class Building : BaseEntity
    {
        private const int TicksToBuild = 8 * 20;

        private int CreatedTicks { get; set; }

        public override int MaxHealth => 20;

        protected abstract SpriteSheet SpriteSheet { get; }

        public Building(World world, Player player, Point position) : base(world, player, position)
        {
            CreatedTicks = world.Ticks;
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            if (Health < MaxHealth / 2)
            {
                return SpriteSheet.GetFrameForAnimation("damaged", 0);
            }
            else
            {

            }
            return SpriteSheet.GetFrameForAnimation("default", 0);
        }
    }
}
