using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class SmallMissile : Projectile
    {
        public SmallMissile(World world, Player player, Point position, Point target) : base(world, player, position, target, Sprites.Dragon, 100f)
        {
        }

        protected override Type ExplosionType => typeof(ShellExplosion);
    }
}
