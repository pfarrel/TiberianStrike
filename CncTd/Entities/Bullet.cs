using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class Bullet
    {
        private const double MovementSpeed = 120.0d;  // per second

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

        public Bullet(Game game, Player player, Point position, Point target)
        {
            Player = player;
            RealPosition = new Vector2(position.X, position.Y);
            Target = target;
            Alive = true;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D sprite)
        {
            int x = Math.Max(0, Position.X - 12);
            int y = Math.Max(0, Position.Y - 12);

            spriteBatch.Draw(sprite, new Rectangle(x, y, 24, 24), new Rectangle(0, 0, 24, 24), Color.White);
        }

        public void Update(GameTime gameTime, List<IPlayerEntity> playerEntities)
        {
            Vector2 targetVector = new Vector2(Target.X, Target.Y);
            float distanceToTarget = Vector2.Distance(RealPosition, targetVector);
            double speedPerFrame = MovementSpeed * (gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
            if (Target == Position)
            {
                Alive = false;
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
    }
}
