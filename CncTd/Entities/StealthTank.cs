using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TiberianStrike.Entities
{
    class StealthTank : Vehicle
    {
        protected override SpriteSheet SpriteSheet => Sprites.StealthTank;

        public override int MaxHealth => 10;

        public StealthTank(World world, Player player, Point position) : base(world, player, position)
        {
        }
    }
}
