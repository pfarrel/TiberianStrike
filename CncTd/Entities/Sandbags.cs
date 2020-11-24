using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    class Sandbags : AbstractWall
    {
        public override int MaxHealth => 20;

        protected override SpriteSheet SpriteSheet => Sprites.Sandbags;

        public Sandbags(World world, Player player, Point position) : base(world, player, position)
        {
        }
    }
}
