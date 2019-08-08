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
        public static SpriteWrapper A10 { get; private set; }
        public static SpriteWrapper Bomblet { get; private set; }
        public static SpriteWrapper BulletImpact { get; private set; }
        public static SpriteWrapper CannonShot120mm { get; private set; }
        public static SpriteWrapper ExplosionBig { get; private set; }
        public static SpriteWrapper ExplosionMedium { get; private set; }
        public static SpriteWrapper ExplosionSmall { get; private set; }
        public static SpriteWrapper Harvester { get; private set; }
        public static SpriteWrapper Map { get; private set; }
        public static SpriteWrapper Napalm1 { get; private set; }
        public static SpriteWrapper Napalm2 { get; private set; }
        public static SpriteWrapper Napalm3 { get; private set; }
        public static SpriteWrapper None { get; private set; }
        public static SpriteWrapper Refinery { get; private set; }
        public static SpriteWrapper RefineryConstructing { get; private set; }
        public static SpriteWrapper Sam { get; private set; }
        public static SpriteWrapper Shadow { get; private set; }
        public static SpriteWrapper Turret { get; private set; }
        public static SpriteWrapper TurretConstructing { get; private set; }
        public static SpriteWrapper WhitePixel { get; private set; }

        public static void Load(ContentManager content)
        {
            A10 = SpriteWrapper.Unit(content.Load<Texture2D>("a10"), 48, 48, 32);
            Bomblet = SpriteWrapper.Animation(content.Load<Texture2D>("bomblet"), 7, 7, 7);
            BulletImpact = SpriteWrapper.Animation(content.Load<Texture2D>("piffpiff"), 15, 15, 8);
            ExplosionBig = SpriteWrapper.Animation(content.Load<Texture2D>("veh-hit1"), 30, 17, 14);
            ExplosionMedium = SpriteWrapper.Animation(content.Load<Texture2D>("veh-hit2"), 21, 17, 22);
            ExplosionSmall = SpriteWrapper.Animation(content.Load<Texture2D>("veh-hit3"), 19, 13, 14);
            CannonShot120mm = SpriteWrapper.Static(content.Load<Texture2D>("120mm"), 24, 24);
            Harvester = SpriteWrapper.Unit(content.Load<Texture2D>("harvester"), 48, 48, 32);
            Map = SpriteWrapper.Static(content.Load<Texture2D>("map"), 744, 744);
            Napalm1 = SpriteWrapper.Animation(content.Load<Texture2D>("napalm1"), 22, 18, 14);
            Napalm2 = SpriteWrapper.Animation(content.Load<Texture2D>("napalm2"), 41, 40, 14);
            Napalm3 = SpriteWrapper.Animation(content.Load<Texture2D>("napalm3"), 72, 72, 14);
            None = SpriteWrapper.Static(content.Load<Texture2D>("whitepixel"), 0, 0);
            Refinery = SpriteWrapper.Animation(content.Load<Texture2D>("refinery"), 72, 72, 12);
            RefineryConstructing = SpriteWrapper.Animation(content.Load<Texture2D>("refinery-build"), 72, 72, 20);
            Sam = SpriteWrapper.Complex(content.Load<Texture2D>("sam"), 48, 24, new List<SpriteFrameSet>()
                {
                    new SpriteFrameSet("closed", 0, 1),
                    new SpriteFrameSet("opening", 1, 16),
                    new SpriteFrameSet("open", 17, 32),
                    new SpriteFrameSet("closing", 50, 14),
                    new SpriteFrameSet("damaged-closed", 64, 1),
                    new SpriteFrameSet("damaged-opening", 65, 16),
                    new SpriteFrameSet("damaged-open", 81, 32),
                    new SpriteFrameSet("damaged-closing", 114, 14),
                }
            ); ; ;
            Shadow = SpriteWrapper.Static(content.Load<Texture2D>("shadow"), 48, 48);
            Turret = SpriteWrapper.Unit(content.Load<Texture2D>("gun-turret"), 24, 24, 32);
            TurretConstructing = SpriteWrapper.Animation(content.Load<Texture2D>("gun-turret-build"), 24, 24, 20);
        }
    }
}
