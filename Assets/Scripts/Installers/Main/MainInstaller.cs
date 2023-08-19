using Scripts.Services.Impls;
using Zenject;

namespace Scripts.Installers.Main
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ProjectileTimeoutService>().AsSingle();
            Container.BindInterfacesTo<SpawnEnemiesService>().AsSingle();
            Container.BindInterfacesTo<VictoryAnimationService>().AsSingle();
        }
    }
}