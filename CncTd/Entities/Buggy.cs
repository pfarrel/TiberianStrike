using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TiberianStrike.Entities
{
    class Buggy : Vehicle
    {
        protected override SpriteSheet SpriteSheet => Sprites.Buggy;

        public override int MaxHealth => 10;

        public Buggy(World world, Player player, Point position) : base(world, player, position)
        {
        }
    }
}
