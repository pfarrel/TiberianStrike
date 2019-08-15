using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TiberianStrike.Entities
{
    class Harvester : BaseEntity
    {
        private const double RotationSpeed = (Math.PI * 2) / 3;  // per second
        private const double MovementSpeed = 20.0d;  // per second

        public Point Target { get; set; }
        private float Rotation { get; set; }

        public override int MaxHealth => 25;

        protected override Type ExplosionType => typeof(ExplosionBig);

        protected override SoundEffect ExplosionSound => Sounds.HarvesterExplosion;

        public Harvester(World world, Player player, Point position, Point target) : base(world, player, position)
        {
            Target = target;
            Rotation = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (Position != Target)
            {
                Point diff = Target - Position;
                Vector2 diffV = new Vector2(diff.X, diff.Y);
                diffV.Normalize();
                float targetRotation = (float)Math.Atan2(diffV.X, -diffV.Y);
                if (Rotation == targetRotation)
                {
                    PositionVector += Vector2.Multiply(diffV, (float)(MovementSpeed * (gameTime.ElapsedGameTime.TotalSeconds)));
                }
                Rotation = targetRotation;
            }
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            return Sprites.Harvester.GetFrameForRotation(Rotation);
        }
    }
}
