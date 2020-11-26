using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiberianStrike.Entities;

namespace TiberianStrike
{
    class RadarView
    {
        const int Width = 336;
        const int Height = 336;
        const int Padding = (TsGame.SidebarWidth - Width) / 2;
        const int StartWidth = (TsGame.ResolutionWidth - TsGame.SidebarWidth) + Padding;
        const int StartHeight = Padding;

        public static void Draw(SpriteBatch spriteBatch, World world, Camera camera)
        {
            spriteBatch.Draw(Sprites.WhitePixel.Texture, new Rectangle(TsGame.ResolutionWidth - TsGame.SidebarWidth, 0, TsGame.SidebarWidth, TsGame.ResolutionHeight), Color.DimGray);

            spriteBatch.Draw(Sprites.Map.Texture, null, new Rectangle(StartWidth - 1, StartHeight, Width + 1, Height), null, null, 0, null, Color.White, SpriteEffects.None, 0f);
            
            foreach (BaseEntity entity in world.Entities)
            {
                SpriteHelper.DrawPoint(spriteBatch, TranslateToMap(entity.Position, world), entity is AbstractWall ? Color.DarkGray : entity.Player == Player.One ? Color.Green : Color.Red, 0, 8);
            }

            for (int y = 0; y < Height / 2; y++)
            {
                for (int x = 0; x < Width / 2; x++)
                {
                    Point worldPoint = new Point(x * world.Width * 2 / Width, y * world.Height * 2 / Height);
                    if (!world.IsExplored(worldPoint))
                    {
                        Point mapPoint = TranslateToMap(worldPoint, world);
                        SpriteHelper.DrawPoint(spriteBatch, mapPoint, Color.Black, 0, 3);
                    }
                }
            }

            SpriteHelper.DrawRectangle(spriteBatch, new Rectangle(StartWidth - 2, StartHeight, Width + 2, Height), Color.White, 0f);
            Rectangle cameraBounds = camera.Bounds;
            Rectangle translated = new Rectangle(
                StartWidth + cameraBounds.X * Width / world.Width,
                StartHeight + cameraBounds.Y * Height / world.Height,
                cameraBounds.Width * Width / world.Width,
                cameraBounds.Height * Height / world.Height
            );
            SpriteHelper.DrawRectangle(spriteBatch, translated, Color.White, 0f);

        }

        private static Point TranslateToMap(Point source, World world)
        {
            return new Point(StartWidth + source.X * Width / world.Width, StartHeight + source.Y * Height / world.Height);
        }


    }
}
