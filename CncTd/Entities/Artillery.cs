using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TiberianStrike.Entities
{
    class Artillery : Vehicle
    {
        protected override SpriteSheet SpriteSheet => Sprites.Artillery;

        public override int MaxHealth => 10;

        public Artillery(World world, Player player, Point position) : base(world, player, position)
        {
        }
    }
}
