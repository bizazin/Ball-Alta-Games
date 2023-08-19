using UnityEngine;

namespace Scripts.Ecs.Projectile.Utils
{
    public static class ProjectileExtensions
    {
        public static ProjectileEntity CreateProjectile(this ProjectileContext context, float infectRadius,
            Vector3 scale)
        {
            var entity = context.CreateEntity();
            
            entity.AddInfectRadius(infectRadius);
            entity.AddProjectileScale(scale);
            
            return entity;
        }
    }
}