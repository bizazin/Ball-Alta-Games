namespace Scripts.Ecs.Projectile.Utils
{
    public static class ProjectileExtensions
    {
        public static ProjectileEntity CreateProjectile(this ProjectileContext context, float infectRadius)
        {
            var entity = context.CreateEntity();
            
            entity.AddInfectRadius(infectRadius);
            
            return entity;
        }
    }
}