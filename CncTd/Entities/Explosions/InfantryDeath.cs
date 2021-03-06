﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Entities.Explosions
{
    class InfantryDeath : Explosion
    {
        protected override int FrameRepeat => 4;

        public InfantryDeath(World world, Point position, SpriteSheet sprite, string spriteSequenceName) : base(world, position, sprite, ExplosionHeight.Ground, spriteSequenceName)
        {
        }
    }
}
