using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd
{
    class VectorHelpers
    {
        public static float GetRotationToFace(Vector2 source, Vector2 target)
        {
            Vector2 diff = target - source;
            Vector2 diffV = new Vector2(diff.X, diff.Y);
            diffV.Normalize();
            float rotation = (float)Math.Atan2(diffV.X, -diffV.Y);
            if (rotation == float.NaN)
            {
                rotation = 0;
            }

            return rotation;
        }

        public static Vector2 MoveInDirection(Vector2 source, float rotation, float distancePerTick)
        {
            Vector2 movement = new Vector2(
                (float)(Math.Cos(rotation - MathHelper.PiOver2)),
                (float)((Math.Sin(rotation - MathHelper.PiOver2)))
            );
            movement.Normalize();
            return source + movement * distancePerTick;
        }
    }
}
