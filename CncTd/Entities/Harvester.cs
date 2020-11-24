using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TiberianStrike.Entities.Explosions;

namespace TiberianStrike.Entities
{
    class Harvester : BaseEntity
    {
        private const float RotationSpeed = (float)(Math.PI * 2) / 3;  // per second
        private const float MovementSpeed = 20.0f / 60;  // per tick

        public Point Target { get; set; }
        private float Rotation { get; set; }

        public override int MaxHealth => 10;

        protected override Type ExplosionType => typeof(ExplosionBig);

        protected override SoundEffect ExplosionSound => Sounds.HarvesterExplosion;

        public Harvester(World world, Player player, Point position, Point target) : base(world, player, position)
        {
            Target = target;
            Rotation = 0;
        }

        public override void Update()
        {
            if (Position != Target)
            {
                float targetRotation = VectorHelpers.GetRotationToFace(PositionVector, new Vector2(Target.X, Target.Y)) ?? Rotation;
                if (Rotation == targetRotation)
                {
                    PositionVector = VectorHelpers.MoveInDirection(PositionVector, targetRotation, MovementSpeed);
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
