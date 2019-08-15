using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    class BulletImpact : Explosion
    {
        public BulletImpact(World world, Point position) : base(world, position, Sprites.BulletImpact)
        {
        }
    }
}
