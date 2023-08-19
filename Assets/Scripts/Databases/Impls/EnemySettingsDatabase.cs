using Scripts.Models;
using UnityEngine;

namespace Scripts.Databases.Impls
{
    [CreateAssetMenu(menuName = "Databases/EnemySettingsDatabase", fileName = "EnemySettingsDatabase")]
    public class EnemySettingsDatabase : ScriptableObject, IEnemySettingsDatabase
    {
        [SerializeField] private EnemySettingsVo enemySettings;

        public EnemySettingsVo Settings => enemySettings;
    }
}