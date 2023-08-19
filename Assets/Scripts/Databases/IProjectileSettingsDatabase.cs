using Scripts.Models;

namespace Scripts.Databases
{
    public interface IProjectileSettingsDatabase
    {
        ProjectileSettingsVo Settings { get; }
    }
}