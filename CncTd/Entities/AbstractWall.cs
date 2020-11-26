using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiberianStrike.Entities
{
    abstract class AbstractWall : BaseEntity
    {
        public override int MaxHealth => 20;
        protected abstract SpriteSheet SpriteSheet { get; }

        public AbstractWall(World world, Player player, Point position) : base(world, player, position)
        {
        }

        protected override SpriteFrame GetSpriteFrame()
        {
            List<IEntity> adjacentWalls = World.GetAdjacentEntities(this).Where(e => e.GetType() == this.GetType()).ToList();
            WallNeighbours wallNeighbours = WallNeighbours.None;
            foreach (IEntity adjacentWall in adjacentWalls)
            {
                if (adjacentWall.Position.X > Position.X && adjacentWall.Position.Y == Position.Y)
                {
                    wallNeighbours |= WallNeighbours.Right;
                }
                else if (adjacentWall.Position.X < Position.X && adjacentWall.Position.Y == Position.Y)
                {
                    wallNeighbours |= WallNeighbours.Left;
                }
                else if (adjacentWall.Position.Y > Position.Y && adjacentWall.Position.X == Position.X)
                {
                    wallNeighbours |= WallNeighbours.Down;
                }
                else if (adjacentWall.Position.Y < Position.Y && adjacentWall.Position.X == Position.X)
                {
                    wallNeighbours |= WallNeighbours.Up;
                }
                else
                {
                    throw new Exception(String.Format("Unexpected neighbour positions. {0} and {1}", this.Position, adjacentWall.Position));
                }
            }
            String name = Health >= (MaxHealth / 2) ? "default" : Health >= (MaxHealth / 4) ? "damaged" : Health >= (MaxHealth / 10) ? "heavily-damaged" : "dead";
            return SpriteSheet.GetFrameForWall(name, wallNeighbours);
        }
    }
}
