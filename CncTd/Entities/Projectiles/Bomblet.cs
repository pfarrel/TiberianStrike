﻿using Microsoft.Xna.Framework;
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
    class Bomblet : Projectile
    {
        public Bomblet(World world, Player player, Point position, Point target) : base(world, player, position, target, null)
        {
        }

        protected override Type ExplosionType => typeof(NapalmExplosion);

        protected override SoundEffect ExplosionSound => Sounds.FireExplosion;

        protected override float MovementSpeed { get { return 20f / 60; } }

        protected override SpriteSheet Sprite { get { return Sprites.Bomblet; } }

        protected override Warhead Warhead => Warhead.Fire;
    }
}
