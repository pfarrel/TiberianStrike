﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class NapalmExplosion : Explosion
    {
        public NapalmExplosion(Point position) : base(position, Sprites.Napalm2)
        {
        }
    }
}