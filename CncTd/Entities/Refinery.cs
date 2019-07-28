using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class Refinery : DrawableGameComponent, IPlayerEntity
    {
        private const float TimeToBuild = 5000;

        public Player Player { get; }
        public Point Position { get; private set; }
        private TimeSpan TimeWhenCreated { get; set; }

        public Refinery(Game game, Player player, Point position, TimeSpan elapsedWhenCreated) : base(game)
        {
            Player = player;
            Position = position;
            TimeWhenCreated = elapsedWhenCreated;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int x = Math.Max(0, Position.X - 36);
            int y = Math.Max(0, Position.Y - 36);

            int spriteNumber;
            if (gameTime.TotalGameTime.TotalMilliseconds > TimeWhenCreated.TotalMilliseconds + TimeToBuild)
            {
                spriteNumber = Sprites.Refinery.Frames - 1;
            } else
            {
                double fraction = (gameTime.TotalGameTime.TotalMilliseconds - TimeWhenCreated.TotalMilliseconds) / TimeToBuild;
                spriteNumber = (int) (fraction * (Sprites.Refinery.Frames - 1));
            }

            if (spriteNumber >= Sprites.Refinery.Frames || spriteNumber < 0)
            {
                throw new Exception("Bad sprite index");
            }

            spriteBatch.Draw(
                Sprites.Refinery.SpriteSheet,
                new Rectangle(x, y, Sprites.Refinery.Width, Sprites.Refinery.Height),
                new Rectangle(Sprites.Refinery.Width * spriteNumber, 0, Sprites.Refinery.Width, Sprites.Refinery.Height),
                Color.White
            );
        }

        public void Update(GameTime gameTime, List<IPlayerEntity> playerEntities)
        {
        }

        public void Damage(int amount) { }
    }
}
