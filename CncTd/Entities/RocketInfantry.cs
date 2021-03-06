﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TiberianStrike.Entities.Explosions;
using TiberianStrike.Entities.Projectiles;

namespace TiberianStrike.Entities
{
    class RocketInfantry : BaseEntity
    {
        private const float MovementPerTick = 0.25f;  // per second
        private const double ChaseRange = 200;
        private const double ShootRange = 100;
        private const int TicksBetweenShots = 196;

        private float Rotation { get; set; }
        private InfantryState State { get; set; }
        private int EnteredStateTicks { get; set; }
        private int LastShotTicks { get; set; }

        public override int MaxHealth => 1;

        protected override Type ExplosionType => typeof(ExplosionBig);

        protected override SoundEffect ExplosionSound => Sounds.HarvesterExplosion;

        public RocketInfantry(World world, Player player, Point position) : base(world, player, position)
        {
            Rotation = MathHelper.Pi;
            State = InfantryState.Stand;
            EnteredStateTicks = world.Ticks;
        }

        public override void Update()
        {
            A10 a10 = World.GetEntities<A10>().First();
            float a10Distance = Vector2.Distance(a10.PositionVector, PositionVector);
            float rotationToA10 = VectorHelpers.GetRotationToFace(PositionVector, a10.PositionVector) ?? Rotation;

            if (a10Distance < ShootRange && State != InfantryState.Shoot && World.Ticks >= LastShotTicks + TicksBetweenShots)
            {
                State = InfantryState.Shoot;
                EnteredStateTicks = World.Ticks;
                LastShotTicks = World.Ticks;
            }
            else if (a10Distance < ShootRange && State != InfantryState.Shoot && State != InfantryState.Aiming && World.Ticks < LastShotTicks + TicksBetweenShots)
            {
                State = InfantryState.Aiming;
                EnteredStateTicks = World.Ticks;
            }
            else if (a10Distance < ChaseRange && State == InfantryState.Stand)
            {
                State = InfantryState.Run;
                EnteredStateTicks = World.Ticks;
            }
            else if (a10Distance < ChaseRange && a10Distance > ShootRange && (State == InfantryState.Shoot || State == InfantryState.Aiming))
            {
                State = InfantryState.Run;
                EnteredStateTicks = World.Ticks;
            }
            else if (a10Distance > ChaseRange && State != InfantryState.Stand)
            {
                State = InfantryState.Stand;
                EnteredStateTicks = World.Ticks;
                Rotation = MathHelper.Pi;
            }

            if (State == InfantryState.Stand)
            {
                // ?
            }
            else if (State == InfantryState.Run)
            {
                Rotation = rotationToA10;
                PositionVector = VectorHelpers.MoveInDirection(PositionVector, Rotation, MovementPerTick);
            }
            else if (State == InfantryState.Aiming)
            {
                Rotation = rotationToA10;
            }
            else if (State == InfantryState.Shoot)
            {
                Rotation = rotationToA10;
                if ((World.Ticks - EnteredStateTicks) % 64 == 48)
                {
                    Point missileStartPosition = new Point(Position.X, Position.Y - 6);
                    SmallMissile smallMissile = new SmallMissile(World, Player.Two, missileStartPosition, a10);
                    World.AddProjectile(smallMissile);
                    Sounds.Bazooka.Play();
                }
                if ((World.Ticks - EnteredStateTicks) % 64 == 63)
                {
                    State = InfantryState.Aiming;
                }
            }
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            InfantryState stateForSprite = State;
            if (stateForSprite == InfantryState.Aiming)
            {
                stateForSprite = InfantryState.Stand;
            }
            string name = stateForSprite.ToString().ToLower();
            return Sprites.RocketInfantry.GetFrameForAnimationAndRotation(name, Rotation, EnteredStateTicks - World.Ticks, 8);
        }

        protected override void Die(Warhead warhead)
        {

            InfantryDeathType infantryDeathType = warhead.GetInfantryDeathType();
            Explosion explosion = new InfantryDeath(World, Position, Sprites.RocketInfantry, infantryDeathType.GetSpriteName());
            World.AddExplosion(explosion);

            infantryDeathType.GetSoundEffect().Play();
        }
    }
}
