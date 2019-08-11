using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class SamMuzzle : Muzzle
    {
        public SamMuzzle(World world, IEntity source, float rotation) : base(world, source, rotation, Sprites.SamFire, 4f)
        {
        }
    }
}
