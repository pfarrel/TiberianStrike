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
        private const float RotationSpeed = (float) (Math.PI * 2) / 25;  // per second
        private const int TicksToBuild = 60 * 3;
        private const int Range = 200;
        private readonly TimeSpan FiringInterval = TimeSpan.FromSeconds(1); // 1 second

        public Point Target { get; set; }

        private float Rotation { get; set; }
        private bool Constructing { get; set; }
        private int CreatedTicks { get; }
        private IEntity TargetEntity { get; set; }
        private TimeSpan LastShot { get; set;}

        public override int MaxHealth => 200;

        public Turret(World world, Player player, Point position) : base(world, player, position)
        {
            Constructing = true;
            Rotation = Rotation = MathHelper.ToRadians(290);
            Target = new Point(0, 0);
            CreatedTicks = world.Ticks;
        }

        public override void Update(GameTime gameTime)
        {
            if (World.Ticks >= CreatedTicks + TicksToBuild)
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
                    float targetRotation = (float) Math.Atan2(diffV.X, -diffV.Y);
                    float rotationDiff = targetRotation - Rotation;
                    float rotationPerFrame = (float) (RotationSpeed * (gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0d));
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
                        Rotation += rotationPerFrame;
                    }
                }
            }
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            if (Constructing)
            {
                return Sprites.TurretConstructing.GetFrameForAnimationAndRotation(0, CreatedTicks - World.Ticks);
            }
            else
            {
                return Sprites.Turret.GetFrameForRotation(Rotation);
            }
        }
    }
}
