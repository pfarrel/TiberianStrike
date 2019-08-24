using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiberianStrike.Entities.Explosions;
using TiberianStrike.Entities.Projectiles;

namespace TiberianStrike.Entities
{
    class A10 : BaseEntity
    {
        private const int FlyingHeight = 30;
        private const double MovementSpeed = 20.0d / 60;  // per tick
        private const int FiringTimeTicks = 30;
        private const double BombingTimeTicks = 12;
        private const float GunRange = 100f;

        private float Rotation { get; set; }
        private int? LastBombingTicks { get; set; }
        private int? LastFiringTicks { get; set; }

        public override int MaxHealth => 100;
        protected override float EntityZOrder => ZOrder.FlyingUnits;

        public A10(World world, Player player, Point position) : base(world, player, position)
        {
            Rotation = MathHelper.ToRadians(90);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int x = Position.X - Sprites.A10.Width / 2;
            int y = Position.Y - Sprites.A10.Height / 2;

            spriteBatch.Draw(Sprites.Shadow.Texture, new Rectangle(x, y + FlyingHeight, Sprites.Shadow.Width, Sprites.Shadow.Height), Color.White);

            base.Draw(spriteBatch);
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            return Sprites.A10.GetFrameForRotation(Rotation);
        }

        public override void Update()
        {
            Vector2 movement = new Vector2(
                (float)(Math.Cos(Rotation - MathHelper.PiOver2)),
                (float)((Math.Sin(Rotation - MathHelper.PiOver2)))
            );
            PositionVector += movement;
        } 

        public void TurnLeft()
        {
            Rotation -= 0.05f;
            Rotation %= MathHelper.TwoPi;
        }

        public void TurnRight()
        {
            Rotation += 0.05f;
            Rotation %= MathHelper.TwoPi;
        }

        public void Shoot()
        {
            if (LastFiringTicks != null && World.Ticks - LastFiringTicks < FiringTimeTicks)
            {
                return;
            }

            LastFiringTicks = World.Ticks;

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

        public void Bomb()
        {
            if (LastBombingTicks != null && World.Ticks - LastBombingTicks < BombingTimeTicks)
            {
                return;
            }

            LastBombingTicks = World.Ticks;

            Point target = Position;
            target.Y += FlyingHeight;

            World.AddProjectile(new Bomblet(World, Player, Position, target));
        }

        public void Rocket()
        {
            if (LastBombingTicks != null && World.Ticks - LastBombingTicks < BombingTimeTicks)
            {
                return;
            }

            LastBombingTicks = World.Ticks;

            Vector2 rocketDirection = new Vector2(
                (float)(Math.Cos(Rotation - MathHelper.PiOver2)),
                (float)((Math.Sin(Rotation - MathHelper.PiOver2)))
            );
            rocketDirection.Normalize();
            rocketDirection = GunRange * 2 * rocketDirection;
            Point rocketTarget = Position + new Point((int)rocketDirection.X, (int)rocketDirection.Y);
            rocketTarget.Y += FlyingHeight;

            rocketTarget.X += new Random().Next(15) - 30;
            rocketTarget.Y += new Random().Next(15) - 30;

            Sounds.Rocket2.Play();
            World.AddProjectile(new Rocket(World, Player, Position, rocketTarget));
        }
    }
}
