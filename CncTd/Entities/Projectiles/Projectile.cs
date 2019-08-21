using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiberianStrike.Entities.Explosions;

namespace TiberianStrike.Entities.Projectiles
{
    abstract class Projectile
    {
        protected const int FrameRepeat = 3;
        protected const double ExplosionRadius = 15;
        protected const int Damage = 10;
        protected readonly SpriteSheet Sprite;

        protected virtual Type ExplosionType { get { return null; } }
        protected virtual SoundEffect ExplosionSound { get { return null; } }
        public Player Player { get; }
        public Point Position
        {
            get
            {
                return new Point((int)PositionVector.X, (int)PositionVector.Y);
            }
        }
        public Point Target { get; set; }
        public bool IsAlive { get; private set; }
        protected World World { get; }

        protected Vector2 PositionVector { get; set; }
        protected float Rotation { get; set; }
        protected float MovementSpeed { get; set; }
        protected bool AirTarget { get; }
        protected int CreatedTicks { get; set; }

        public Projectile(World world, Player player, Point position, Point target, SpriteSheet sprite, float speed, Boolean airTarget = false)
        {
            World = world;
            Player = player;
            PositionVector = new Vector2(position.X, position.Y);
            Target = target;
            IsAlive = true;
            Sprite = sprite;
            MovementSpeed = speed;
            AirTarget = airTarget;
            CreatedTicks = world.Ticks;

            Point diff = Target - Position;
            Vector2 diffV = new Vector2(diff.X, diff.Y);
            diffV.Normalize();
            float targetRotation = (float)Math.Atan2(diffV.X, -diffV.Y);
            Rotation = targetRotation;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int x = Math.Max(0, Position.X - Sprite.Width / 2);
            int y = Math.Max(0, Position.Y - Sprite.Height / 2);

            SpriteFrame frame = GetSpriteFrame(gameTime);

            spriteBatch.Draw(Sprite.Texture, new Rectangle(x, y, Sprite.Width, Sprite.Height), frame.Coordinates, Color.White);
        }

        protected virtual SpriteFrame GetSpriteFrame(GameTime gameTime)
        {
            return Sprite.GetFrameForAnimationAndRotation("default", Rotation, World.Ticks - CreatedTicks, 3);
        }

        public virtual void Update(GameTime gameTime)
        {
            Vector2 targetVector = new Vector2(Target.X, Target.Y);
            float distanceToTarget = Vector2.Distance(PositionVector, targetVector);
            double speedPerFrame = MovementSpeed * (gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
            if (Target == Position)
            {
                Explode();
            }
            else if (distanceToTarget < speedPerFrame)
            {
                PositionVector = new Vector2(Target.X, Target.Y);
            }
            else
            {
                Vector2 diff = targetVector - PositionVector;
                diff.Normalize();
                PositionVector += Vector2.Multiply(diff, (float) speedPerFrame);
            }
        }

        protected virtual void Explode()
        {
            IsAlive = false;
            List<IEntity> inRadius = World.Entities
                .Where(e => Vector2.Distance(new Vector2(e.Position.X, e.Position.Y), PositionVector) < ExplosionRadius)
                .ToList();
            foreach (IEntity entity in inRadius)
            {
                float distance = Vector2.Distance(new Vector2(entity.Position.X, entity.Position.Y), PositionVector);
                int damage = (int)(Damage * (distance / ExplosionRadius));
                entity.Damage(damage);
            }
            MakeExplosion();
        }

        protected virtual void MakeExplosion()
        {
            if (ExplosionType != null)
            {
                Explosion explosion = (Explosion)Activator.CreateInstance(ExplosionType, new object[] { World, Position, AirTarget ? ExplosionHeight.Air : ExplosionHeight.Ground });
                World.AddExplosion(explosion);
            }
            if (ExplosionSound != null)
            {
                ExplosionSound.Play();
            }
        }
    }
}
