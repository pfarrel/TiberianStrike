using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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

        public SpriteFrame GetFrameForRotation(float rotation)
        {
            double adjustedRotation = rotation < 0 ? rotation + Math.PI * 2 : rotation;
            int spriteNumber = Convert.ToInt32(((adjustedRotation / (Math.PI * 2)) * Frames));
            spriteNumber -= 1;
            spriteNumber = (Frames - 1) - spriteNumber;
            spriteNumber %= Frames;

            if (spriteNumber >= Frames || spriteNumber < 0)
            {
                throw new Exception("Bad sprite index");
            }

            return new SpriteFrame(SpriteSheet, new Rectangle(spriteNumber * Width, 0, Width, Height));
        }

        public SpriteFrame GetFrameForAnimation(int frame)
        {
            if (frame >= Frames || frame < 0)
            {
                throw new Exception("Bad sprite index");
            }

            return new SpriteFrame(SpriteSheet, new Rectangle(frame * Width, 0, Width, Height));
        }
    }
}
