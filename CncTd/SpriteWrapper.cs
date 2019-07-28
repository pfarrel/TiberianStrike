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

        private SpriteWrapper(Texture2D spriteSheet, int width, int height, int frames)
        {
            SpriteSheet = spriteSheet;
            Width = width;
            Height = height;
            Frames = frames;
        }

        public static SpriteWrapper Animation(Texture2D spriteSheet, int width, int height, int frames)
        {
            return new SpriteWrapper(spriteSheet, width, height, frames);
        }
        public static SpriteWrapper Static(Texture2D spriteSheet, int width, int height)
        {
            return new SpriteWrapper(spriteSheet, width, height, 1);
        }
        public static SpriteWrapper Unit(Texture2D spriteSheet, int width, int height, int directions)
        {
            return new SpriteWrapper(spriteSheet, width, height, directions);
        }
    }
}
