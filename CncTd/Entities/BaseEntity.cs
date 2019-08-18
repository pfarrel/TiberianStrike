﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace TiberianStrike.Entities
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
        protected virtual Type ExplosionType { get { return null; } }
        protected virtual SoundEffect ExplosionSound { get { return null; } }
        protected virtual int HealthBarOffset { get { return 2; } }

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
                    Die();
                }
                else
                {
                    Health -= amount;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            SpriteFrame spriteFrame = GetSpriteFrame();
            int x = Position.X - spriteFrame.Coordinates.Width / 2;
            int y = Position.Y - spriteFrame.Coordinates.Height / 2;

            spriteBatch.Draw(spriteFrame.Texture, new Rectangle(x, y, spriteFrame.Coordinates.Width, spriteFrame.Coordinates.Height), spriteFrame.Coordinates, Color.White);

            int maxHealthBarWidth = spriteFrame.Coordinates.Width / 2;
            float healthFraction = (float)Health / MaxHealth;
            int healthBarWidth = (int)(healthFraction * (maxHealthBarWidth - 2));
            Color barColor = healthFraction > 0.5 ? Color.LimeGreen : healthFraction > 0.25 ? Color.Gold : Color.Red;

            spriteBatch.Draw(Sprites.None.Texture, new Rectangle(x + maxHealthBarWidth / 2, y + HealthBarOffset, maxHealthBarWidth, 4), new Rectangle(0, 0, 1, 1), Color.Black);
            spriteBatch.Draw(Sprites.None.Texture, new Rectangle(x + maxHealthBarWidth / 2 + 1, y + HealthBarOffset + 1, healthBarWidth, 2), new Rectangle(0, 0, 1, 1), barColor);
        }

        public virtual void Update(GameTime gameTime) { }

        protected abstract SpriteFrame GetSpriteFrame();

        protected virtual void Die()
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
