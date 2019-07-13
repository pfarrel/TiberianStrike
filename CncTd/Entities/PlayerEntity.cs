using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd.Entities
{
    interface IPlayerEntity
    {
        Player Player { get; }
        Point Position { get; }
    }
}
