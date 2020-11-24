using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    class Obelisk : Building
    {
        protected const int ChargeTicks = 180;
        protected const int FiringTicks = 15;
        protected const int RechargeTicks = 160;
        protected override SpriteSheet SpriteSheet => Sprites.Obelisk;
        protected IEntity target;
        protected ObeliskState state;
        protected int StateChangedTicks;
        protected int? LastShotTicks;

        public Obelisk(World world, Player player, Point position) : base(world, player, position)
        {
            target = null;
            state = ObeliskState.Idle;
            StateChangedTicks = world.Ticks;
        }

        public override void Update()
        {
            IEntity nearestEnemy = World.Entities.Where(e => e.Player != Player)
                .OrderBy(e => Vector2.Distance(PositionVector, e.PositionVector))
                .FirstOrDefault();

            if (state == ObeliskState.Idle)
            {
                if (nearestEnemy != null)
                {
                    float distance = Vector2.Distance(nearestEnemy.PositionVector, PositionVector);
                    if (distance < 300 && (LastShotTicks == null || World.Ticks - LastShotTicks >= RechargeTicks))
                    {
                        target = nearestEnemy;
                        state = ObeliskState.Charging;
                        StateChangedTicks = World.Ticks;
                        Sounds.ObeliskCharging.Play();
                    }
                }
            }
            else if (state == ObeliskState.Charging)
            {
                float distance = Vector2.Distance(nearestEnemy.PositionVector, PositionVector);
                if (!target.IsAlive || distance > 300)
                {
                    target = null;
                    state = ObeliskState.Idle;
                    StateChangedTicks = World.Ticks;
                }
                else
                {
                    if (World.Ticks - StateChangedTicks >= ChargeTicks)
                    {
                        target.Damage(1000, Warhead.Fire);
                        Sounds.ObeliskShot.Play();
                        state = ObeliskState.Firing;
                        StateChangedTicks = World.Ticks;
                        LastShotTicks = World.Ticks;
                    }
                }
            }
            else if (state == ObeliskState.Firing)
            {
                if (World.Ticks - LastShotTicks > FiringTicks)
                {
                    state = ObeliskState.Idle;
                }
            }
            else
            {
                throw new Exception("unhandled state");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (state == ObeliskState.Firing)
            {
                DrawLine(spriteBatch, PositionVector - new Vector2(2, 17), target.PositionVector);
            }
        }

        void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle = VectorHelpers.GetRotationToFace(start, end) ?? 0;

            spriteBatch.Draw(Sprites.WhitePixel.Texture,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Red, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }

        protected override SpriteFrame GetSpriteFrame()
        {
            string spriteName;
            switch (state)
            {
                case (ObeliskState.Idle):
                    spriteName = "default";
                    break;
                case (ObeliskState.Charging):
                    spriteName = "active";
                    break;
                case (ObeliskState.Firing):
                    spriteName = "firing";
                    break;
                default:
                    throw new Exception("unknown state");
            };

            if (Health < MaxHealth / 2)
            {
                spriteName = "damaged-" + spriteName;
            }

            return Sprites.Obelisk.GetFrameForAnimation(spriteName, World.Ticks - StateChangedTicks, frameRepeat: 45);
        }
    }
}
