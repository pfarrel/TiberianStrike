using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CncTd.Entities
{
    class Harvester : IPlayerEntity
    {
        private const double RotationSpeed = (Math.PI * 2) / 3;  // per second
        private const double MovementSpeed = 20.0d;  // per second
        private const int MaxHealth = 25;

        public Player Player { get; }
        public Point Position
        {
            get
            {
                return new Point((int)RealPosition.X, (int)RealPosition.Y);
            }
        }
        public Point Target { get; set; }
        public int Health { get; private set; }

        private Vector2 RealPosition { get; set; }
        private double Rotation { get; set; }
        private World World { get; set; }
        public Boolean IsAlive { get; private set; }

        public Harvester(World world, Player player, Point position, Point target)
        {
            World = world;
            Player = player;
            RealPosition = new Vector2(position.X, position.Y);
            Target = target;
            Rotation = 0;
            Health = MaxHealth;
            IsAlive = true;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int x = Math.Max(0, Position.X - 24);
            int y = Math.Max(0, Position.Y - 24);

            double adjustedRotation = Rotation < 0 ? Rotation + Math.PI * 2 : Rotation;
            int spriteNumber = (int) ((adjustedRotation / (Math.PI * 2)) * Sprites.Harvester.Frames);
            spriteNumber -= 1;
            spriteNumber = (Sprites.Harvester.Frames - 1) - spriteNumber;
            spriteNumber %= Sprites.Harvester.Frames;

            //Console.WriteLine("Rotation: {0}, AdjustedRotation: {1}, SpriteNumber: {2}", Rotation, adjustedRotation, spriteNumber);
            if (spriteNumber >= 32 || spriteNumber < 0)
            {
                throw new Exception("Bad sprite index");
            }

            spriteBatch.Draw(Sprites.Harvester.SpriteSheet, new Rectangle(x, y, 48, 48), new Rectangle(48 * spriteNumber, 0, 48, 48), Color.White);

            int maxHealthBarWidth = 24;
            float healthFraction = (float) Health / MaxHealth;
            int healthBarWidth = (int)(healthFraction * (maxHealthBarWidth - 2));
            Color barColor = healthFraction > 0.5 ? Color.LimeGreen : healthFraction > 0.25 ? Color.Gold : Color.Red;

            spriteBatch.Draw(Sprites.None.SpriteSheet, new Rectangle(x + 12, y + 2, maxHealthBarWidth, 4), new Rectangle(0, 0, 1, 1), Color.Black);
            spriteBatch.Draw(Sprites.None.SpriteSheet, new Rectangle(x + 12 + 1, y + 3, healthBarWidth, 2), new Rectangle(0, 0, 1, 1), barColor);
        }

        public void Update(GameTime gameTime)
        {
            if (IsAlive)
            {
                if (Health == 0)
                {
                    IsAlive = false;
                    World.AddExplosion(new ExplosionBig(Position));
                    Sounds.HarvesterExplosion.Play();
                }
            }
            if (Position != Target)
            {
                Point diff = Target - Position;
                Vector2 diffV = new Vector2(diff.X, diff.Y);
                diffV.Normalize();
                float targetRotation = (float)Math.Atan2(diffV.X, -diffV.Y);
                //Console.WriteLine("Source: {0}, Target: {1}, TargetVector: {2} TargetRotation: {3}", Position, Target, diffV, targetRotation);
                if (Rotation == targetRotation)
                {
                    RealPosition += Vector2.Multiply(diffV, (float)(MovementSpeed * (gameTime.ElapsedGameTime.TotalSeconds)));
                }
                Rotation = targetRotation;
            }
        }

        public void Damage(int amount)
        {
            Health = Math.Max(0, Health - amount);
        }
    }
}
