using UnityEngine;

namespace Scripts.Behaviours
{
    public interface IMainSceneView
    {
        Transform ProjectileSpawnPoint { get; }
        LineRenderer ShootLine { get; }
        Transform EnemySpawnPoint { get; }
        void SetShootLineActive(bool isActive);
    }
}