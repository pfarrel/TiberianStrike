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
        public SpriteFrameSet[] SpriteFrameSet { get; }

        private SpriteWrapper(Texture2D spriteSheet, int width, int height, params SpriteFrameSet[] spriteFrameSet)
        {
            SpriteSheet = spriteSheet;
            Width = width;
            Height = height;
            SheetWidth = spriteSheet.Width;
            Stride = height;
            SpriteFrameSet = spriteFrameSet;
            Frames = spriteFrameSet.Select(s => s.Length * s.Facings).Sum();
        }

        public static SpriteWrapper Animation(Texture2D spriteSheet, int width, int height, int frames)
        {
            return new SpriteWrapper(spriteSheet, width, height, new SpriteFrameSet("default", 0) { Length = frames });
        }

        public static SpriteWrapper Static(Texture2D spriteSheet, int width, int height)
        {
            return new SpriteWrapper(spriteSheet, width, height, new SpriteFrameSet("default", 0));
        }

        public static SpriteWrapper Unit(Texture2D spriteSheet, int width, int height, int directions)
        {
            return new SpriteWrapper(spriteSheet, width, height, new SpriteFrameSet("default", 0) { Facings = directions });
        }

        public static SpriteWrapper Complex(Texture2D spriteSheet, int width, int height, params SpriteFrameSet[] spriteFrameSets)
        {
            return new SpriteWrapper(spriteSheet, width, height, spriteFrameSets);
        }

        public SpriteFrame GetFrameForRotation(float rotation)
        {
            return GetFrameForRotation("default", rotation);
        }

        public SpriteFrame GetFrameForRotation(string name, float rotation)
        {
            SpriteFrameSet spriteFrameSet = SpriteFrameSet.Where(s => s.Name == name).First();
            int facing = getFacing(spriteFrameSet, rotation);
            int frame = spriteFrameSet.Start + (facing * spriteFrameSet.Length);
            return GetFrame(frame);
        }

        public SpriteFrame GetFrameForAnimation(int frame)
        {
            return GetFrameForAnimation("default", frame);
        }

        public SpriteFrame GetFrameForAnimation(string name, int spriteNumber)
        {
            SpriteFrameSet spriteFrameSet = SpriteFrameSet.Where(s => s.Name == name).First();
            if (spriteNumber < 0 || spriteNumber >= spriteFrameSet.Length)
            {
                throw new Exception("Bad sprite index");
            }

            spriteNumber = spriteNumber + spriteFrameSet.Start;
            return GetFrame(spriteNumber);
        }

        public SpriteFrame GetFrameForAnimationAndRotation(string name, float rotation, int ticks, int frameRepeat = 8)
        {
            SpriteFrameSet spriteFrameSet = SpriteFrameSet.Where(s => s.Name == name).First();
            int facing = getFacing(spriteFrameSet, rotation);
            int startOfAnimation = spriteFrameSet.Start + (facing * spriteFrameSet.Length);
            int animationOffset = (ticks / frameRepeat) % spriteFrameSet.Length;
            int frame = startOfAnimation + animationOffset;
            return GetFrame(frame);
        }

        private int getFacing(SpriteFrameSet spriteFrameSet, float rotation)
        {
            double adjustedRotation = rotation < 0 ? rotation + Math.PI * 2 : rotation;
            int spriteNumber = Convert.ToInt32(((adjustedRotation / (Math.PI * 2)) * spriteFrameSet.Facings));
            spriteNumber -= 1;
            spriteNumber = (spriteFrameSet.Facings - 1) - spriteNumber;
            spriteNumber %= spriteFrameSet.Facings;

            if (spriteNumber < 0 || spriteNumber >= spriteFrameSet.Facings)
            {
                throw new Exception("Calculated facing greater than number of facings in SpriteFrameSet");
            }

            return spriteNumber;
        }


        private SpriteFrame GetFrame(int frame)
        {
            if (frame > Frames)
            {
                throw new Exception("Bad sprite index");
            }

            int framesPerRow = SheetWidth / Math.Max(Width, 1);
            int row = frame / framesPerRow;
            int rowIndex = frame % framesPerRow;

            return new SpriteFrame(SpriteSheet, new Rectangle(rowIndex * Width, row * Height, Width, Height));
        }
    }
}
