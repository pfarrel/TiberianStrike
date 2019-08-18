using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiberianStrike.Entities.Explosions;

namespace TiberianStrike.Entities
{
    class Bomblet : Projectile
    {
        public Bomblet(World world, Player player, Point position, Point target) : base(world, player, position, target, Sprites.Bomblet, 20f)
        {
        }

        protected override Type ExplosionType => typeof(NapalmExplosion);

        protected override SoundEffect ExplosionSound => Sounds.FireExplosion;
    }
}
