using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TiberianStrike.Entities
{
    interface IEntity
    {
        bool IsAlive { get; }
        Player Player { get; }
        Point Position { get; }
        Vector2 PositionVector { get; }
        void Damage(int amount);
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}
