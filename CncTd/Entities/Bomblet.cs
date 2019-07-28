using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class Bomblet : Projectile
    {
        public Bomblet(World world, Player player, Point position, Point target) : base(world, player, position, target, Sprites.Bomblet, 20f)
        {
        }
    }
}
