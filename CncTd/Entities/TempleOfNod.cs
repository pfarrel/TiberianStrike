using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    class TempleOfNod : Building
    {
        protected override SpriteSheet SpriteSheet => Sprites.TempleOfNod;

        public TempleOfNod(World world, Player player, Point position) : base(world, player, position)
        {
        }
    }
}
