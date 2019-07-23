using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd
{
    static class Sprites
    {
        public static SpriteWrapper Bomblet { get; private set; }
        public static SpriteWrapper BulletImpact { get; private set; }
        public static SpriteWrapper CannonShot120mm { get; private set; }
        public static SpriteWrapper ExplosionBig { get; private set; }
        public static SpriteWrapper ExplosionMedium { get; private set; }
        public static SpriteWrapper ExplosionSmall { get; private set; }
        public static SpriteWrapper Napalm1 { get; private set; }
        public static SpriteWrapper Napalm2 { get; private set; }
        public static SpriteWrapper Napalm3 { get; private set; }
        public static SpriteWrapper None { get; private set; }
        public static SpriteWrapper Shadow { get; private set; }

        public static void Load(ContentManager content)
        {
            Bomblet = new SpriteWrapper(content.Load<Texture2D>("bomblet"), 7, 7, 7);
            BulletImpact = new SpriteWrapper(content.Load<Texture2D>("piffpiff"), 15, 15, 8);
            ExplosionBig = new SpriteWrapper(content.Load<Texture2D>("veh-hit1"), 30, 17, 14);
            ExplosionMedium = new SpriteWrapper(content.Load<Texture2D>("veh-hit2"), 21, 17, 22);
            ExplosionSmall = new SpriteWrapper(content.Load<Texture2D>("veh-hit3"), 19, 13, 14);
            CannonShot120mm = new SpriteWrapper(content.Load<Texture2D>("120mm"), 24, 24, 1);
            Napalm1 = new SpriteWrapper(content.Load<Texture2D>("napalm1"), 22, 18, 14);
            Napalm2 = new SpriteWrapper(content.Load<Texture2D>("napalm2"), 41, 40, 14);
            Napalm3 = new SpriteWrapper(content.Load<Texture2D>("napalm3"), 72, 72, 14);
            None = new SpriteWrapper(content.Load<Texture2D>("whitepixel"), 0, 0, 1);
            Shadow = new SpriteWrapper(content.Load<Texture2D>("shadow"), 48, 48, 1);
        }
    }
}
