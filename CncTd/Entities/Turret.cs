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
    class Turret : BaseEntity
    {
        private const double RotationSpeed = (Math.PI * 2) / 25;  // per second
        private const float TimeToBuild = 5000;
        private const int Range = 200;
        private readonly TimeSpan FiringInterval = TimeSpan.FromSeconds(1); // 1 second

        public Point Target { get; set; }

        private double Rotation { get; set; }
        private Boolean Constructing { get; set; }
        private TimeSpan TimeWhenCreated { get; }
        private IEntity TargetEntity { get; set; }
        private TimeSpan LastShot { get; set;}

        public override int MaxHealth => 200;

        public Turret(World world, Player player, Point position, TimeSpan timeWhenCreated) : base(world, player, position)
        {
            Constructing = true;
            Rotation = 0;
            Target = new Point(0, 0);
            TimeWhenCreated = timeWhenCreated;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int x = Math.Max(0, Position.X - Sprites.Turret.Width / 2);
            int y = Math.Max(0, Position.Y - Sprites.Turret.Height / 2);

            SpriteWrapper spriteToUse;
            int spriteNumber;
            if (Constructing)
            {
                spriteToUse = Sprites.TurretConstructing;

                double fraction = (gameTime.TotalGameTime.TotalMilliseconds - TimeWhenCreated.TotalMilliseconds) / TimeToBuild;
                spriteNumber = (int)(fraction * (spriteToUse.Frames - 1));
            }
            else
            {
                spriteToUse = Player == Player.One ? Sprites.Turret : Sprites.Turret; // no nod support

                double adjustedRotation = Rotation < 0 ? Rotation + Math.PI * 2 : Rotation;
                spriteNumber = (int)((adjustedRotation / (Math.PI * 2)) * spriteToUse.Frames);
                spriteNumber -= 1;
                spriteNumber = (spriteToUse.Frames - 1) - spriteNumber;
                spriteNumber %= spriteToUse.Frames;

                //Console.WriteLine("Rotation: {0}, AdjustedRotation: {1}, SpriteNumber: {2}", Rotation, adjustedRotation, spriteNumber);
                if (spriteNumber >= spriteToUse.Frames || spriteNumber < 0)
                {
                    throw new Exception("Bad sprite index");
                }
            }

            spriteBatch.Draw(spriteToUse.SpriteSheet, new Rectangle(x, y, spriteToUse.Width, spriteToUse.Height), new Rectangle(spriteToUse.Width * spriteNumber, 0, spriteToUse.Width, spriteToUse.Height), Color.White);
        }

        public override void Update(GameTime gameTime)
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
                    if (distance > Range || !TargetEntity.IsAlive)
                    {
                        TargetEntity = null;
                    }
                }
                if (TargetEntity == null)
                {
                    IEntity nearestEnemyHarvester = World.Entities.Where(e => e.Player != Player && e is Harvester)
                        .OrderBy(e => Vector2.Distance(new Vector2(Position.X, Position.Y), new Vector2(e.Position.X, e.Position.Y)))
                        .FirstOrDefault();
                    if (nearestEnemyHarvester != null)
                    {
                        float distance = Vector2.Distance(new Vector2(Position.X, Position.Y), new Vector2(nearestEnemyHarvester.Position.X, nearestEnemyHarvester.Position.Y));
                        if (distance < Range)
                        {
                            TargetEntity = nearestEnemyHarvester;
                        }
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
                            int x = Math.Max(0, Position.X - Sprites.Turret.Width / 2);
                            int y = Math.Max(0, Position.Y - Sprites.Turret.Height / 2);


                            Projectile bullet = new CannonShot(World, Player, Position, Target);
                            World.AddProjectile(bullet);
                            LastShot = gameTime.TotalGameTime;
                            Sounds.CannonShot.Play();
                        }
                    }
                    else
                    {
                        Rotation+= rotationPerFrame;
                    }
                }
            }
        }
    }
}
