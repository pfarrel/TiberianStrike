using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiberianStrike.Entities.Explosions;

namespace TiberianStrike.Entities.Projectiles
{
    class CannonShot : Projectile
    {
        public CannonShot(World world, Player player, Point position, Point target) : base(world, player, position, target, Sprites.CannonShot120mm, 250f / 60)
        {
        }

        protected override Type ExplosionType => typeof(ShellExplosion);
    }
}
