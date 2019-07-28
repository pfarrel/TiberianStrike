using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class A10 : BaseEntity
    {
        private const int MaxHealthConst = 100;
        private const int FlyingHeight = 30;
        private const double MovementSpeed = 20.0d;  // per second
        private const double FiringTime = 0.5d;
        private const double BombingTime = 0.2d;
        private const float GunRange = 100f;
        private float _rotation;

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

        public A10(World world, Player player, Point position) : base(world, player, position, MaxHealthConst)
        {
            Rotation = MathHelper.ToRadians(90);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int x = Math.Max(0, Position.X - Sprites.A10.Width / 2);
            int y = Math.Max(0, Position.Y - Sprites.A10.Height / 2 - FlyingHeight);

            double adjustedRotation = Rotation < 0 ? Rotation + Math.PI * 2 : Rotation;
            int spriteNumber = (int) Math.Round(((adjustedRotation / (Math.PI * 2)) * Sprites.A10.Frames));
            spriteNumber -= 1;
            spriteNumber = (Sprites.A10.Frames - 1) - spriteNumber;
            spriteNumber %= Sprites.A10.Frames;

            //Console.WriteLine("Rotation: {0}, AdjustedRotation: {1}, SpriteNumber: {2}", Rotation, adjustedRotation, spriteNumber);
            if (spriteNumber >= Sprites.A10.Frames || spriteNumber < 0)
            {
                throw new Exception("Bad sprite index");
            }

            spriteBatch.Draw(Sprites.Shadow.SpriteSheet, new Rectangle(x, y + FlyingHeight, Sprites.Shadow.Width, Sprites.Shadow.Height), Color.White);
            spriteBatch.Draw(Sprites.A10.SpriteSheet, new Rectangle(x, y, Sprites.A10.Width, Sprites.A10.Height), new Rectangle(Sprites.A10.Width * spriteNumber, 0, Sprites.A10.Width, Sprites.A10.Height), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 movement = new Vector2(
                (float)(Math.Cos(Rotation - MathHelper.PiOver2)),
                (float)((Math.Sin(Rotation - MathHelper.PiOver2)))
            );
            PositionVector += movement;
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

        public void Shoot(GameTime gameTime)
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
            Bullet bullet = new Bullet(World, Player.One, Position, bulletImpact);
            World.AddProjectile(bullet);
            Sounds.HeavyMachineGun.Play(0.5f, 0, 0);
        }

        public void Bomb(GameTime gameTime)
        {
            if (gameTime.TotalGameTime < LastBombingTime + TimeSpan.FromSeconds(BombingTime))
            {
                return;
            }

            LastBombingTime = gameTime.TotalGameTime;

            Point target = Position;
            Point start = Position;
            start.Y -= FlyingHeight;

            World.AddProjectile(new Bomblet(World, Player, start, target));
        }
    }
}
