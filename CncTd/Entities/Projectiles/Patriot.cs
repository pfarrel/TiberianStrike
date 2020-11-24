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
    class Patriot : AbstractMissile
    {
        protected override Type ExplosionType => typeof(ExplosionBig);

        protected override SpriteSheet Sprite => Sprites.Patriot;

        protected override Warhead Warhead => Warhead.Explosive;

        protected override float MovementSpeed => 250f / 60;

        protected override int Damage => 30;

        protected override int ExplosionRadius => 15;

        protected override float TurnRate => 0.01f;

        public Patriot(World world, Player player, Point position, IEntity targetEntity) : base(world, player, position, targetEntity)
        {
        }
    }
}
