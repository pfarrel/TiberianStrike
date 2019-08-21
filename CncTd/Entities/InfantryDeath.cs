using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    public enum InfantryDeathType
    {
        Bullet,
        Explosive,
        Fire
    }

    public static class InfantryDeathExtensionMethods
    {
        public static SoundEffect GetSoundEffect(this InfantryDeathType self)
        {
            List<SoundEffect> deathSounds = new List<SoundEffect>() { Sounds.InfantryDeath1, Sounds.InfantryDeath2, Sounds.InfantryDeath3, Sounds.InfantryDeath4 };

            switch (self)
            {
                case InfantryDeathType.Bullet:
                    return Sounds.InfantryDeath4;
                case InfantryDeathType.Explosive:
                    return deathSounds[new Random().Next(deathSounds.Count)];
                case InfantryDeathType.Fire:
                    return Sounds.InfantryDeath1;
                default:
                    throw new Exception("unhandled death type: " + self.ToString());
            }
        }

        public static string GetSpriteName(this InfantryDeathType self)
        {
            List<string> explosiveDeaths = new List<string>() { "die2", "die3", "die4" };

            switch (self)
            {
                case InfantryDeathType.Bullet:
                    return "die1";
                case InfantryDeathType.Explosive:
                    return explosiveDeaths[new Random().Next(explosiveDeaths.Count)];
                case InfantryDeathType.Fire:
                    return "die5";
                default:
                    throw new Exception("unhandled death type: " + self.ToString());
            }
        }
    }
}
