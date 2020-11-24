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

        protected virtual int ExplosionRadius => 15;
        protected virtual int Damage => 10;
        protected abstract SpriteSheet Sprite { get; }
        protected abstract Type ExplosionType { get; }
        protected abstract SoundEffect ExplosionSound { get; }
        protected abstract Warhead Warhead { get; }
        protected abstract float MovementSpeed { get; }

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
        
        protected bool AirTarget { get; }
        protected int CreatedTicks { get; set; }

        public Projectile(World world, Player player, Point position, Point target, bool airTarget = false)
        {
            World = world;
            Player = player;
            PositionVector = new Vector2(position.X, position.Y);
            Target = target;
            IsAlive = true;
            AirTarget = airTarget;
            CreatedTicks = world.Ticks;
            Rotation = VectorHelpers.GetRotationToFace(PositionVector, new Vector2(Target.X, Target.Y)) ?? 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int x = Math.Max(0, Position.X - Sprite.Width / 2);
            int y = Math.Max(0, Position.Y - Sprite.Height / 2);

            SpriteFrame frame = GetSpriteFrame();

            spriteBatch.Draw(Sprite.Texture, new Rectangle(x, y, Sprite.Width, Sprite.Height), frame.Coordinates, Color.White, 0, Vector2.Zero, SpriteEffects.None, ZOrder.Projectiles);
        }

        protected virtual SpriteFrame GetSpriteFrame()
        {
            return Sprite.GetFrameForAnimationAndRotation("default", Rotation, World.Ticks - CreatedTicks, 3);
        }

        public virtual void Update()
        {
            Vector2 targetVector = new Vector2(Target.X, Target.Y);
            float distanceToTarget = Vector2.Distance(PositionVector, targetVector);
            double speedPerFrame = MovementSpeed;
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
                entity.Damage(damage, Warhead);
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
