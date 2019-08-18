using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike
{
    static class Sprites
    {
        public static SpriteSheet A10 { get; private set; }
        public static SpriteSheet Bomblet { get; private set; }
        public static SpriteSheet BulletImpact { get; private set; }
        public static SpriteSheet CannonShot120mm { get; private set; }
        public static SpriteSheet Dragon { get; private set; }
        public static SpriteSheet ExplosionBig { get; private set; }
        public static SpriteSheet ExplosionMedium { get; private set; }
        public static SpriteSheet ExplosionSmall { get; private set; }
        public static SpriteSheet Harvester { get; private set; }
        public static SpriteSheet Map { get; private set; }
        public static SpriteSheet MuzzleFlash { get; private set; }
        public static SpriteSheet Napalm1 { get; private set; }
        public static SpriteSheet Napalm2 { get; private set; }
        public static SpriteSheet Napalm3 { get; private set; }
        public static SpriteSheet None { get; private set; }
        public static SpriteSheet Patriot { get; private set; }
        public static SpriteSheet Refinery { get; private set; }
        public static SpriteSheet RefineryConstructing { get; private set; }
        public static SpriteSheet RocketInfantry { get; private set; }
        public static SpriteSheet RocketInfantryRot { get; private set; }
        public static SpriteSheet Sam { get; private set; }
        public static SpriteSheet SamFire { get; private set; }
        public static SpriteSheet Shadow { get; private set; }
        public static SpriteSheet Turret { get; private set; }
        public static SpriteSheet TurretConstructing { get; private set; }
        public static SpriteSheet WhitePixel { get; private set; }

        public static void Load(ContentManager content)
        {
            A10 = SpriteSheet.Unit(content.Load<Texture2D>("a10"), 48, 48, 32);
            Bomblet = SpriteSheet.Animation(content.Load<Texture2D>("bomblet"), 7, 7, 7);
            BulletImpact = SpriteSheet.Animation(content.Load<Texture2D>("piffpiff"), 15, 15, 8);
            CannonShot120mm = SpriteSheet.Static(content.Load<Texture2D>("120mm"), 24, 24);
            Dragon = SpriteSheet.Unit(content.Load<Texture2D>("dragon"), 15, 15, 32);
            ExplosionBig = SpriteSheet.Animation(content.Load<Texture2D>("veh-hit1"), 30, 17, 14);
            ExplosionMedium = SpriteSheet.Animation(content.Load<Texture2D>("veh-hit2"), 21, 17, 22);
            ExplosionSmall = SpriteSheet.Animation(content.Load<Texture2D>("veh-hit3"), 19, 13, 14);
            Harvester = SpriteSheet.Unit(content.Load<Texture2D>("harvester"), 48, 48, 32);
            Map = SpriteSheet.Static(content.Load<Texture2D>("map"), 744, 744);
            MuzzleFlash = SpriteSheet.Complex(content.Load<Texture2D>("minigun"), 18, 17,
                new SpriteSequence("default", 0) { Facings = 8, Length = 6 }
            );
            Napalm1 = SpriteSheet.Animation(content.Load<Texture2D>("napalm1"), 22, 18, 14);
            Napalm2 = SpriteSheet.Animation(content.Load<Texture2D>("napalm2"), 41, 40, 14);
            Napalm3 = SpriteSheet.Animation(content.Load<Texture2D>("napalm3"), 72, 72, 14);
            None = SpriteSheet.Static(content.Load<Texture2D>("whitepixel"), 0, 0);
            Patriot = SpriteSheet.Unit(content.Load<Texture2D>("patriot"), 26, 15, 32);
            Refinery = SpriteSheet.Animation(content.Load<Texture2D>("refinery"), 72, 72, 12);
            RefineryConstructing = SpriteSheet.Animation(content.Load<Texture2D>("refinery-build"), 72, 72, 20);
            RocketInfantry = SpriteSheet.Complex(content.Load<Texture2D>("e3"), 50, 39,
                new SpriteSequence("stand", 0)          { Facings = 8 },
                new SpriteSequence("stand2", 8)         { Facings = 8 },
                new SpriteSequence("run", 16)           { Facings = 8, Length = 6 },
                new SpriteSequence("shoot", 64)         { Facings = 8, Length = 8 },
                new SpriteSequence("liedown", 128)      { Facings = 8, Length = 2 },
                new SpriteSequence("standup", 176)      { Facings = 8, Length = 2 },
                new SpriteSequence("prone-stand", 144)  { Facings = 8 },
                new SpriteSequence("prone-stand2", 144) { Facings = 8 },
                new SpriteSequence("prone-run", 144)    { Facings = 8, Length = 8 },
                new SpriteSequence("prone-shoot", 192)  { Facings = 8, Length = 10 },
                new SpriteSequence("idle1", 274)        { Length = 12 },
                new SpriteSequence("idle2", 289)        { Length = 14 },
                new SpriteSequence("cheer", 476)        { Facings = 8, Length = 3 },
                new SpriteSequence("die1", 397)         { Length = 9 },
                new SpriteSequence("die2", 406)         { Length = 8 },
                new SpriteSequence("die3", 414)         { Length = 8 },
                new SpriteSequence("die4", 422)         { Length = 12 },
                new SpriteSequence("die5", 434)         { Length = 18 },
                new SpriteSequence("die6", 382)         { Length = 11 }
            );
            RocketInfantryRot = SpriteSheet.Complex(content.Load<Texture2D>("e3rot"), 39, 31,
                new SpriteSequence("default", 0) { Length = 4 }
                // There are other missing animations here, don't need for now.
            );
            Sam = SpriteSheet.Complex(content.Load<Texture2D>("sam"), 48, 24, 
                new SpriteSequence("closed-idle", 0),
                new SpriteSequence("opening", 1) { Length = 16 },
                new SpriteSequence("idle", 17) { Facings = 32 },
                new SpriteSequence("closing", 50) { Length = 14 },
                new SpriteSequence("damaged-closed", 64),
                new SpriteSequence("damaged-opening", 65) { Length = 16 },
                new SpriteSequence("damaged-idle", 81) { Facings = 32 },
                new SpriteSequence("damaged-closing", 114) { Length = 14 }
            );
            SamFire = SpriteSheet.Complex(content.Load<Texture2D>("samfire"), 55, 35,
                new SpriteSequence("default", 0) { Facings = 8, Length = 18 }
            );
            Shadow = SpriteSheet.Static(content.Load<Texture2D>("shadow"), 48, 48);
            Turret = SpriteSheet.Unit(content.Load<Texture2D>("gun-turret"), 24, 24, 32);
            TurretConstructing = SpriteSheet.Animation(content.Load<Texture2D>("gun-turret-build"), 24, 24, 20);
        }
    }
}
