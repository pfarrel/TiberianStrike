using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private const int Frames = 32;
        private const double MovementSpeed = 20.0d;  // per second
        private const double FiringTime = 0.5d;
        private const double BombingTime = 0.2d;
        private const float GunRange = 100f;
        private float _rotation;

        public Player Player { get; }
        public Point Position
        {
            get
            {
                return new Point((int)RealPosition.X, (int)RealPosition.Y);
            }
        }

        private Vector2 RealPosition { get; set; }
        private float Rotation
        {
            get
            {
                return _rotation;
                //return (float) (Math.Round(_rotation / (Math.PI * 2 / 32)) * (Math.PI * 2 / 32));
            }
            set
            {
                _rotation = value;
            }
        }
        private TimeSpan LastBombingTime { get; set; }
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
            int y = Math.Max(0, Position.Y - 24 - 30);

            double adjustedRotation = Rotation < 0 ? Rotation + Math.PI * 2 : Rotation;
            int spriteNumber = (int) Math.Round(((adjustedRotation / (Math.PI * 2)) * Frames));
            spriteNumber -= 1;
            spriteNumber = (Frames - 1) - spriteNumber;
            spriteNumber %= Frames;

            //Console.WriteLine("Rotation: {0}, AdjustedRotation: {1}, SpriteNumber: {2}", Rotation, adjustedRotation, spriteNumber);
            if (spriteNumber >= 32 || spriteNumber < 0)
            {
                throw new Exception("Bad sprite index");
            }

            spriteBatch.Draw(Sprites.Shadow.SpriteSheet, new Rectangle(x, y + 30, Sprites.Shadow.Width, Sprites.Shadow.Height), Color.White);
            spriteBatch.Draw(sprite, new Rectangle(x, y, 48, 48), new Rectangle(48 * spriteNumber, 0, 48, 48), Color.White);
        }

        public void Update(GameTime gameTime, List<IPlayerEntity> playerEntities)
        {
            Vector2 movement = new Vector2(
                (float)(Math.Cos(Rotation - MathHelper.PiOver2)),
                (float)((Math.Sin(Rotation - MathHelper.PiOver2)))
            );
            RealPosition += movement;
        }

        public void TurnLeft()
        {
            _rotation -= 0.05f;
            _rotation %= MathHelper.TwoPi;
        }

        public void TurnRight()
        {
            _rotation += 0.05f;
            _rotation %= MathHelper.TwoPi;
        }

        public void Damage(int amount)
        {
            
        }

        public void Shoot(GameTime gameTime, List<Projectile> projectiles)
        {
            if (gameTime.TotalGameTime < LastFiringTime + TimeSpan.FromSeconds(FiringTime))
            {
                return;
            }

            LastFiringTime = gameTime.TotalGameTime;

            Vector2 bulletDirection = new Vector2(
                (float) (Math.Cos(Rotation - MathHelper.PiOver2)),
                (float) ((Math.Sin(Rotation - MathHelper.PiOver2)))
            );
            bulletDirection.Normalize();
            bulletDirection = GunRange * bulletDirection;
            Point bulletImpact = Position + new Point((int) bulletDirection.X, (int) bulletDirection.Y);
            Bullet bullet = new Bullet(Game, Player.One, Position, bulletImpact);
            projectiles.Add(bullet);
            Sounds.HeavyMachineGun.Play(0.5f, 0, 0);
        }

        public void Bomb(Game game, GameTime gameTime, List<Projectile> projectiles)
        {
            if (gameTime.TotalGameTime < LastBombingTime + TimeSpan.FromSeconds(BombingTime))
            {
                return;
            }

            LastBombingTime = gameTime.TotalGameTime;

            Point target = Position;
            Point start = Position;
            start.Y -= 30;

            projectiles.Add(new Bomblet(game, Player, start, target));
        }
    }
}
