using Scripts.Behaviours.Impls;
using Scripts.Core.Abstracts;
using UnityEngine;

namespace Scripts.Views
{
    public class MainSceneView : View
    {
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] private Transform enemySpawnPoint;
        [SerializeField] private PlayerBehaviour playerBehaviour;
        [SerializeField] private DoorBehaviour doorBehaviour;
        [SerializeField] private PathBehaviour pathBehaviour;

        public Transform ProjectileSpawnPoint => projectileSpawnPoint;
        public Transform EnemySpawnPoint => enemySpawnPoint;
        public PlayerBehaviour PlayerBehaviour => playerBehaviour;
        public DoorBehaviour DoorBehaviour => doorBehaviour;
        public PathBehaviour PathBehaviour => pathBehaviour;
    }
}