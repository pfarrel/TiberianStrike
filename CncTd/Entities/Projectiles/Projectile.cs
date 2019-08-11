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
    abstract class Projectile
    {
        private const double ExplosionRadius = 15;
        private const int Damage = 10;
        private readonly SpriteWrapper sprite;

        protected virtual Type ExplosionType { get { return null; } }
        protected virtual SoundEffect ExplosionSound { get { return null; } }
        public Player Player { get; }
        public Point Position
        {
            get
            {
                return new Point((int)RealPosition.X, (int)RealPosition.Y);
            }
        }
        public Point Target { get; set; }
        public bool IsAlive { get; private set; }
        private World World { get; }

        private Vector2 RealPosition { get; set; }
        private float MovementSpeed { get; set; }
        private float Rotation { get; set; }
        private int CreatedTicks { get; set; }

        public Projectile(World world, Player player, Point position, Point target, SpriteWrapper sprite, float speed)
        {
            World = world;
            Player = player;
            RealPosition = new Vector2(position.X, position.Y);
            Target = target;
            IsAlive = true;
            this.sprite = sprite;
            MovementSpeed = speed;
            CreatedTicks = world.Ticks;

            Point diff = Target - Position;
            Vector2 diffV = new Vector2(diff.X, diff.Y);
            diffV.Normalize();
            float targetRotation = (float)Math.Atan2(diffV.X, -diffV.Y);
            Rotation = targetRotation;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int x = Math.Max(0, Position.X - sprite.Width / 2);
            int y = Math.Max(0, Position.Y - sprite.Height / 2);

            SpriteFrame frame = GetSpriteFrame(gameTime);

            spriteBatch.Draw(sprite.SpriteSheet, new Rectangle(x, y, sprite.Width, sprite.Height), frame.Coordinates, Color.White);
        }

        protected virtual SpriteFrame GetSpriteFrame(GameTime gameTime)
        {
            return sprite.GetFrameForAnimationAndRotation("default", Rotation, World.Ticks - CreatedTicks);
        }

        public void Update(GameTime gameTime)
        {
            Vector2 targetVector = new Vector2(Target.X, Target.Y);
            float distanceToTarget = Vector2.Distance(RealPosition, targetVector);
            double speedPerFrame = MovementSpeed * (gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
            if (Target == Position)
            {
                IsAlive = false;
                List<IEntity> inRadius = World.Entities
                    .Where(e => Vector2.Distance(new Vector2(e.Position.X, e.Position.Y), targetVector) < ExplosionRadius)
                    .ToList();
                foreach (IEntity entity in inRadius)
                {
                    float distance = Vector2.Distance(new Vector2(entity.Position.X, entity.Position.Y), targetVector);
                    int damage = (int)(Damage * (distance / ExplosionRadius));
                    entity.Damage(damage);
                }
                Explode(gameTime);
            }
            else if (distanceToTarget < speedPerFrame)
            {
                RealPosition = new Vector2(Target.X, Target.Y);
            }
            else
            {
                Vector2 diff = targetVector - RealPosition;
                diff.Normalize();
                RealPosition += Vector2.Multiply(diff, (float) speedPerFrame);
            }
        }

        protected virtual void Explode(GameTime gameTime)
        {
            if (ExplosionType != null)
            {
                Explosion explosion = (Explosion)Activator.CreateInstance(ExplosionType, new object[] { World, Position });
                World.AddExplosion(explosion);
            }
            if (ExplosionSound != null)
            {
                ExplosionSound.Play();
            }
        }
    }
}
