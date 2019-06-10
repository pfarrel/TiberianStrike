using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class Refinery : DrawableGameComponent
    {
        private const int Sprites = 20;
        private const float TimeToBuild = 5000;
        private const int Size = 72;

        private Point Position { get; set; }
        private TimeSpan TimeWhenCreated { get; set; }

        public Refinery(Game game, Point position, TimeSpan elapsedWhenCreated) : base(game)
        {
            Position = position;
            TimeWhenCreated = elapsedWhenCreated;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D sprite)
        {
            int x = Math.Max(0, Position.X - 36);
            int y = Math.Max(0, Position.Y - 36);

            int spriteNumber;
            if (gameTime.TotalGameTime.TotalMilliseconds > TimeWhenCreated.TotalMilliseconds + TimeToBuild)
            {
                spriteNumber = Sprites - 1;
            } else
            {
                double fraction = (gameTime.TotalGameTime.TotalMilliseconds - TimeWhenCreated.TotalMilliseconds) / TimeToBuild;
                spriteNumber = (int) (fraction * (Sprites - 1));
            }

            if (spriteNumber >= Sprites || spriteNumber < 0)
            {
                throw new Exception("Bad sprite index");
            }

            spriteBatch.Draw(sprite, new Rectangle(x, y, Size, Size), new Rectangle(Size * spriteNumber, 0, Size, Size), Color.White);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
