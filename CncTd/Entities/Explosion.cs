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
        private const int FrameRepeat = 2;

        public Point Position { get; private set; }
        public bool IsAlive { get; private set; }
        private int FrameNumber { get; set; }
        private SpriteWrapper Sprite { get; set; }

        public Explosion(Point position, SpriteWrapper sprite)
        {
            Position = position;
            IsAlive = true;
            FrameNumber = -1;
            Sprite = sprite;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int x = Math.Max(0, Position.X - (Sprite.Width / 2));
            int y = Math.Max(0, Position.Y - (Sprite.Height / 2));

            if (IsAlive)
            {
                spriteBatch.Draw(Sprite.SpriteSheet, new Rectangle(x, y, Sprite.Width, Sprite.Height), new Rectangle((FrameNumber / FrameRepeat) * Sprite.Width, 0, Sprite.Width, Sprite.Height), Color.White);
            }
        }

        public void Update(GameTime gameTime, List<IPlayerEntity> playerEntities)
        {
            FrameNumber++;
            if (FrameNumber == Sprite.Frames * FrameRepeat)
            {
                IsAlive = false;
            }
        }
    }
}
