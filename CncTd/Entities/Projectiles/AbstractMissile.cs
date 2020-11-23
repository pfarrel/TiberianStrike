using Microsoft.Xna.Framework;
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
        protected const float TurnRate = 0.02f;
        protected IEntity TargetEntity { get; }
        protected float DistanceTravelled { get; set; }
        
        protected override Type ExplosionType => typeof(ShellExplosion);
        protected override SoundEffect ExplosionSound => Sounds.Explosion;

        public AbstractMissile(World world, Player player, Point position, Point target) : base(world, player, position, target, Sprites.Dragon, 100f / 60, false)
        {
            TargetEntity = null;
        }

        public AbstractMissile(World world, Player player, Point position, IEntity targetEntity) : base(world, player, position, targetEntity.Position, Sprites.Dragon, 100f / 60, targetEntity is A10)
        {
            TargetEntity = targetEntity;
        }

        public override void Update()
        {
            float targetRotation = VectorHelpers.GetRotationToFace(PositionVector, TargetEntity.PositionVector) ?? Rotation;
            float rotationDiff = targetRotation - Rotation;
            float amountToTurn = Math.Min(TurnRate, rotationDiff);
            Rotation += amountToTurn;

            float distanceToTarget = Vector2.Distance(TargetEntity.PositionVector, PositionVector);
            float distanceToFly = Math.Min(distanceToTarget, MovementSpeed);
            PositionVector = VectorHelpers.MoveInDirection(PositionVector, Rotation, distanceToFly);

            if (Vector2.Distance(TargetEntity.PositionVector, PositionVector) < ExplosionRadius)
            {
                Explode();
            }
        }
    }
}
