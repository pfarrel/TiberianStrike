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
    class Bullet : Projectile
    {
        public Bullet(World world, Player player, Point position, Point target) : base(world, player, position, target)
        {
        }

        protected override Type ExplosionType => typeof(BulletImpact);

        protected override Warhead Warhead => Warhead.Bullet;

        protected override SpriteSheet Sprite => Sprites.None;

        protected override SoundEffect ExplosionSound => Sounds.Void;

        protected override float MovementSpeed => 2000f / 60;
    }
}
