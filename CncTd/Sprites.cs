using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd
{
    class Sprites
    {
        public SpriteWrapper BulletImpact { get; set; }
        public SpriteWrapper CannonImpact { get; set; }

        public static Sprites Load(ContentManager content)
        {
            Sprites sprites = new Sprites()
            {
                BulletImpact = new SpriteWrapper(content.Load<Texture2D>("piffpiff"), 15, 15, 8),
                CannonImpact = new SpriteWrapper(content.Load<Texture2D>("veh-hit3"), 19, 13, 14),
            };

            return sprites;
        }
    }
}
