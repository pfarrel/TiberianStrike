using Microsoft.Xna.Framework;
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

        public Player Player { get; }
        public Point Position
        {
            get
            {
                return new Point((int)RealPosition.X, (int)RealPosition.Y);
            }
        }
        public Point Target { get; set; }
        public bool Alive { get; private set; }

        private Vector2 RealPosition { get; set; }
        private float MovementSpeed { get; set; }
        private int FrameNumber { get; set; }


        public Projectile(Game game, Player player, Point position, Point target, SpriteWrapper sprite, float speed)
        {
            Player = player;
            RealPosition = new Vector2(position.X, position.Y);
            Target = target;
            Alive = true;
            this.sprite = sprite;
            MovementSpeed = speed;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int x = Math.Max(0, Position.X - sprite.Width);
            int y = Math.Max(0, Position.Y - sprite.Height);

            spriteBatch.Draw(sprite.SpriteSheet, new Rectangle(x, y, sprite.Width, sprite.Height), new Rectangle((FrameNumber / 2) * sprite.Width, 0, sprite.Width, sprite.Height), Color.White);
        }

        public void Update(GameTime gameTime, List<IPlayerEntity> playerEntities)
        {
            Vector2 targetVector = new Vector2(Target.X, Target.Y);
            float distanceToTarget = Vector2.Distance(RealPosition, targetVector);
            double speedPerFrame = MovementSpeed * (gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
            if (Target == Position)
            {
                Alive = false;
                List<IPlayerEntity> inRadius = playerEntities.Where(e => Vector2.Distance(new Vector2(e.Position.X, e.Position.Y), targetVector) < ExplosionRadius)
                    .ToList();
                foreach (IPlayerEntity entity in inRadius)
                {
                    float distance = Vector2.Distance(new Vector2(entity.Position.X, entity.Position.Y), targetVector);
                    int damage = (int)(Damage * (distance / ExplosionRadius));
                    entity.Damage(damage);
                }
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

            FrameNumber++;
            FrameNumber %= sprite.Frames * 2;
        }
    }
}
