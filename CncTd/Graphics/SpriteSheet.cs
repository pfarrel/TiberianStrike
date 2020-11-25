using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TiberianStrike.Entities;

namespace TiberianStrike
{
    public class SpriteSheet
    {
        public Texture2D Texture { get; }
        public int Width { get; }
        public int Height { get; }
        public int Frames { get; }
        public int SheetWidth { get; }
        public int Stride { get; }
        public SpriteSequence[] SpriteSequences { get; }

        private SpriteSheet(Texture2D spriteSheet, int width, int height, params SpriteSequence[] spriteSequences)
        {
            Texture = spriteSheet;
            Width = width;
            Height = height;
            SheetWidth = spriteSheet.Width;
            Stride = height;
            SpriteSequences = spriteSequences;
            Frames = spriteSequences.Select(s => s.Start + s.Length * s.Facings).Max();
        }

        public static SpriteSheet Animation(Texture2D texture, int width, int height, int frames)
        {
            return new SpriteSheet(texture, width, height, new SpriteSequence("default", 0) { Length = frames });
        }

        public static SpriteSheet Building(Texture2D texture, int width, int height)
        {
            return new SpriteSheet(texture, width, height, new SpriteSequence("default", 0), new SpriteSequence("damaged", 1), new SpriteSequence("destroyed", 2));
        }

        public static SpriteSheet Building(Texture2D texture, int width, int height, int defaultState = 0, int damaged = 1, int destroyed = 2)
        {
            return new SpriteSheet(texture, width, height, new SpriteSequence("default", defaultState), new SpriteSequence("damaged", damaged), new SpriteSequence("dead", destroyed));
        }

        public static SpriteSheet Static(Texture2D texture, int width, int height)
        {
            return new SpriteSheet(texture, width, height, new SpriteSequence("default", 0));
        }

        public static SpriteSheet Unit(Texture2D texture, int width, int height, int directions)
        {
            return new SpriteSheet(texture, width, height, new SpriteSequence("default", 0) { Facings = directions });
        }

        public static SpriteSheet Wall(Texture2D texture, int width, int height, bool hasHeavyDamage, bool hasDead)
        {
            return new SpriteSheet(texture, width, height, new SpriteSequence("default", 0) { Length = 16 }, new SpriteSequence("damaged", 16) { Length = 16 }, new SpriteSequence("heavily-damaged", hasHeavyDamage ? 32 : 16) { Length = 16 }, new SpriteSequence("dead", hasDead ? 48 : 16) { Length = 16 });
        }

        public static SpriteSheet Complex(Texture2D texture, int width, int height, params SpriteSequence[] spriteSequences)
        {
            return new SpriteSheet(texture, width, height, spriteSequences);
        }

        public SpriteFrame GetFrameForRotation(float rotation)
        {
            return GetFrameForRotation("default", rotation);
        }

        public SpriteFrame GetFrameForRotation(string name, float rotation)
        {
            SpriteSequence spriteFrameSet = SpriteSequences.Where(s => s.Name == name).First();
            int facing = GetFacing(spriteFrameSet, rotation);
            int frame = spriteFrameSet.Start + (facing * spriteFrameSet.Length);
            return GetFrame(frame);
        }

        public SpriteFrame GetFrameForAnimation(int ticks, int frameRepeat = 8)
        {
            return GetFrameForAnimation("default", ticks, frameRepeat);
        }

        public SpriteFrame GetFrameForAnimation(string name, int ticks, int frameRepeat = 8)
        {
            return GetFrameForAnimationAndRotation(name, 0, ticks, frameRepeat);
        }

        public SpriteFrame GetFrameForAnimationAndRotation(float rotation, int ticks, int frameRepeat = 8)
        {
            return GetFrameForAnimationAndRotation("default", rotation, ticks, frameRepeat);
        }

        public SpriteFrame GetFrameForAnimationAndRotation(string name, float rotation, int ticks, int frameRepeat = 8)
        {
            SpriteSequence spriteFrameSet = SpriteSequences.Where(s => s.Name == name).First();
            int facing = GetFacing(spriteFrameSet, rotation);
            int startOfAnimation = spriteFrameSet.Start + (facing * spriteFrameSet.Length);
            int animationOffset = (ticks / frameRepeat) % spriteFrameSet.Length;
            int frame = startOfAnimation + animationOffset;
            return GetFrame(frame);
        }

        private int GetFacing(SpriteSequence spriteFrameSet, float rotation)
        {
            return GetFacing(spriteFrameSet.Facings, rotation);
        }

        // Visible for testing
        public static int GetFacing(int facings, float rotation)
        {
            // Sprite sheet is reversed rotation
            rotation *= -1;
            // Sprite sheets start at 270' rather than 0'
            rotation -= MathHelper.PiOver2;
            // Normalize to positive range of rotation
            while (rotation < 0)
            {
                rotation += MathHelper.TwoPi;
            }
            rotation %= MathHelper.TwoPi;

            int spriteNumber = Convert.ToInt32(((rotation / (Math.PI * 2)) * facings));
            spriteNumber %= facings;

            if (spriteNumber < 0 || spriteNumber >= facings)
            {
                throw new Exception(String.Format("Calculated facing: {0} greater than number of facings in SpriteFrameSet: {1}", spriteNumber, facings));
            }

            return spriteNumber;
        }

        public SpriteFrame GetFrameForWall(string name, WallNeighbours wallNeighbours)
        {
            SpriteSequence spriteFrameSet = SpriteSequences.Where(s => s.Name == name).First();
            int frame = (int)wallNeighbours;
            return GetFrame(spriteFrameSet.Start + frame);
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

            return new SpriteFrame(Texture, new Rectangle(rowIndex * Width, row * Height, Width, Height));
        }
    }
}
