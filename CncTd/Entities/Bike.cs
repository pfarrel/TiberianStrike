using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TiberianStrike.Entities
{
    class Apc : Vehicle
    {
        protected override SpriteSheet SpriteSheet => Sprites.Apc;

        public override int MaxHealth => 10;

        public Apc(World world, Player player, Point position) : base(world, player, position)
        {
        }
    }
}
