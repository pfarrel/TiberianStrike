using TiberianStrike.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiberianStrike.Entities.Explosions;
using TiberianStrike.Entities.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TiberianStrike
{
    class World
    {
        public const int CellSize = 24;
        public const int FixedWidth = 744;
        public const int FixedHeight = 744;

        public int Width { get; }
        public int Height { get; }
        public List<IEntity> Entities { get; set; }
        public List<Projectile> Projectiles { get; set; }
        public List<Explosion> Explosions { get; set; }
        public bool[,] Explored { get; private set; }
        public int Ticks { get; private set; }

        public World(int width, int height)
        {
            Width = width;
            Height = height;
            Entities = new List<IEntity>();
            Projectiles = new List<Projectile>();
            Explosions = new List<Explosion>();
            Explored = new bool[height / CellSize + 1, width / CellSize + 1];
            Ticks = 0;
        }

        public void AddEntity(IEntity entity)
        {
            Entities.Add(entity);
        }

        public void AddExplosion(Explosion explosion)
        {
            Explosions.Add(explosion);
        }

        public void AddProjectile(Projectile projectile)
        {
            Projectiles.Add(projectile);
        }

        public List<TPlayerEntity> GetEntities<TPlayerEntity>() where TPlayerEntity : IEntity
        {
            return Entities
                .Where(entity => entity is TPlayerEntity)
                .Select(entity => (TPlayerEntity) entity)
                .ToList();
        }

        public void Tick()
        {
            Ticks++;
        }

        public void Explore(Vector2 center, float range)
        {
            for (int y = 0; y < Explored.GetLength(0); y++)
            {
                for (int x = 0; x < Explored.GetLength(1); x++)
                {
                    Vector2 cell = new Vector2(x * CellSize + CellSize / 2, y * CellSize + CellSize / 2);
                    if (Vector2.Distance(center, cell) < range)
                    {
                        Explored[y, x] = true;
                    }
                }
            }
        }

        public bool IsExplored(Point point)
        {
            return Explored[point.Y / CellSize, point.X / CellSize];
        }

        public List<IEntity> GetAdjacentEntities(IEntity entity)
        {
            Point entityCellPoint = ToCellPoint(entity.Position);
            return Entities
                .Where(e => ManhattanDistance(entityCellPoint, ToCellPoint(e.Position)) <= 1)
                .Where(e => e != entity)
                .ToList();
        }

        private Point ToCellPoint(Point point)
        {
            return new Point(point.X / CellSize, point.Y / CellSize);
        }

        private static int ManhattanDistance(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public void DrawFog(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Explored.GetLength(0); y++)
            {
                for (int x = 0; x < Explored.GetLength(1); x++)
                {
                    if (!Explored[y, x])
                    {
                        spriteBatch.Draw(Sprites.WhitePixel.Texture, new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), null, Color.Black, 0, Vector2.Zero, SpriteEffects.None, ZOrder.Fog);
                    }
                }
            }
        }
    }
}
