using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class Harvester : DrawableGameComponent
    {
        private const int Sprites = 32;

        private Point position;
        double Rotation { get; set; }

        public Harvester(Game game, Point position) : base(game)
        {
            this.position = position;
            this.Rotation = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D sprite)
        {
            int x = Math.Max(0, position.X - 24);
            int y = Math.Max(0, position.Y - 24);

            int spriteNumber = (int) (Rotation * Sprites);

            spriteBatch.Draw(sprite, new Rectangle(x, y, 48, 48), new Rectangle(48 * spriteNumber, 0, 48, 48), Color.White);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Rotation += gameTime.ElapsedGameTime.TotalMilliseconds / 3000;
            Rotation = Rotation % 1;
            base.Update(gameTime);
        }
    }
}
