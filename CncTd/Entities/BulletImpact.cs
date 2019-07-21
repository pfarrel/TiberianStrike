using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class BulletImpact : Explosion
    {
        public BulletImpact(Point position, Sprites sprites) : base(position, sprites.BulletImpact)
        {
        }
    }
}
