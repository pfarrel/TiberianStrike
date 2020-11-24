using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike
{
    public class VectorHelpers
    {
        public static float? GetRotationToFace(Vector2 source, Vector2 target)
        {
            Vector2 diff = target - source;
            diff.Normalize();
            float rotation = (float)Math.Atan2(diff.Y, diff.X);
            if (float.IsNaN(rotation))
            {
                return null;
            }
            return MathHelper.WrapAngle(rotation);
        }

        public static Vector2 GetVectorInDirection(float rotation)
        {
            Vector2 vector = new Vector2(
                (float)(Math.Cos(rotation)),
                (float)((Math.Sin(rotation)))
            );
            vector.Normalize();
            return vector;
        }

        public static Vector2 MoveInDirection(Vector2 source, float rotation, float distancePerTick)
        {
            return source + GetVectorInDirection(rotation) * distancePerTick;
        }
    }
}
