using Scripts.Behaviours;
using Scripts.Behaviours.Impls;
using Scripts.Databases.Projectile;
using Scripts.Databases.Projectile.Impls;
using Scripts.ObjectPooling.Objects;
using Scripts.ObjectPooling.Pools;
using Scripts.ObjectPooling.Pools.Impls;
using UnityEngine;
using Zenject;

namespace Scripts.Installers.Main
{
    [CreateAssetMenu(menuName = "Installers/MainPrefabInstaller", fileName = "MainPrefabInstaller")]
    public class MainPrefabInstaller : ScriptableObjectInstaller
    {
        [Header("Databases")]
        [SerializeField] private ProjectileSettingsDatabase projectileSettingsDatabase;
        [SerializeField] private EnemySettingsDatabase enemySettingsDatabase;

        [Header("Prefabas")] 
        [SerializeField] private ProjectileBehaviour projectileBehaviour;
        [SerializeField] private EnemyBehaviour enemyBehaviour;
        [SerializeField] private MainSceneView mainSceneView;

        public override void InstallBindings()
        {
            BindDatabases();
            BindObjectPools();
            BindPrefabs();
        }

        private void BindDatabases()
        {
            Container.Bind<IProjectileSettingsDatabase>().FromInstance(projectileSettingsDatabase).AsSingle();
            Container.Bind<IEnemySettingsDatabase>().FromInstance(enemySettingsDatabase).AsSingle();
        }

        private void BindObjectPools()
        {
            BindPool<ProjectileBehaviour, ProjectilePool, IProjectilePool>(projectileBehaviour);
            BindPool<EnemyBehaviour, EnemyPool, IEnemyPool>(enemyBehaviour, 150);
        }

        private void BindPrefabs()
        {
#if UNITY_EDITOR
            var parent = new GameObject("GameWorld").transform;
#endif
            BindPrefab(mainSceneView, parent);
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
        
        private void BindPrefab<TContent>(TContent prefab, Transform parent)
            where TContent : Object =>
            Container.BindInterfacesAndSelfTo<TContent>()
                .FromComponentInNewPrefab(prefab)
#if UNITY_EDITOR
                .UnderTransform(parent)
#endif
                .AsSingle();


    }
}