using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike
{
    class Sounds
    {
        public static SoundEffect Bazooka { get; private set; }
        public static SoundEffect CannonShot { get; private set; }
        public static SoundEffect Explosion { get; private set; }
        public static SoundEffect FireExplosion { get; private set; }
        public static SoundEffect HarvesterExplosion { get; private set; }
        public static SoundEffect HeavyMachineGun { get; private set; }
        public static SoundEffect ObeliskCharging { get; private set; }
        public static SoundEffect ObeliskShot { get; private set; }
        public static SoundEffect Rocket1 { get; private set; }
        public static SoundEffect Rocket2 { get; private set; }

        public static SoundEffect InfantryDeath1 { get; private set; }
        public static SoundEffect InfantryDeath2 { get; private set; }
        public static SoundEffect InfantryDeath3 { get; private set; }
        public static SoundEffect InfantryDeath4 { get; private set; }

        public static SoundEffect MissionAccomplished { get; private set; }
        public static SoundEffect MissionFailed { get; private set; }

        public static SoundEffect Void { get; private set; }

        public static void Load(ContentManager content)
        {
            Bazooka = content.Load<SoundEffect>("Sounds/bazook1");
            CannonShot = content.Load<SoundEffect>("Sounds/tnkfire4");
            Explosion = content.Load<SoundEffect>("Sounds/xplos");
            FireExplosion = content.Load<SoundEffect>("Sounds/flamer2");
            HarvesterExplosion = content.Load<SoundEffect>("Sounds/xplobig4");
            HeavyMachineGun = content.Load<SoundEffect>("Sounds/gun8");
            ObeliskCharging = content.Load<SoundEffect>("Sounds/obelpowr");
            ObeliskShot = content.Load<SoundEffect>("Sounds/obelray1");
            Rocket1 = content.Load<SoundEffect>("Sounds/rocket1");
            Rocket2 = content.Load<SoundEffect>("Sounds/rocket2");

            InfantryDeath1 = content.Load<SoundEffect>("Sounds/nuyell1");
            InfantryDeath2 = content.Load<SoundEffect>("Sounds/nuyell3");
            InfantryDeath3 = content.Load<SoundEffect>("Sounds/nuyell4");
            InfantryDeath4 = content.Load<SoundEffect>("Sounds/nuyell5");

            MissionAccomplished = content.Load<SoundEffect>("Sounds/accom1");
            MissionFailed = content.Load<SoundEffect>("Sounds/fail1");

            Void = content.Load<SoundEffect>("Sounds/void");
        }
    }
}
