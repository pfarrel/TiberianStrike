using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CncTd
{
    class SpriteWrapper
    {
        public Texture2D SpriteSheet { get; }
        public int Width { get; }
        public int Height { get; }
        public int Frames { get; }
        public int SheetWidth { get; }
        public int Stride { get; }
        public List<SpriteFrameSet> SpriteFrameSet { get; }

        private SpriteWrapper(Texture2D spriteSheet, int width, int height, int frames, List<SpriteFrameSet> spriteFrameSet = null)
        {
            SpriteSheet = spriteSheet;
            Width = width;
            Height = height;
            Frames = frames;
            SpriteFrameSet = spriteFrameSet;
            Frames = frames;
            SheetWidth = spriteSheet.Width;
            Stride = height;
            if (spriteFrameSet == null)
            {
                spriteFrameSet = new List<SpriteFrameSet>() { new SpriteFrameSet("default", 0, frames) };
            }
            SpriteFrameSet = spriteFrameSet;
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
        public static SpriteWrapper Complex(Texture2D spriteSheet, int width, int height, List<SpriteFrameSet> spriteFrameSets)
        {
            int totalFrames = spriteFrameSets.Select(s => s.Count).Sum();
            return new SpriteWrapper(spriteSheet, width, height, totalFrames, spriteFrameSets);
        }

        public SpriteFrame GetFrameForRotation(float rotation)
        {
            return GetFrameForRotation("default", rotation);
        }

        public SpriteFrame GetFrameForRotation(string name, float rotation)
        {
            SpriteFrameSet spriteFrameSet = SpriteFrameSet.Where(s => s.Name == name).First();

            double adjustedRotation = rotation < 0 ? rotation + Math.PI * 2 : rotation;
            int spriteNumber = Convert.ToInt32(((adjustedRotation / (Math.PI * 2)) * spriteFrameSet.Count));
            spriteNumber -= 1;
            spriteNumber = (spriteFrameSet.Count - 1) - spriteNumber;
            spriteNumber %= spriteFrameSet.Count;

            if (spriteNumber < 0 || spriteNumber >= spriteFrameSet.Count)
            {
                throw new Exception("Bad sprite index");
            }

            spriteNumber = spriteNumber + spriteFrameSet.Offset;
            if (spriteNumber < 0 || spriteNumber >= Frames)
            {
                throw new Exception("Bad sprite index");
            }

            return GetFrame(spriteNumber);
        }

        public SpriteFrame GetFrameForAnimation(int frame)
        {
            return GetFrameForAnimation("default", frame);
        }

        public SpriteFrame GetFrameForAnimation(string name, int spriteNumber)
        {
            SpriteFrameSet spriteFrameSet = SpriteFrameSet.Where(s => s.Name == name).First();
            if (spriteNumber < 0 || spriteNumber >= spriteFrameSet.Count)
            {
                throw new Exception("Bad sprite index");
            }

            spriteNumber = spriteNumber + spriteFrameSet.Offset;
            if (spriteNumber < 0 || spriteNumber >= Frames)
            {
                throw new Exception("Bad sprite index");
            }

            return GetFrame(spriteNumber);
        }

        private SpriteFrame GetFrame(int frame)
        {
            int framesPerRow = SheetWidth / Width;
            int row = frame / framesPerRow;
            int rowIndex = frame % framesPerRow;

            return new SpriteFrame(SpriteSheet, new Rectangle(rowIndex * Width, row * Height, Width, Height));
        }
    }
}
