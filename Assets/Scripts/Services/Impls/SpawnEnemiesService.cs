using System.Collections.Generic;
using Scripts.Core.Abstracts;
using Scripts.Databases;
using Scripts.Extensions;
using Scripts.ObjectPooling.Pools;
using UnityEngine;

namespace Scripts.Services.Impls
{
    public class SpawnEnemiesService : ISpawnEnemiesService
    {
        private readonly IEnemySettingsDatabase _enemySettingsDatabase;
        private readonly IEnemyPool _enemyPool;
        private List<Vector3> _spawnedPositions = new();

        public SpawnEnemiesService
        (
            IEnemySettingsDatabase enemySettingsDatabase,
            IEnemyPool enemyPool
        )
        {
            _enemySettingsDatabase = enemySettingsDatabase;
            _enemyPool = enemyPool;
        }
        
        public void SpawnEnemies(Transform enemySpawnPoint)
        {
            for (var i = 0; i < _enemySettingsDatabase.Settings.EnemiesCount; i++)
            {
                var spawnPos = GenerateSpawnPosition(enemySpawnPoint.position);

                var enemyBehaviour = _enemyPool.SpawnAndActivate(enemySpawnPoint);
                enemyBehaviour.transform.position = spawnPos;

                _spawnedPositions.Add(spawnPos);
            }
        }

        private Vector3 GenerateSpawnPosition(Vector3 enemySpawnPosition)
        {
            Vector3 spawnPos;
            bool positionValid;

            do
            {
                var angle = Random.Range(0, 360);
                var distance = Random.Range(0, _enemySettingsDatabase.Settings.CircleRadius);
                var x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
                var z = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
                spawnPos = new Vector3(x, 1, z) + enemySpawnPosition;

                positionValid = true;

                foreach (var pos in _spawnedPositions)
                    if (Vector3.Distance(spawnPos, pos) < _enemySettingsDatabase.Settings.MinEnemySpacing)
                    {
                        positionValid = false;
                        break;
                    }
            } while (!positionValid);

            return spawnPos;
        }
    }
}