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
        public static SpriteSheet Airfield { get; private set; }
        public static SpriteSheet Apc { get; private set; }
        public static SpriteSheet Artillery { get; private set; }
        public static SpriteSheet ArtilleryExplosion { get; private set; }
        public static SpriteSheet AdvancedCommunicationsGdi { get; private set; }
        public static SpriteSheet Bike { get; private set; }
        public static SpriteSheet Bomblet { get; private set; }
        public static SpriteSheet Buggy { get; private set; }
        public static SpriteSheet BulletImpact { get; private set; }
        public static SpriteSheet BulletImpactSmall { get; private set; }
        public static SpriteSheet CannonShot120mm { get; private set; }
        public static SpriteSheet ChainlinkFence { get; private set; }
        public static SpriteSheet ChemTrooper { get; private set; }
        public static SpriteSheet Chinook { get; private set; }
        public static SpriteSheet ConcreteWall { get; private set; }
        public static SpriteSheet ConstructionYard { get; private set; }
        public static SpriteSheet Dragon { get; private set; }
        public static SpriteSheet Engineer { get; private set; }
        public static SpriteSheet ExplosionBig { get; private set; }
        public static SpriteSheet ExplosionMedium { get; private set; }
        public static SpriteSheet ExplosionSmall { get; private set; }
        public static SpriteSheet FlameInfantry { get; private set; }
        public static SpriteSheet FlameInfantryRot { get; private set; }
        public static SpriteSheet FlameTank { get; private set; }
        public static SpriteSheet Frag1 { get; private set; }
        public static SpriteSheet Frag3 { get; private set; }
        public static SpriteSheet GrenadeInfantry { get; private set; }
        public static SpriteSheet GrenadeInfantryRot { get; private set; }
        public static SpriteSheet HandOfNod { get; private set; }
        public static SpriteSheet Harvester { get; private set; }
        public static SpriteSheet IonSfx { get; private set; }
        public static SpriteSheet Map { get; private set; }
        public static SpriteSheet MobileSam { get; private set; }
        public static SpriteSheet MuzzleFlash { get; private set; }
        public static SpriteSheet Napalm1 { get; private set; }
        public static SpriteSheet Napalm2 { get; private set; }
        public static SpriteSheet Napalm3 { get; private set; }
        public static SpriteSheet None { get; private set; }
        public static SpriteSheet Obelisk { get; private set; }
        public static SpriteSheet Patriot { get; private set; }
        public static SpriteSheet Rambo { get; private set; }
        public static SpriteSheet Refinery { get; private set; }
        public static SpriteSheet RefineryConstructing { get; private set; }
        public static SpriteSheet RifleInfantry { get; private set; }
        public static SpriteSheet RifleInfantryRot { get; private set; }
        public static SpriteSheet RocketInfantry { get; private set; }
        public static SpriteSheet RocketInfantryRot { get; private set; }
        public static SpriteSheet Sam { get; private set; }
        public static SpriteSheet SamFire { get; private set; }
        public static SpriteSheet Sandbags { get; private set; }
        public static SpriteSheet Shadow { get; private set; }
        public static SpriteSheet StealthTank { get; private set; }
        public static SpriteSheet TempleOfNod { get; private set; }
        public static SpriteSheet Turret { get; private set; }
        public static SpriteSheet TurretConstructing { get; private set; }
        public static SpriteSheet WhitePixel { get; private set; }

        public static void Load(ContentManager content)
        {
            A10 = SpriteSheet.Unit(content.Load<Texture2D>("a10"), 48, 48, 32);
            Airfield = SpriteSheet.Building(content.Load<Texture2D>("afld"), 96, 48, 0, 16, 32);
            Apc = SpriteSheet.Unit(content.Load<Texture2D>("apc"), 24, 24, 32);
            AdvancedCommunicationsGdi = SpriteSheet.Complex(content.Load<Texture2D>("eye-gdi"), 48, 48,
                new SpriteSequence("default", 0) { Length = 16 },
                new SpriteSequence("damaged", 16) { Length = 16 },
                new SpriteSequence("destroyed", 32) { Length = 1 }
            );
            AdvancedCommunicationsGdi = SpriteSheet.Complex(content.Load<Texture2D>("eye-nod"), 48, 48,
                new SpriteSequence("default", 0) { Length = 16 },
                new SpriteSequence("damaged", 16) { Length = 16 },
                new SpriteSequence("destroyed", 32) { Length = 1 }
            );
            Artillery = SpriteSheet.Unit(content.Load<Texture2D>("arty"), 24, 24, 32);
            ArtilleryExplosion = SpriteSheet.Animation(content.Load<Texture2D>("art-exp1"), 41, 35, 22);
            Bike = SpriteSheet.Unit(content.Load<Texture2D>("bike"), 24, 24, 32);
            Bomblet = SpriteSheet.Animation(content.Load<Texture2D>("bomblet"), 7, 7, 7);
            Buggy = SpriteSheet.Unit(content.Load<Texture2D>("bggy"), 24, 24, 32);
            BulletImpact = SpriteSheet.Animation(content.Load<Texture2D>("piffpiff"), 15, 15, 8);
            BulletImpactSmall = SpriteSheet.Animation(content.Load<Texture2D>("piff"), 9, 13, 4);
            CannonShot120mm = SpriteSheet.Static(content.Load<Texture2D>("120mm"), 24, 24);
            ChainlinkFence = SpriteSheet.Wall(content.Load<Texture2D>("cycl"), 24, 24);
            ChemTrooper = SpriteSheet.Complex(content.Load<Texture2D>("e5"), 50, 39,
                new SpriteSequence("stand", 0) { Facings = 8 },
                new SpriteSequence("stand2", 8) { Facings = 8 },
                new SpriteSequence("run", 16) { Facings = 8, Length = 6 },
                new SpriteSequence("shoot", 64) { Facings = 8, Length = 16 },
                new SpriteSequence("liedown", 192) { Facings = 8, Length = 2 },
                new SpriteSequence("standup", 240) { Facings = 8, Length = 2 },
                new SpriteSequence("prone-stand", 208) { Facings = 8 },
                new SpriteSequence("prone-stand2", 208) { Facings = 8 },
                new SpriteSequence("prone-run", 208) { Facings = 8, Length = 4 },
                new SpriteSequence("prone-shoot", 256) { Facings = 8, Length = 16 },
                new SpriteSequence("idle1", 384) { Length = 16 },
                new SpriteSequence("idle2", 400) { Length = 16 },
                new SpriteSequence("cheer", 588) { Facings = 8, Length = 3 },
                new SpriteSequence("die1", 509) { Length = 9 },
                new SpriteSequence("die2", 518) { Length = 8 },
                new SpriteSequence("die3", 526) { Length = 8 },
                new SpriteSequence("die4", 534) { Length = 12 },
                new SpriteSequence("die5", 546) { Length = 18 },
                new SpriteSequence("die6", 494) { Length = 10 }
            );
            Chinook = SpriteSheet.Unit(content.Load<Texture2D>("tran"), 48, 48, 32);
            ConcreteWall = SpriteSheet.Wall(content.Load<Texture2D>("brik"), 24, 24);
            ConstructionYard = SpriteSheet.Building(content.Load<Texture2D>("fact"), 72, 48, 0, 25, 49);
            Dragon = SpriteSheet.Unit(content.Load<Texture2D>("dragon"), 15, 15, 32);
            Engineer = SpriteSheet.Complex(content.Load<Texture2D>("e6"), 50, 39,
                new SpriteSequence("stand", 0) { Facings = 8 },
                new SpriteSequence("stand2", 8) { Facings = 8 },
                new SpriteSequence("run", 16) { Facings = 8, Length = 6 },
                new SpriteSequence("liedown", 66) { Facings = 8, Length = 2 },
                new SpriteSequence("standup", 114) { Facings = 8, Length = 2 },
                new SpriteSequence("prone-stand", 82) { Facings = 8 },
                new SpriteSequence("prone-stand2", 82) { Facings = 8 },
                new SpriteSequence("prone-run", 82) { Facings = 8, Length = 4 },
                new SpriteSequence("idle1", 114) { Length = 6 },
                new SpriteSequence("idle2", 200) { Length = 6 },
                new SpriteSequence("cheer", 200) { Facings = 8, Length = 3 },
                new SpriteSequence("die1", 146) { Length = 8 },
                new SpriteSequence("die2", 154) { Length = 8 },
                new SpriteSequence("die3", 162) { Length = 8 },
                new SpriteSequence("die4", 170) { Length = 12 },
                new SpriteSequence("die5", 182) { Length = 18 },
                new SpriteSequence("die6", 130) { Length = 4 }
            );
            ExplosionBig = SpriteSheet.Animation(content.Load<Texture2D>("veh-hit1"), 30, 17, 14);
            ExplosionMedium = SpriteSheet.Animation(content.Load<Texture2D>("veh-hit2"), 21, 17, 22);
            ExplosionSmall = SpriteSheet.Animation(content.Load<Texture2D>("veh-hit3"), 19, 13, 14);
            FlameTank = SpriteSheet.Unit(content.Load<Texture2D>("ftnk"), 24, 24, 32);
            FlameInfantry = SpriteSheet.Complex(content.Load<Texture2D>("e4"), 50, 39,
                new SpriteSequence("stand", 0) { Facings = 8 },
                new SpriteSequence("stand2", 8) { Facings = 8 },
                new SpriteSequence("run", 16) { Facings = 8, Length = 6 },
                new SpriteSequence("shoot", 64) { Facings = 8, Length = 16 },
                new SpriteSequence("liedown", 192) { Facings = 8, Length = 2 },
                new SpriteSequence("standup", 240) { Facings = 8, Length = 2 },
                new SpriteSequence("prone-stand", 208) { Facings = 8 },
                new SpriteSequence("prone-stand2", 208) { Facings = 8 },
                new SpriteSequence("prone-run", 208) { Facings = 8, Length = 4 },
                new SpriteSequence("prone-shoot", 256) { Facings = 8, Length = 16 },
                new SpriteSequence("idle1", 384) { Length = 16 },
                new SpriteSequence("idle2", 400) { Length = 16 },
                new SpriteSequence("cheer", 588) { Facings = 8, Length = 3 },
                new SpriteSequence("die1", 509) { Length = 9 },
                new SpriteSequence("die2", 518) { Length = 8 },
                new SpriteSequence("die3", 526) { Length = 8 },
                new SpriteSequence("die4", 534) { Length = 12 },
                new SpriteSequence("die5", 546) { Length = 18 },
                new SpriteSequence("die6", 494) { Length = 10 }
            );
            Frag1 = SpriteSheet.Animation(content.Load<Texture2D>("frag1"), 45, 33, 14);
            Frag3 = SpriteSheet.Animation(content.Load<Texture2D>("frag3"), 41, 28, 22);
            GrenadeInfantry = SpriteSheet.Complex(content.Load<Texture2D>("e2"), 50, 39,
                new SpriteSequence("stand", 0) { Facings = 8 },
                new SpriteSequence("stand2", 8) { Facings = 8 },
                new SpriteSequence("run", 16) { Facings = 8, Length = 6 },
                new SpriteSequence("shoot", 64) { Facings = 8, Length = 20 },
                new SpriteSequence("liedown", 224) { Facings = 8, Length = 2 },
                new SpriteSequence("standup", 272) { Facings = 8, Length = 2 },
                new SpriteSequence("prone-stand", 240) { Facings = 8 },
                new SpriteSequence("prone-stand2", 240) { Facings = 8 },
                new SpriteSequence("prone-run", 240) { Facings = 8, Length = 8 },
                new SpriteSequence("prone-shoot", 288) { Facings = 8, Length = 20 },
                new SpriteSequence("idle1", 384) { Length = 12 },
                new SpriteSequence("idle2", 400) { Length = 14 },
                new SpriteSequence("cheer", 588) { Facings = 8, Length = 3 },
                new SpriteSequence("die1", 509) { Length = 9 },
                new SpriteSequence("die2", 518) { Length = 8 },
                new SpriteSequence("die3", 526) { Length = 8 },
                new SpriteSequence("die4", 534) { Length = 12 },
                new SpriteSequence("die5", 546) { Length = 18 },
                new SpriteSequence("die6", 494) { Length = 11 }
            );
            HandOfNod = SpriteSheet.Building(content.Load<Texture2D>("hand"), 48, 72);
            Harvester = SpriteSheet.Unit(content.Load<Texture2D>("harvester"), 48, 48, 32);
            IonSfx = SpriteSheet.Animation(content.Load<Texture2D>("ionsfx"), 72, 191, 15);
            Map = SpriteSheet.Static(content.Load<Texture2D>("map"), 744, 744);
            MobileSam = SpriteSheet.Unit(content.Load<Texture2D>("msam"), 24, 24, 32);
            MuzzleFlash = SpriteSheet.Complex(content.Load<Texture2D>("minigun"), 18, 17,
                new SpriteSequence("default", 0) { Facings = 8, Length = 6 }
            );
            Napalm1 = SpriteSheet.Animation(content.Load<Texture2D>("napalm1"), 22, 18, 14);
            Napalm2 = SpriteSheet.Animation(content.Load<Texture2D>("napalm2"), 41, 40, 14);
            Napalm3 = SpriteSheet.Animation(content.Load<Texture2D>("napalm3"), 72, 72, 14);
            None = SpriteSheet.Static(content.Load<Texture2D>("whitepixel"), 0, 0);
            Obelisk = SpriteSheet.Complex(content.Load<Texture2D>("obli"), 24, 48,
                new SpriteSequence("default", 0),
                new SpriteSequence("damaged-default", 4),
                new SpriteSequence("active", 0) { Length = 4 },
                new SpriteSequence("damaged-active", 4) { Length = 4 },
                new SpriteSequence("firing", 3),
                new SpriteSequence("damaged-firing", 7),
                new SpriteSequence("dead", 8)
            );
            Patriot = SpriteSheet.Unit(content.Load<Texture2D>("patriot"), 26, 15, 32);
            Rambo = SpriteSheet.Complex(content.Load<Texture2D>("rmbo"), 50, 39,
                new SpriteSequence("stand", 0) { Facings = 8 },
                new SpriteSequence("stand2", 8) { Facings = 8 },
                new SpriteSequence("run", 16) { Facings = 8, Length = 6 },
                new SpriteSequence("shoot", 64) { Facings = 8, Length = 4 },
                new SpriteSequence("liedown", 96) { Facings = 8, Length = 2 },
                new SpriteSequence("standup", 144) { Facings = 8, Length = 2 },
                new SpriteSequence("prone-stand", 112) { Facings = 8 },
                new SpriteSequence("prone-stand2", 112) { Facings = 8 },
                new SpriteSequence("prone-run", 112) { Facings = 8, Length = 4 },
                new SpriteSequence("prone-shoot", 160) { Facings = 8, Length = 4 },
                new SpriteSequence("idle1", 192) { Length = 16 },
                new SpriteSequence("idle2", 208) { Length = 16 },
                new SpriteSequence("idle3", 224) { Length = 15 },
                new SpriteSequence("cheer", 396) { Facings = 8, Length = 3 },
                new SpriteSequence("die1", 318) { Length = 8 },
                new SpriteSequence("die2", 326) { Length = 8 },
                new SpriteSequence("die3", 334) { Length = 8 },
                new SpriteSequence("die4", 342) { Length = 12 },
                new SpriteSequence("die5", 354) { Length = 18 },
                new SpriteSequence("die6", 366) { Length = 4 }
            );
            Refinery = SpriteSheet.Animation(content.Load<Texture2D>("refinery"), 72, 72, 12);
            RefineryConstructing = SpriteSheet.Animation(content.Load<Texture2D>("refinery-build"), 72, 72, 20);
            RifleInfantry = SpriteSheet.Complex(content.Load<Texture2D>("e1"), 50, 39,
                new SpriteSequence("stand", 0) { Facings = 8 },
                new SpriteSequence("stand2", 8) { Facings = 8 },
                new SpriteSequence("run", 16) { Facings = 8, Length = 6 },
                new SpriteSequence("shoot", 64) { Facings = 8, Length = 8 },
                new SpriteSequence("liedown", 128) { Facings = 8, Length = 2 },
                new SpriteSequence("standup", 176) { Facings = 8, Length = 2 },
                new SpriteSequence("prone-stand", 144) { Facings = 8 },
                new SpriteSequence("prone-stand2", 144) { Facings = 8 },
                new SpriteSequence("prone-run", 144) { Facings = 8, Length = 8 },
                new SpriteSequence("prone-shoot", 192) { Facings = 8, Length = 10 },
                new SpriteSequence("idle1", 257) { Length = 15 },
                new SpriteSequence("idle2", 272) { Length = 16 },
                new SpriteSequence("cheer", 460) { Facings = 8, Length = 3 },
                new SpriteSequence("die1", 381) { Length = 9 },
                new SpriteSequence("die2", 390) { Length = 8 },
                new SpriteSequence("die3", 398) { Length = 8 },
                new SpriteSequence("die4", 406) { Length = 12 },
                new SpriteSequence("die5", 418) { Length = 18 },
                new SpriteSequence("die6", 366) { Length = 11 }
            );
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
                new SpriteSequence("damaged-closed-idle", 64),
                new SpriteSequence("damaged-opening", 65) { Length = 16 },
                new SpriteSequence("damaged-idle", 81) { Facings = 32 },
                new SpriteSequence("damaged-closing", 114) { Length = 14 }
            );
            SamFire = SpriteSheet.Complex(content.Load<Texture2D>("samfire"), 55, 35,
                new SpriteSequence("default", 0) { Facings = 8, Length = 18 }
            );
            Sandbags = SpriteSheet.Wall(content.Load<Texture2D>("sbag"), 24, 24);
            Shadow = SpriteSheet.Static(content.Load<Texture2D>("shadow"), 48, 48);
            StealthTank = SpriteSheet.Unit(content.Load<Texture2D>("stnk"), 24, 24, 32);
            TempleOfNod = SpriteSheet.Building(content.Load<Texture2D>("tmpl"), 72, 72, 0, 6, 11);
            Turret = SpriteSheet.Unit(content.Load<Texture2D>("gun-turret"), 24, 24, 32);
            TurretConstructing = SpriteSheet.Animation(content.Load<Texture2D>("gun-turret-build"), 24, 24, 20);
            WhitePixel = SpriteSheet.Static(content.Load<Texture2D>("whitepixel"), 1, 1);
        }
    }
}
