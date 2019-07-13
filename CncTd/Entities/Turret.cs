using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    class Turret : DrawableGameComponent, IPlayerEntity
    {
        private const int Sprites = 32;
        private const int ConstructionSprites = 20;
        private const double RotationSpeed = (Math.PI * 2) / 30;  // per second
        private const int Size = 24;
        private const float TimeToBuild = 5000;
        private const int Range = 200;
        private readonly TimeSpan FiringInterval = TimeSpan.FromSeconds(1); // 1 second

        public Player Player { get; }
        public Point Position { get; }
        public Point Target { get; set; }

        private double Rotation { get; set; }
        private Boolean Constructing { get; set; }
        private TimeSpan TimeWhenCreated { get; }
        private IPlayerEntity TargetEntity { get; set; }
        private TimeSpan LastShot { get; set;}

        public Turret(Game game, Player player, Point position, TimeSpan timeWhenCreated) : base(game)
        {
            Player = player;
            Constructing = true;
            Position = position;
            Rotation = 0;
            Target = new Point(0, 0);
            TimeWhenCreated = timeWhenCreated;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D constructionSprite, Texture2D mainSprite)
        {
            int x = Math.Max(0, Position.X - Size / 2);
            int y = Math.Max(0, Position.Y - Size / 2);

            Texture2D spriteToUse;
            int spriteNumber;
            if (Constructing)
            {
                spriteToUse = constructionSprite;

                double fraction = (gameTime.TotalGameTime.TotalMilliseconds - TimeWhenCreated.TotalMilliseconds) / TimeToBuild;
                spriteNumber = (int)(fraction * (ConstructionSprites - 1));
            }
            else
            {
                spriteToUse = mainSprite;

                double adjustedRotation = Rotation < 0 ? Rotation + Math.PI * 2 : Rotation;
                spriteNumber = (int)((adjustedRotation / (Math.PI * 2)) * Sprites);
                spriteNumber -= 1;
                spriteNumber = (Sprites - 1) - spriteNumber;
                spriteNumber %= Sprites;

                //Console.WriteLine("Rotation: {0}, AdjustedRotation: {1}, SpriteNumber: {2}", Rotation, adjustedRotation, spriteNumber);
                if (spriteNumber >= Sprites || spriteNumber < 0)
                {
                    throw new Exception("Bad sprite index");
                }
            }

            spriteBatch.Draw(spriteToUse, new Rectangle(x, y, Size, Size), new Rectangle(Size * spriteNumber, 0, Size, Size), Color.White);

            base.Draw(gameTime);
        }

        public void Update(GameTime gameTime, List<IPlayerEntity> playerEntities, List<Bullet> bullets)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > TimeWhenCreated.TotalMilliseconds + TimeToBuild)
            {
                Constructing = false;
            }

            if (!Constructing)
            {
                if (TargetEntity != null)
                {
                    float distance = Vector2.Distance(new Vector2(Position.X, Position.Y), new Vector2(TargetEntity.Position.X, TargetEntity.Position.Y));
                    if (distance > Range)
                    {
                        TargetEntity = null;
                    }
                }
                if (TargetEntity == null)
                {
                    IPlayerEntity nearestEnemyHarvester = playerEntities.Where(e => e.Player != Player && e is Harvester)
                        .OrderBy(e => Vector2.Distance(new Vector2(Position.X, Position.Y), new Vector2(e.Position.X, e.Position.Y)))
                        .FirstOrDefault();
                    float distance = Vector2.Distance(new Vector2(Position.X, Position.Y), new Vector2(nearestEnemyHarvester.Position.X, nearestEnemyHarvester.Position.Y));
                    if (distance < Range)
                    {
                        TargetEntity = nearestEnemyHarvester;
                    }
                }

                if (TargetEntity != null)
                {
                    Target = TargetEntity.Position;
                    Point diff = Target - Position;
                    Vector2 diffV = new Vector2(diff.X, diff.Y);
                    diffV.Normalize();
                    double targetRotation = Math.Atan2(diffV.X, -diffV.Y);
                    double rotationDiff = targetRotation - Rotation;
                    double rotationPerFrame = RotationSpeed * (gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0d);
                    //Console.WriteLine("Source: {0}, Target: {1}, Diff: {2} MaxPerFrame: {3}", Rotation, targetRotation, rotationDiff, rotationPerFrame);
                    if (rotationDiff < rotationPerFrame)
                    {
                        Rotation = targetRotation;
                        if (LastShot == null || gameTime.TotalGameTime - LastShot > FiringInterval)
                        {
                            Bullet bullet = new Bullet(Game, Player, Position, Target);
                            bullets.Add(bullet);
                            LastShot = gameTime.TotalGameTime;
                        }
                    }
                    else
                    {
                        Rotation+= rotationPerFrame;
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
