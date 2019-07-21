using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class ShellExplosion : Explosion
    {
        public ShellExplosion(Point position, Sprites sprites) : base(position, sprites.CannonImpact)
        {
        }
    }
}
