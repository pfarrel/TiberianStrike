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
    class CellPoint
    {
        public int X { get; }
        public int Y { get; }
        public int WorldX => X * World.CellSize + World.CellSize / 2;
        public int WorldY => Y * World.CellSize + World.CellSize / 2;
        public Point WorldPoint => new Point(WorldX, WorldY);

        public CellPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static CellPoint operator +(CellPoint a, CellPoint b) => new CellPoint(a.X + b.X, a.Y + b.Y);
    }
}
