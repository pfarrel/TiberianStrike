using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Entities.Explosions
{
    class BulletImpact : Explosion
    {
        public BulletImpact(World world, Point position, ExplosionHeight explosionHeight) : base(world, position, Sprites.BulletImpact, explosionHeight)
        {
        }
    }
}
