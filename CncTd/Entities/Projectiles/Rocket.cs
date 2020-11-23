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
    class Rocket : Projectile
    {
        protected override Type ExplosionType => typeof(ShellExplosion);
        protected override SoundEffect ExplosionSound => Sounds.Explosion;

        public Rocket(World world, Player player, Point position, Point target) : base(world, player, position, target, Sprites.Dragon, 200f / 60, false)
        {
        }
    }
}