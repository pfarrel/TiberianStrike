using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiberianStrike.Entities;

namespace TiberianStrike
{
    class EntityPlacement
    {
        public Player Player { get; }
        public CellPoint CellPoint { get; }
        public Type EntityType { get; }

        public EntityPlacement(Player player, CellPoint cellPoint, Type entityType)
        {
            Player = player;
            CellPoint = cellPoint;
            EntityType = entityType;
        }
    }
}
