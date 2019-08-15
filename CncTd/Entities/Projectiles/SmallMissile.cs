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
        private const float TurnRate = 0.05f;
        protected IEntity TargetEntity { get; }

        protected override Type ExplosionType => typeof(ShellExplosion);

        public SmallMissile(World world, Player player, Point position, Point target, IEntity targetEntity) : base(world, player, position, target, Sprites.Dragon, 100f)
        {
            TargetEntity = targetEntity;
        }

        public override void Update(GameTime gameTime)
        {
            if (Vector2.Distance(TargetEntity.PositionVector, PositionVector) < ExplosionRadius)
            {
                Explode();
                return;
            }

            float targetRotation = VectorHelpers.GetRotationToFace(PositionVector, TargetEntity.PositionVector);
            float rotationDiff = targetRotation - Rotation;
            float amountToTurn = Math.Min(TurnRate, rotationDiff);
            Rotation += amountToTurn;

            PositionVector = VectorHelpers.MoveInDirection(PositionVector, Rotation, MovementSpeed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000));
        }
    }
}
