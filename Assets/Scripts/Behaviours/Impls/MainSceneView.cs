using UnityEngine;

namespace Scripts.Behaviours.Impls
{
    public class MainSceneView : MonoBehaviour, IMainSceneView
    {
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] private LineRenderer shootLine;
        [SerializeField] private Transform enemySpawnPoint;

        public Transform ProjectileSpawnPoint => projectileSpawnPoint;
        public LineRenderer ShootLine => shootLine;
        public Transform EnemySpawnPoint => enemySpawnPoint;
        
        public void SetShootLineActive(bool isActive) => shootLine.enabled = isActive;
    }
}