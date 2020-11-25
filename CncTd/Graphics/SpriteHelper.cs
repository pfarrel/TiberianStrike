using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike
{
    class SpriteHelper
    {
        // unused manual remap of turret sprite pixels.
        // need to replace with palette based recolor.
        public void recolor(ContentManager content)
        {
            Texture2D refinerySprite = content.Load<Texture2D>("gun-turret");
            Color[] data = new Color[refinerySprite.Width * refinerySprite.Height];
            refinerySprite.GetData(data);
            Color source = new Color(246, 214, 121);
            Color source2 = new Color(222, 190, 105);
            Color source3 = new Color(178, 149, 80);
            Color source4 = new Color(170, 153, 85);
            Color source5 = new Color(194, 174, 97);
            Color source6 = new Color(198, 170, 93);
            Color source7 = new Color(145, 137, 76);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == source || data[i] == source2 || data[i] == source3 || data[i] == source4 || data[i] == source5 || data[i] == source6 || data[i] == source7)
                {
                    data[i] = Color.Red;
                }
            }
            refinerySprite.SetData(data);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float zOrder)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle = VectorHelpers.GetRotationToFace(start, end) ?? 0;

            spriteBatch.Draw(
                Sprites.WhitePixel.Texture,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                color, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                zOrder);
        }

        public static void DrawAxisOrientedLine(SpriteBatch spriteBatch, Point start, Point end, Color color, float zOrder)
        {
            spriteBatch.Draw(
                Sprites.WhitePixel.Texture,
                new Rectangle(
                    Math.Min(start.X, end.X),
                    Math.Min(start.Y, end.Y),
                    Math.Abs(end.X - start.X),
                    Math.Abs(end.Y - start.Y)
                ),
                null,
                color,
                0,  
                new Vector2(0, 0),
                SpriteEffects.None,
                zOrder
            );
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, float zOrder)
        {
            DrawAxisOrientedLine(spriteBatch, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X + rectangle.Width, rectangle.Y + 1), color, zOrder); // top
            DrawAxisOrientedLine(spriteBatch, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X + 1, rectangle.Y + rectangle.Height), color, zOrder); // left
            DrawAxisOrientedLine(spriteBatch, new Point(rectangle.X, rectangle.Y + rectangle.Height - 1), new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height), color, zOrder); // bottom
            DrawAxisOrientedLine(spriteBatch, new Point(rectangle.X + rectangle.Width - 1, rectangle.Y), new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height), color, zOrder); // right
        }

        public static void DrawPoint(SpriteBatch spriteBatch, Point point, Color color, float zOrder)
        {
            spriteBatch.Draw(
                Sprites.WhitePixel.Texture,
                new Rectangle(
                    point.X,
                    point.Y,
                    1,
                    1
                ),
                null,
                color,
                0,
                new Vector2(0, 0),
                SpriteEffects.None,
                zOrder
            );
        }
    }
}
