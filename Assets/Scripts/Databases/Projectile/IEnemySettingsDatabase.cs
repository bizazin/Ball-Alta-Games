using Scripts.Models;

namespace Scripts.Databases.Projectile
{
    public interface IEnemySettingsDatabase
    {
        EnemySettingsVo Settings { get; }
    }
}