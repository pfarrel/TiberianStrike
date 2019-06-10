using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class Turret : DrawableGameComponent
    {
        private const int Sprites = 32;
        private const int ConstructionSprites = 20;
        private const double RotationSpeed = (Math.PI * 2) / 3;  // per second
        private const int Size = 24;
        private const float TimeToBuild = 5000;

        public Point Position{ get; }
        public Point Target { get; set; }

        private double Rotation { get; set; }
        private Boolean Constructing { get; set; }
        private TimeSpan TimeWhenCreated { get; }

        public Turret(Game game, Point position, TimeSpan timeWhenCreated) : base(game)
        {
            Constructing = true;
            Position = position;
            Rotation = 0;
            Target = new Point(0, 0);
            TimeWhenCreated = timeWhenCreated;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D constructionSprite, Texture2D mainSprite)
        {
            int x = Math.Max(0, Position.X - Size / 2);
            int y = Math.Max(0, Position.Y - Size / 2);

            Texture2D spriteToUse;
            int spriteNumber;
            if (Constructing)
            {
                spriteToUse = constructionSprite;

                double fraction = (gameTime.TotalGameTime.TotalMilliseconds - TimeWhenCreated.TotalMilliseconds) / TimeToBuild;
                spriteNumber = (int)(fraction * (ConstructionSprites - 1));
            }
            else
            {
                spriteToUse = mainSprite;

                double adjustedRotation = Rotation < 0 ? Rotation + Math.PI * 2 : Rotation;
                spriteNumber = (int)((adjustedRotation / (Math.PI * 2)) * Sprites);
                spriteNumber -= 1;
                spriteNumber = (Sprites - 1) - spriteNumber;
                spriteNumber %= Sprites;

                Console.WriteLine("Rotation: {0}, AdjustedRotation: {1}, SpriteNumber: {2}", Rotation, adjustedRotation, spriteNumber);
                if (spriteNumber >= Sprites || spriteNumber < 0)
                {
                    throw new Exception("Bad sprite index");
                }
            }

            spriteBatch.Draw(spriteToUse, new Rectangle(x, y, Size, Size), new Rectangle(Size * spriteNumber, 0, Size, Size), Color.White);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > TimeWhenCreated.TotalMilliseconds + TimeToBuild)
            {
                Constructing = false;
            }

            if (!Constructing)
            {
                if (Position != Target)
                {
                    Point diff = Target - Position;
                    Vector2 diffV = new Vector2(diff.X, diff.Y);
                    diffV.Normalize();
                    float targetRotation = (float)Math.Atan2(diffV.X, -diffV.Y);
                    //Console.WriteLine("Source: {0}, Target: {1}, TargetVector: {2} TargetRotation: {3}", Position, Target, diffV, targetRotation);
                    //if (Rotation == targetRotation)
                    //{
                    //    RealPosition += Vector2.Multiply(diffV, (float)(MovementSpeed * (gameTime.ElapsedGameTime.TotalSeconds)));
                    //}
                    Rotation = targetRotation;
                }
            }

            base.Update(gameTime);
        }
    }
}
