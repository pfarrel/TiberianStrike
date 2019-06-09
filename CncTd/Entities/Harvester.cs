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
        private const double RotationSpeed = (Math.PI * 2) / 3;

        private Point Position { get; set; }
        private Point Target { get; set; }

        double Rotation { get; set; }

        public Harvester(Game game, Point position) : base(game)
        {
            Position = position;
            Target = new Point(0, 0);
            Rotation = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D sprite)
        {
            int x = Math.Max(0, Position.X - 24);
            int y = Math.Max(0, Position.Y - 24);

            double adjustedRotation = Rotation < 0 ? Rotation + Math.PI * 2 : Rotation;
            int spriteNumber = (Sprites- 1) - (int) ((adjustedRotation / (Math.PI * 2)) * Sprites);

            Console.WriteLine("Rotation: {0}, AdjustedRotation: {1}, SpriteNumber: {2}", Rotation, adjustedRotation, spriteNumber);
            if (spriteNumber >= 32 || spriteNumber < 0)
            {
                throw new Exception("Bad sprit index");
            }

            spriteBatch.Draw(sprite, new Rectangle(x, y, 48, 48), new Rectangle(48 * spriteNumber, 0, 48, 48), Color.White);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (Position != Target)
            {
                Point diff = Target - Position;
                Vector2 diffV = new Vector2(diff.X, diff.Y);
                float targetRotation = (float)Math.Atan2(diffV.X, -diffV.Y);
                Console.WriteLine("Source: {0}, Target: {1}, TargetVector: {2} TargetRotation: {3}", Position, Target, diffV, targetRotation);
                Rotation = targetRotation;
            }

            base.Update(gameTime);
        }

        public void setTarget(Point target)
        {
            Target = target;
        }
    }
}
