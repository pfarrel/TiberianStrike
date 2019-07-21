using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class A10 : DrawableGameComponent, IPlayerEntity
    {
        private const int Sprites = 32;
        private const double MovementSpeed = 20.0d;  // per second
        private const double FiringTime = 1.0d;
        private const float GunRange = 100f;

        public Player Player { get; }
        public Point Position
        {
            get
            {
                return new Point((int)RealPosition.X, (int)RealPosition.Y);
            }
        }
        public bool IsFiring { get; private set; }

        private Vector2 RealPosition { get; set; }
        private double Rotation { get; set; }
        private TimeSpan LastFiringTime { get; set; }

        public A10(Game game, Player player, Point position) : base(game)
        {
            Player = player;
            RealPosition = new Vector2(position.X, position.Y);
            Rotation = MathHelper.ToRadians(90);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D sprite, Texture2D whitePixelSprite)
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
        }

        public void Update(GameTime gameTime, List<IPlayerEntity> playerEntities)
        {
            Vector2 movement = new Vector2(
                (float)(Math.Cos(Rotation - MathHelper.PiOver2)),
                (float)((Math.Sin(Rotation - MathHelper.PiOver2)))
            );
            RealPosition += movement;
            if (IsFiring)
            {
                if (gameTime.TotalGameTime > LastFiringTime + TimeSpan.FromSeconds(FiringTime))
                {
                    IsFiring = false;
                }
            }
        }

        public void TurnLeft()
        {
            Rotation -= 0.1f;
            Rotation %= MathHelper.TwoPi;
        }

        public void TurnRight()
        {
            Rotation += 0.1f;
            Rotation %= MathHelper.TwoPi;
        }

        public void Damage(int amount)
        {
            
        }

        public void Shoot(GameTime gameTime, List<Explosion> explosions, Sprites sprites)
        {
            IsFiring = true;
            LastFiringTime = gameTime.TotalGameTime;

            Vector2 bulletDirection = new Vector2(
                (float) (Math.Cos(Rotation - MathHelper.PiOver2)),
                (float) ((Math.Sin(Rotation - MathHelper.PiOver2)))
            );
            bulletDirection.Normalize();
            bulletDirection = GunRange * bulletDirection;
            Point bulletImpact = Position + new Point((int) bulletDirection.X, (int) bulletDirection.Y);
            explosions.Add(new BulletImpact(bulletImpact, sprites));
        }
    }
}
