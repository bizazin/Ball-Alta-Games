using Scripts.Controllers.Impls;
using Scripts.Databases;
using Scripts.Databases.Impls;
using Scripts.Extensions;
using Scripts.ObjectPooling.Objects;
using Scripts.ObjectPooling.Pools;
using Scripts.ObjectPooling.Pools.Impls;
using Scripts.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Installers.Main
{
    [CreateAssetMenu(menuName = "Installers/MainPrefabInstaller", fileName = "MainPrefabInstaller")]
    public class MainPrefabInstaller : ScriptableObjectInstaller
    {
        [Header("Databases")]
        [SerializeField] private ProjectileSettingsDatabase projectileSettingsDatabase;
        [SerializeField] private EnemySettingsDatabase enemySettingsDatabase;
        [SerializeField] private PlayerSettingsDatabase playerSettingsDatabase;
        [SerializeField] private PlayerAnimationSettingsDatabase playerAnimationSettingsDatabase;
        [SerializeField] private DoorAnimationSettingsDatabase doorAnimationSettingsDatabase;
        [SerializeField] private GameOutcomeDatabase gameOutcomeDatabase;


        [Header("Canvas")] 
        [SerializeField] private Canvas canvas;

        [Header("Ui Views")]
        [SerializeField] private GameResultView gameResultView;

        [Header("Prefabas")] 
        [SerializeField] private ProjectileBehaviour projectileBehaviour;

        [SerializeField] private EnemyBehaviour enemyBehaviour;
        [SerializeField] private MainSceneView mainSceneView;

        public override void InstallBindings()
        {
            BindDatabases();
            BindObjectPools();
            BindViews();
            BindUiViews();
        }

        private void BindDatabases()
        {
            Container.Bind<IProjectileSettingsDatabase>().FromInstance(projectileSettingsDatabase).AsSingle();
            Container.Bind<IEnemySettingsDatabase>().FromInstance(enemySettingsDatabase).AsSingle();
            Container.Bind<IPlayerSettingsDatabase>().FromInstance(playerSettingsDatabase).AsSingle();
            Container.Bind<IPlayerAnimationSettingsDatabase>().FromInstance(playerAnimationSettingsDatabase).AsSingle();
            Container.Bind<IDoorAnimationSettingsDatabase>().FromInstance(doorAnimationSettingsDatabase).AsSingle();
            Container.Bind<IGameOutcomeDatabase>().FromInstance(gameOutcomeDatabase).AsSingle();
        }

        private void BindObjectPools()
        {
            BindPool<ProjectileBehaviour, ProjectilePool, IProjectilePool>(projectileBehaviour);
            BindPool<EnemyBehaviour, EnemyPool, IEnemyPool>(enemyBehaviour, 100);
        }

        private void BindViews()
        {
            var parent = new GameObject("GameWorld").transform;
            Container.BindView<MainSceneController, MainSceneView>(mainSceneView, parent);
        }

        private void BindUiViews()
        {
            Container.Bind<CanvasScaler>().FromComponentInNewPrefab(canvas).AsSingle();
            var parent = Container.Resolve<CanvasScaler>().transform;
            
            Container.BindUiView<GameResultController, GameResultView>(gameResultView, parent);
        }

        private void BindPool<TItemContract, TPoolConcrete, TPoolContract>(TItemContract prefab, int size = 1)
            where TItemContract : MonoBehaviour
            where TPoolConcrete : TPoolContract, IMemoryPool
            where TPoolContract : IMemoryPool
        {
            var poolContainerName = "[Pool] " + prefab;
            Container.BindMemoryPoolCustomInterface<TItemContract, TPoolConcrete, TPoolContract>()
                .WithInitialSize(size)
                .FromComponentInNewPrefab(prefab)
#if UNITY_EDITOR
                .UnderTransformGroup(poolContainerName)
#endif
                .AsCached()
                .OnInstantiated((pool, item) => (item as MonoBehaviour)?.gameObject.SetActive(false));
            
            
        }
    }
}