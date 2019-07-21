using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd
{
    class SpriteWrapper
    {
        public Texture2D SpriteSheet { get; }
        public int Width { get; }
        public int Height { get; }
        public int Frames { get; }

        public SpriteWrapper(Texture2D spriteSheet, int width, int height, int frames)
        {
            SpriteSheet = spriteSheet;
            Width = width;
            Height = height;
            Frames = frames;
        }
    }
}
