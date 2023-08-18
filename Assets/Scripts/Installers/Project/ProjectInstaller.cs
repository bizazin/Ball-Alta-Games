using Scripts.ObjectPooling.Objects.Impls;
using Services.SceneLoading.Impls;
using UnityEngine;
using Zenject;

namespace Installers.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineRunner coroutineRunner;

        public override void InstallBindings()
        {
            BindPrefabs();
            BindServices();
        }

        private void BindPrefabs()
        {
#if UNITY_EDITOR
            var parent = new GameObject("ProjectGameWorld").transform;
#endif

            BindPrefab(coroutineRunner, parent);
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<SceneLoadingService>().AsSingle();
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