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
    class EntityBlock
    {
        public EntityPlacement[] EntityPlacements { get; }
        public CellPoint Offset { get; }

        public EntityBlock(CellPoint offset, params EntityPlacement[] entityPlacements)
        {
            EntityPlacements = entityPlacements;
            Offset = offset;
        }

        public void AddToWorld(World world)
        {
            foreach (EntityPlacement entityPlacement in EntityPlacements)
            {
                Point position = (entityPlacement.CellPoint + Offset).WorldPoint;
                BaseEntity entity = (BaseEntity)Activator.CreateInstance(entityPlacement.EntityType, world, entityPlacement.Player, position);
                world.AddEntity(entity);
            }
        }
    }
}
