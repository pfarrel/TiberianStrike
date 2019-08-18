using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Entities.Explosions
{
    class ShellExplosion : Explosion
    {
        public ShellExplosion(World world, Point position, ExplosionHeight height) : base(world, position, Sprites.ExplosionSmall, height)
        {
        }
    }
}
