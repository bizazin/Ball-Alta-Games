using Scripts.Core.Abstracts;
using Scripts.Ecs.Player.Utils;
using Scripts.Extensions;
using Scripts.ObjectPooling.Objects;
using Scripts.ObjectPooling.Pools;
using Scripts.Services;
using Scripts.Views;
using Zenject;

namespace Scripts.Controllers.Impls
{
    public class MainSceneController : Controller<MainSceneView>, IInitializable, IMainSceneController
    {
        private readonly ISpawnEnemiesService _spawnEnemiesService;
        private readonly PlayerContext _playerContext;
        private readonly IProjectilePool _projectilePool;
        private readonly IVictoryAnimationService _victoryAnimationService;

        public MainSceneController
        (
            ISpawnEnemiesService spawnEnemiesService,
            PlayerContext playerContext,
            IProjectilePool projectilePool,
            IVictoryAnimationService victoryAnimationService
        )
        {
            _spawnEnemiesService = spawnEnemiesService;
            _playerContext = playerContext;
            _projectilePool = projectilePool;
            _victoryAnimationService = victoryAnimationService;
        }

        public void Initialize()
        {
            _spawnEnemiesService.SpawnEnemies(View.EnemySpawnPoint);
            var playerEntity = _playerContext.CreatePlayer(View.PlayerBehaviour.transform.localScale);
            View.PlayerBehaviour.Link(playerEntity, _playerContext);
            View.PathBehaviour.Link(playerEntity, _playerContext);
        }

        
        public ProjectileBehaviour SpawnProjectile()
        {
            var projectile = _projectilePool.SpawnAndActivate(View.ProjectileSpawnPoint);
            projectile.transform.position = View.ProjectileSpawnPoint.position;
            return projectile;
        }

        public void DetectEnemiesOnPath()
        {
            if (!View.PathBehaviour.IsCollidingWithEnemy())
                _victoryAnimationService.PlayVictoryAnimations(View.PlayerBehaviour, View.DoorBehaviour);
        }
    }
}