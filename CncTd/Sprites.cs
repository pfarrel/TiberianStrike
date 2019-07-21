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
        public SpriteWrapper Bomblet { get; private set; }
        public SpriteWrapper BulletImpact { get; private set; }
        public SpriteWrapper CannonImpact { get; private set; }
        public SpriteWrapper CannonShot120mm { get; private set; }
        public SpriteWrapper Napalm1 { get; private set; }
        public SpriteWrapper Napalm2 { get; private set; }
        public SpriteWrapper Napalm3 { get; private set; }

        public static Sprites Load(ContentManager content)
        {
            Sprites sprites = new Sprites()
            {
                Bomblet = new SpriteWrapper(content.Load<Texture2D>("bomblet"), 7, 7, 7),
                BulletImpact = new SpriteWrapper(content.Load<Texture2D>("piffpiff"), 15, 15, 8),
                CannonImpact = new SpriteWrapper(content.Load<Texture2D>("veh-hit3"), 19, 13, 14),
                CannonShot120mm = new SpriteWrapper(content.Load<Texture2D>("120mm"), 24, 24, 1),
                Napalm1 = new SpriteWrapper(content.Load<Texture2D>("napalm1"), 22, 18, 14),
                Napalm2 = new SpriteWrapper(content.Load<Texture2D>("napalm2"), 41, 40, 14),
                Napalm3 = new SpriteWrapper(content.Load<Texture2D>("napalm3"), 72, 72, 14),
            };

            return sprites;
        }
    }
}
