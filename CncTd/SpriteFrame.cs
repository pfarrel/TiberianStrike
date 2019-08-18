using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TiberianStrike
{
    struct SpriteFrame
    {
        public Texture2D Texture { get; }
        public Rectangle Coordinates { get; }

        public SpriteFrame(Texture2D texture, Rectangle coordinates)
        {
            Texture = texture;
            Coordinates = coordinates;
        }
    }
}
