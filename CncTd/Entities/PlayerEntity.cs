using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CncTd.Entities
{
    interface IPlayerEntity
    {
        bool IsAlive { get; }
        Player Player { get; }
        Point Position { get; }
        void Damage(int amount);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}
