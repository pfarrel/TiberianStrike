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
        protected override Type ExplosionType => typeof(ShellExplosion);

        protected override SpriteSheet Sprite => Sprites.CannonShot120mm;

        protected override SoundEffect ExplosionSound => Sounds.Explosion;

        protected override Warhead Warhead => Warhead.Explosive;

        protected override float MovementSpeed => 250f / 60;

        public CannonShot(World world, Player player, Point position, Point target) : base(world, player, position, target)
        {
        }
    }
}
