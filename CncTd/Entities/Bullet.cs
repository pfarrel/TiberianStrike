using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class Bullet : Projectile
    {
        public Bullet(World world, Player player, Point position, Point target) : base(world, player, position, target, Sprites.None, 500f)
        {
        }
    }
}
