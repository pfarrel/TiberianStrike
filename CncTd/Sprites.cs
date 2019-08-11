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
        public static SpriteWrapper Dragon { get; private set; }
        public static SpriteWrapper ExplosionBig { get; private set; }
        public static SpriteWrapper ExplosionMedium { get; private set; }
        public static SpriteWrapper ExplosionSmall { get; private set; }
        public static SpriteWrapper Harvester { get; private set; }
        public static SpriteWrapper Map { get; private set; }
        public static SpriteWrapper MuzzleFlash { get; private set; }
        public static SpriteWrapper Napalm1 { get; private set; }
        public static SpriteWrapper Napalm2 { get; private set; }
        public static SpriteWrapper Napalm3 { get; private set; }
        public static SpriteWrapper None { get; private set; }
        public static SpriteWrapper Patriot { get; private set; }
        public static SpriteWrapper Refinery { get; private set; }
        public static SpriteWrapper RefineryConstructing { get; private set; }
        public static SpriteWrapper RocketInfantry { get; private set; }
        public static SpriteWrapper RocketInfantryRot { get; private set; }
        public static SpriteWrapper Sam { get; private set; }
        public static SpriteWrapper SamFire { get; private set; }
        public static SpriteWrapper Shadow { get; private set; }
        public static SpriteWrapper Turret { get; private set; }
        public static SpriteWrapper TurretConstructing { get; private set; }
        public static SpriteWrapper WhitePixel { get; private set; }

        public static void Load(ContentManager content)
        {
            A10 = SpriteWrapper.Unit(content.Load<Texture2D>("a10"), 48, 48, 32);
            Bomblet = SpriteWrapper.Animation(content.Load<Texture2D>("bomblet"), 7, 7, 7);
            BulletImpact = SpriteWrapper.Animation(content.Load<Texture2D>("piffpiff"), 15, 15, 8);
            CannonShot120mm = SpriteWrapper.Static(content.Load<Texture2D>("120mm"), 24, 24);
            Dragon = SpriteWrapper.Unit(content.Load<Texture2D>("dragon"), 24, 24, 32);
            ExplosionBig = SpriteWrapper.Animation(content.Load<Texture2D>("veh-hit1"), 30, 17, 14);
            ExplosionMedium = SpriteWrapper.Animation(content.Load<Texture2D>("veh-hit2"), 21, 17, 22);
            ExplosionSmall = SpriteWrapper.Animation(content.Load<Texture2D>("veh-hit3"), 19, 13, 14);
            Harvester = SpriteWrapper.Unit(content.Load<Texture2D>("harvester"), 48, 48, 32);
            Map = SpriteWrapper.Static(content.Load<Texture2D>("map"), 744, 744);
            MuzzleFlash = SpriteWrapper.Complex(content.Load<Texture2D>("minigun"), 18, 17,
                new SpriteFrameSet("default", 0) { Facings = 8, Length = 6 }
            );
            Napalm1 = SpriteWrapper.Animation(content.Load<Texture2D>("napalm1"), 22, 18, 14);
            Napalm2 = SpriteWrapper.Animation(content.Load<Texture2D>("napalm2"), 41, 40, 14);
            Napalm3 = SpriteWrapper.Animation(content.Load<Texture2D>("napalm3"), 72, 72, 14);
            None = SpriteWrapper.Static(content.Load<Texture2D>("whitepixel"), 0, 0);
            Patriot = SpriteWrapper.Unit(content.Load<Texture2D>("patriot"), 26, 15, 32);
            Refinery = SpriteWrapper.Animation(content.Load<Texture2D>("refinery"), 72, 72, 12);
            RefineryConstructing = SpriteWrapper.Animation(content.Load<Texture2D>("refinery-build"), 72, 72, 20);
            RocketInfantry = SpriteWrapper.Complex(content.Load<Texture2D>("e3"), 50, 39,
                new SpriteFrameSet("stand", 0)          { Facings = 8 },
                new SpriteFrameSet("stand2", 8)         { Facings = 8 },
                new SpriteFrameSet("run", 16)           { Facings = 8, Length = 6 },
                new SpriteFrameSet("shoot", 64)         { Facings = 8, Length = 8 },
                new SpriteFrameSet("liedown", 128)      { Facings = 8, Length = 2 },
                new SpriteFrameSet("standup", 176)      { Facings = 8, Length = 2 },
                new SpriteFrameSet("prone-stand", 144)  { Facings = 8 },
                new SpriteFrameSet("prone-stand2", 144) { Facings = 8 },
                new SpriteFrameSet("prone-run", 144)    { Facings = 8, Length = 8 },
                new SpriteFrameSet("prone-shoot", 192)  { Facings = 8, Length = 10 },
                new SpriteFrameSet("idle1", 274)        { Length = 12 },
                new SpriteFrameSet("idle2", 289)        { Length = 14 },
                new SpriteFrameSet("cheer", 476)        { Facings = 8, Length = 3 },
                new SpriteFrameSet("die1", 397)         { Length = 9 },
                new SpriteFrameSet("die2", 406)         { Length = 8 },
                new SpriteFrameSet("die3", 414)         { Length = 8 },
                new SpriteFrameSet("die4", 422)         { Length = 12 },
                new SpriteFrameSet("die5", 434)         { Length = 18 },
                new SpriteFrameSet("die6", 382)         { Length = 11 }
            );
            RocketInfantryRot = SpriteWrapper.Complex(content.Load<Texture2D>("e3rot"), 39, 31,
                new SpriteFrameSet("default", 0) { Length = 4 }
                // There are other missing animations here, don't need for now.
            );
            Sam = SpriteWrapper.Complex(content.Load<Texture2D>("sam"), 48, 24, 
                new SpriteFrameSet("closed-idle", 0),
                new SpriteFrameSet("opening", 1) { Length = 16 },
                new SpriteFrameSet("idle", 17) { Facings = 32 },
                new SpriteFrameSet("closing", 50) { Length = 14 },
                new SpriteFrameSet("damaged-closed", 64),
                new SpriteFrameSet("damaged-opening", 65) { Length = 16 },
                new SpriteFrameSet("damaged-idle", 81) { Facings = 32 },
                new SpriteFrameSet("damaged-closing", 114) { Length = 14 }
            );
            SamFire = SpriteWrapper.Complex(content.Load<Texture2D>("samfire"), 55, 35,
                new SpriteFrameSet("default", 0) { Facings = 8, Length = 18 }
            );
            Shadow = SpriteWrapper.Static(content.Load<Texture2D>("shadow"), 48, 48);
            Turret = SpriteWrapper.Unit(content.Load<Texture2D>("gun-turret"), 24, 24, 32);
            TurretConstructing = SpriteWrapper.Animation(content.Load<Texture2D>("gun-turret-build"), 24, 24, 20);
        }
    }
}
