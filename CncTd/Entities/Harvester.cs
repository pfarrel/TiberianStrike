using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CncTd.Entities
{
    class Harvester : BaseEntity
    {
        private const double RotationSpeed = (Math.PI * 2) / 3;  // per second
        private const double MovementSpeed = 20.0d;  // per second

        public Point Target { get; set; }
        private float Rotation { get; set; }

        public override int MaxHealth => 25;

        public Harvester(World world, Player player, Point position, Point target) : base(world, player, position)
        {
            Target = target;
            Rotation = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (Health == 0)
            {
                World.AddExplosion(new ExplosionBig(Position));
                Sounds.HarvesterExplosion.Play();
                return;
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
                    PositionVector += Vector2.Multiply(diffV, (float)(MovementSpeed * (gameTime.ElapsedGameTime.TotalSeconds)));
                }
                Rotation = targetRotation;
            }
        }

        protected override SpriteFrame GetSpriteFrame(GameTime gameTime)
        {
            return Sprites.Harvester.GetFrameForRotation(Rotation);
        }
    }
}
