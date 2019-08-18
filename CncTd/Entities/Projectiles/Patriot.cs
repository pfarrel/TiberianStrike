﻿using Microsoft.Xna.Framework;
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
    class Patriot : Projectile
    {
        public Patriot(World world, Player player, Point position, Point target) : base(world, player, position, target, Sprites.Patriot, 200f, true)
        {
        }

        protected override Type ExplosionType => typeof(ShellExplosion);
    }
}
