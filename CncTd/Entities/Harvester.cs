using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class Harvester : DrawableGameComponent, IPlayerEntity
    {
        private const int Sprites = 32;
        private const double RotationSpeed = (Math.PI * 2) / 3;  // per second
        private const double MovementSpeed = 50.0d;  // per second

        public Player Player { get; }
        public Point Position
        {
            get
            {
                return new Point((int)RealPosition.X, (int)RealPosition.Y);
            }
        }
        public Point Target { get; set; }

        private Vector2 RealPosition { get; set; }
        private double Rotation { get; set; }

        public Harvester(Game game, Player player, Point position, Point target) : base(game)
        {
            Player = player;
            RealPosition = new Vector2(position.X, position.Y);
            Target = target;
            Rotation = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D sprite)
        {
            int x = Math.Max(0, Position.X - 24);
            int y = Math.Max(0, Position.Y - 24);

            double adjustedRotation = Rotation < 0 ? Rotation + Math.PI * 2 : Rotation;
            int spriteNumber = (int) ((adjustedRotation / (Math.PI * 2)) * Sprites);
            spriteNumber -= 1;
            spriteNumber = (Sprites - 1) - spriteNumber;
            spriteNumber %= Sprites;

            //Console.WriteLine("Rotation: {0}, AdjustedRotation: {1}, SpriteNumber: {2}", Rotation, adjustedRotation, spriteNumber);
            if (spriteNumber >= 32 || spriteNumber < 0)
            {
                throw new Exception("Bad sprite index");
            }

            spriteBatch.Draw(sprite, new Rectangle(x, y, 48, 48), new Rectangle(48 * spriteNumber, 0, 48, 48), Color.White);

            base.Draw(gameTime);
        }

        public void Update(GameTime gameTime, List<IPlayerEntity> playerEntities)
        {
            if (Position != Target)
            {
                Point diff = Target - Position;
                Vector2 diffV = new Vector2(diff.X, diff.Y);
                diffV.Normalize();
                float targetRotation = (float)Math.Atan2(diffV.X, -diffV.Y);
                //Console.WriteLine("Source: {0}, Target: {1}, TargetVector: {2} TargetRotation: {3}", Position, Target, diffV, targetRotation);
                if (Rotation == targetRotation)
                {
                    RealPosition += Vector2.Multiply(diffV, (float)(MovementSpeed * (gameTime.ElapsedGameTime.TotalSeconds)));
                }
                Rotation = targetRotation;
            }

            base.Update(gameTime);
        }
    }
}
