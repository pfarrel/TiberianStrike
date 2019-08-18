using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    class Obelisk : Building
    {
        protected override SpriteSheet SpriteSheet => Sprites.Obelisk;

        public Obelisk(World world, Player player, Point position) : base(world, player, position)
        {
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            if (Health < MaxHealth / 2)
            {
                return SpriteSheet.GetFrameForAnimation("damaged-default", 0);
            }
            else
            {

            }
            return SpriteSheet.GetFrameForAnimation("default", 0);
        }
    }
}
