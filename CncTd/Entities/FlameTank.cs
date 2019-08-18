using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TiberianStrike.Entities
{
    class FlameTank : Vehicle
    {
        protected override SpriteSheet SpriteSheet => Sprites.FlameTank;

        public override int MaxHealth => 10;

        public FlameTank(World world, Player player, Point position) : base(world, player, position)
        {
        }
    }
}
