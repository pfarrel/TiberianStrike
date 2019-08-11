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

        public override int MaxHealth => 100;

        public A10(World world, Player player, Point position) : base(world, player, position)
        {
            Rotation = MathHelper.ToRadians(90);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int x = Position.X - Sprites.A10.Width / 2;
            int y = Position.Y - Sprites.A10.Height / 2;

            spriteBatch.Draw(Sprites.Shadow.SpriteSheet, new Rectangle(x, y + FlyingHeight, Sprites.Shadow.Width, Sprites.Shadow.Height), Color.White);

            base.Draw(gameTime, spriteBatch);
        }

        protected override SpriteFrame GetSpriteFrame(GameTime gameTime)
        {
            return Sprites.A10.GetFrameForRotation(Rotation);
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
            bulletImpact.Y += FlyingHeight;
            Bullet bullet = new Bullet(World, Player.One, Position, bulletImpact);
            World.AddProjectile(bullet);
            Sounds.HeavyMachineGun.Play(0.5f, 0, 0);
            World.AddExplosion(new GunfireMuzzle(World, this, Rotation));
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
            target.Y += FlyingHeight;

            World.AddProjectile(new Bomblet(World, Player, start, target));
        }
    }
}
