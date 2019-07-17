using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class ShellExplosion
    {
        private const int Width = 19;
        private const int Height = 13;
        private const int Frames = 14;

        public Point Position { get; private set; }
        public bool IsAlive { get; private set; }
        private int FrameNumber { get; set; }

        public ShellExplosion(Point position)
        {
            Position = position;
            IsAlive = true;
            FrameNumber = -1;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D sprite)
        {
            int x = Math.Max(0, Position.X - (Width / 2));
            int y = Math.Max(0, Position.Y - (Height / 2));

            if (IsAlive)
            {
                spriteBatch.Draw(sprite, new Rectangle(x, y, Width, Height), new Rectangle(FrameNumber * Width, 0, Width, Height), Color.White);
            }
        }

        public void Update(GameTime gameTime, List<IPlayerEntity> playerEntities)
        {
            FrameNumber++;
            if (FrameNumber == Frames)
            {
                IsAlive = false;
            }
        }
    }
}
