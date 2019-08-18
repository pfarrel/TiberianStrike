using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    abstract class Explosion
    {
        protected virtual int FrameRepeat => 3;
        protected readonly int ExplosionLength;

        public virtual Point Position { get; private set; }
        public bool IsAlive { get; private set; }
        protected World World { get; }
        protected SpriteSheet Sprite { get; set; }
        public string SpriteSequenceName { get; }
        protected int CreatedTicks { get; set; }

        public Explosion(World world, Point position, SpriteSheet sprite, string name = "default")
        {
            World = world;
            Position = position;
            IsAlive = true;
            Sprite = sprite;
            SpriteSequenceName = name;
            CreatedTicks = world.Ticks;
            ExplosionLength = sprite.SpriteSequences.Where(s => s.Name == name).First().Length * FrameRepeat;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int x = Math.Max(0, Position.X - (Sprite.Width / 2));
            int y = Math.Max(0, Position.Y - (Sprite.Height / 2));

            if (IsAlive)
            {
                SpriteFrame spriteFrame = GetSpriteFrame();
                spriteBatch.Draw(Sprite.Texture, new Rectangle(x, y, Sprite.Width, Sprite.Height), spriteFrame.Coordinates, Color.White);
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
            return Sprite.GetFrameForAnimationAndRotation(SpriteSequenceName, 0, ticksSinceCreated, FrameRepeat);
        }
    }
}
