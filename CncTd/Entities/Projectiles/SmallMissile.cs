using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    class SmallMissile : Projectile
    {
        protected const float TurnRate = 0.02f;
        protected IEntity TargetEntity { get; }
        protected float DistanceTravelled { get; set; }

        protected override Type ExplosionType => typeof(ShellExplosion);
        protected override SoundEffect ExplosionSound => Sounds.Explosion;

        public SmallMissile(World world, Player player, Point position, Point target, IEntity targetEntity) : base(world, player, position, target, Sprites.Dragon, 100f)
        {
            TargetEntity = targetEntity;
        }

        public override void Update(GameTime gameTime)
        {
            float targetRotation = VectorHelpers.GetRotationToFace(PositionVector, TargetEntity.PositionVector) ?? Rotation;
            float rotationDiff = targetRotation - Rotation;
            float amountToTurn = Math.Min(TurnRate, rotationDiff);
            Rotation += amountToTurn;

            float distanceToTarget = Vector2.Distance(TargetEntity.PositionVector, PositionVector);
            float distanceToFly = Math.Min(distanceToTarget, MovementSpeed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000));
            PositionVector = VectorHelpers.MoveInDirection(PositionVector, Rotation, distanceToFly);

            if (Vector2.Distance(TargetEntity.PositionVector, PositionVector) < ExplosionRadius)
            {
                Explode();
            }
        }
    }
}
