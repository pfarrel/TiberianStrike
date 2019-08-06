﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd
{
    static class Fonts
    {
        public static SpriteFont Font { get; private set; }

        public static void Load(ContentManager content)
        {
            Font = content.Load<SpriteFont>("Font");
        }
    }
}
