using UnityEngine;

namespace Scripts.Services
{
    public interface ISpawnEnemiesService
    {
        void SpawnEnemies(Transform enemySpawnPoint);
    }
}