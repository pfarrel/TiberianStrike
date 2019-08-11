using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    abstract class Explosion
    {
        protected const int FrameRepeat = 2;
        protected readonly int ExplosionLength;

        public Point Position { get; private set; }
        public bool IsAlive { get; private set; }
        protected World World { get; }
        protected SpriteWrapper Sprite { get; set; }
        protected int CreatedTicks { get; set; }

        public Explosion(World world, Point position, SpriteWrapper sprite)
        {
            World = world;
            Position = position;
            IsAlive = true;
            Sprite = sprite;
            CreatedTicks = world.Ticks;
            ExplosionLength = sprite.SpriteFrameSet.First().Length * FrameRepeat;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int x = Math.Max(0, Position.X - (Sprite.Width / 2));
            int y = Math.Max(0, Position.Y - (Sprite.Height / 2));

            if (IsAlive)
            {
                SpriteFrame spriteFrame = GetSpriteFrame();
                spriteBatch.Draw(Sprite.SpriteSheet, new Rectangle(x, y, Sprite.Width, Sprite.Height), spriteFrame.Coordinates, Color.White);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (World.Ticks >= CreatedTicks + ExplosionLength)
            {
                IsAlive = false;
            }
        }

        protected virtual SpriteFrame GetSpriteFrame()
        {
            int ticksSinceCreated = World.Ticks - CreatedTicks;
            int frame = ticksSinceCreated / FrameRepeat;
            return Sprite.GetFrameForAnimation(frame);
        }
    }
}
