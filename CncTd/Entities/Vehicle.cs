using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TiberianStrike.Entities.Explosions;

namespace TiberianStrike.Entities
{
    abstract class Vehicle : BaseEntity
    {
        private const double MovementSpeed = 20.0d;  // per second

        protected float Rotation { get; set; }

        public override int MaxHealth => 10;

        protected override Type ExplosionType => typeof(ExplosionBig);

        protected override SoundEffect ExplosionSound => Sounds.HarvesterExplosion;

        protected abstract SpriteSheet SpriteSheet { get; }

        public Vehicle(World world, Player player, Point position) : base(world, player, position)
        {
            Rotation = 0;
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            return SpriteSheet.GetFrameForRotation(Rotation);
        }
    }
}
