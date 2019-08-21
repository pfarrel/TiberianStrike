using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    public enum Warhead
    {
        Bullet,
        Explosive,
        Fire,
    }

    public static class WarheadExtensionMethods
    {
        public static InfantryDeathType GetInfantryDeathType(this Warhead self)
        {
            switch (self)
            {
                case Warhead.Bullet:
                    return InfantryDeathType.Bullet;
                case Warhead.Explosive:
                    return InfantryDeathType.Explosive;
                case Warhead.Fire:
                    return InfantryDeathType.Fire;
                default:
                    throw new Exception("unhandled warhead: " + self.ToString());
            }
        }
    }
}
