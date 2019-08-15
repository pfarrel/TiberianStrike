using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TiberianStrike
{
    struct SpriteFrame
    {
        public Texture2D SpriteSheet { get; }
        public Rectangle Coordinates { get; }

        public SpriteFrame(Texture2D spriteSheet, Rectangle coordinates)
        {
            SpriteSheet = spriteSheet;
            Coordinates = coordinates;
        }
    }
}
