﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiberianStrike.Entities.Explosions;

namespace TiberianStrike.Entities.Projectiles
{
    abstract class AbstractMissile : Projectile
    {
        protected virtual float TurnRate => 0.02f;
        protected float DistanceTravelled { get; set; }
        
        protected override Type ExplosionType => typeof(ShellExplosion);
        protected override SoundEffect ExplosionSound => Sounds.Explosion;

        public AbstractMissile(World world, Player player, Point position, IEntity targetEntity) : base(world, player, position, targetEntity.Position, targetEntity)
        {
        }

        public override void Update()
        {
            Point target = TargetEntity != null ? TargetEntity.Position : Target;
            Vector2 targetPositionVector = new Vector2(target.X, target.Y);
            
            float rotationDiff = VectorHelpers.FindRotationAdjustment(PositionVector, targetPositionVector, Rotation);
            float turnFactor = Math.Min(TurnRate / Math.Abs(rotationDiff), 1);
            float amountToTurn = rotationDiff * turnFactor;
            Rotation += amountToTurn;

            float distanceToTarget = Vector2.Distance(targetPositionVector, PositionVector);
            float distanceToFly = Math.Min(distanceToTarget, MovementSpeed);
            PositionVector = VectorHelpers.MoveInDirection(PositionVector, Rotation, distanceToFly);

            if (Vector2.Distance(targetPositionVector, PositionVector) < ExplosionRadius)
            {
                Explode();
            }
        }
    }
}
