using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CncTd.Entities
{
    abstract class BaseEntity : IEntity
    {
        public int Health { get; protected set; }
        public bool IsAlive { get { return Health > 0; } }
        public abstract int MaxHealth { get; }
        public Player Player { get; }
        public Point Position { get { return new Point(Convert.ToInt32(PositionVector.X), Convert.ToInt32(PositionVector.Y)); } }
        public Vector2 PositionVector { get; protected set; }
        protected World World { get; }

        protected BaseEntity(World world, Player player, Point position)
        {
            Health = MaxHealth;
            Player = player;
            PositionVector = new Vector2(position.X, position.Y);
            World = world;
        }

        public void Damage(int amount)
        {
            if (IsAlive)
            {
                if (Health - amount <= 0)
                {
                    Health = 0;
                }
                else
                {
                    Health -= amount;
                }
            }
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
