using System.Collections.Generic;
using Scripts.Behaviours;
using Scripts.Databases.Projectile;
using Scripts.Extensions;
using Scripts.ObjectPooling.Pools;
using UnityEngine;
using Zenject;

namespace Scripts.Controllers
{
    public class MainSceneController : IInitializable
    {
        private readonly IMainSceneView _mainSceneView;
        private readonly IEnemySettingsDatabase _enemySettingsDatabase;
        private readonly IEnemyPool _enemyPool;

        private readonly List<Vector3> _spawnedPositions = new();

        public MainSceneController
        (
            IMainSceneView mainSceneView,
            IEnemySettingsDatabase enemySettingsDatabase,
            IEnemyPool enemyPool
        )
        {
            _mainSceneView = mainSceneView;
            _enemySettingsDatabase = enemySettingsDatabase;
            _enemyPool = enemyPool;
        }

        public void Initialize()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            for (var i = 0; i < _enemySettingsDatabase.Settings.EnemiesCount; i++)
            {
                var spawnPos = GenerateSpawnPosition();

                var enemyBehaviour = _enemyPool.SpawnAndActivate(_mainSceneView.EnemySpawnPoint);
                enemyBehaviour.transform.position = spawnPos;

                _spawnedPositions.Add(spawnPos);
            }
        }

        private Vector3 GenerateSpawnPosition()
        {
            Vector3 spawnPos;
            bool positionValid;

            do
            {
                var angle = Random.Range(0, 360);
                var distance = Random.Range(0, _enemySettingsDatabase.Settings.CircleRadius);
                var x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
                var z = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
                spawnPos = new Vector3(x, 1, z) + _mainSceneView.EnemySpawnPoint.position;

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