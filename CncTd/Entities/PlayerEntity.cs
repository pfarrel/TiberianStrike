using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CncTd.Entities
{
    interface IEntity
    {
        bool IsAlive { get; }
        Player Player { get; }
        Point Position { get; }
        void Damage(int amount);
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}
