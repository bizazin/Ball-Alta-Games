using Scripts.Models;

namespace Scripts.Databases
{
    public interface IEnemySettingsDatabase
    {
        EnemySettingsVo Settings { get; }
    }
}