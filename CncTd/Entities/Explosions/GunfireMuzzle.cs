using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Entities.Explosions
{
    class GunfireMuzzle : Muzzle
    {
        public GunfireMuzzle(World world, IEntity source, float rotation) : base(world, source, rotation, Sprites.MuzzleFlash, -15.0f)
        {
        }
    }
}
