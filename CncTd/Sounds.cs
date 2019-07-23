using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd
{
    class Sounds
    {
        public static SoundEffect FireExplosion { get; private set; }
        public static SoundEffect HarvesterExplosion { get; private set; }
        public static SoundEffect HeavyMachineGun { get; private set; }
        public static SoundEffect CannonShot { get; private set; }

        public static void Load(ContentManager content)
        {
            FireExplosion = content.Load<SoundEffect>("Sounds/flamer2");
            HarvesterExplosion = content.Load<SoundEffect>("Sounds/xplobig4");
            HeavyMachineGun = content.Load<SoundEffect>("Sounds/gun8");
            CannonShot = content.Load<SoundEffect>("Sounds/tnkfire4");
        }
    }
}
