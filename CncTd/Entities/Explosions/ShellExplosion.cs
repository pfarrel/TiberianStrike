using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    class ShellExplosion : Explosion
    {
        public ShellExplosion(World world, Point position) : base(world, position, Sprites.ExplosionSmall)
        {
        }
    }
}
