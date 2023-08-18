using Scripts.Controllers;
using Scripts.Services.ProjectileTimeout.Impls;
using Zenject;

namespace Scripts.Installers.Main
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Controllers
            Container.BindInterfacesTo<MainSceneController>().AsSingle();

            //Services
            Container.BindInterfacesTo<ProjectileTimeoutService>().AsSingle();

        }
    }
}