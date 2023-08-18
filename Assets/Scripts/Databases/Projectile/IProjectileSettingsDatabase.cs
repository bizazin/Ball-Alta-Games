using Scripts.Models;

namespace Scripts.Databases.Projectile
{
    public interface IProjectileSettingsDatabase
    {
        ProjectileSettingsVo Settings { get; }
    }
}