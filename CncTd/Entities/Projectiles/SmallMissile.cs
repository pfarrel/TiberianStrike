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
    class SmallMissile : AbstractMissile
    {
        protected override Type ExplosionType => typeof(ShellExplosion);
        
        protected override SoundEffect ExplosionSound => Sounds.Explosion;

        protected override SpriteSheet Sprite => Sprites.Dragon;

        protected override Warhead Warhead => Warhead.Explosive;

        protected override float MovementSpeed => 20.0f / 60;

        public SmallMissile(World world, Player player, Point position, IEntity targetEntity) : base(world, player, position, targetEntity)
        {
        }
    }
}
