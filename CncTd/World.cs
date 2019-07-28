using CncTd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncTd
{
    class World
    {
        public List<IPlayerEntity> Entities { get; set; }
        public List<Projectile> Projectiles { get; set; }
        public List<Explosion> Explosions { get; set; }

        public World()
        {
            Entities = new List<IPlayerEntity>();
            Projectiles = new List<Projectile>();
            Explosions = new List<Explosion>();
        }

        public void AddEntity(IPlayerEntity entity)
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

        public List<TPlayerEntity> GetEntities<TPlayerEntity>() where TPlayerEntity : IPlayerEntity
        {
            return Entities
                .Where(entity => entity is TPlayerEntity)
                .Select(entity => (TPlayerEntity) entity)
                .ToList();
        }
    }
}
