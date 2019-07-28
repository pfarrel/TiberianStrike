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
        public List<IEntity> Entities { get; set; }
        public List<Projectile> Projectiles { get; set; }
        public List<Explosion> Explosions { get; set; }

        public World()
        {
            Entities = new List<IEntity>();
            Projectiles = new List<Projectile>();
            Explosions = new List<Explosion>();
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
    }
}
