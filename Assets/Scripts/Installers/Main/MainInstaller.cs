using Scripts.Controllers;
using Zenject;

namespace Scripts.Installers.Main
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindControllers();
        }

        private void BindControllers()
        {
            Container.BindInterfacesTo<MainSceneController>().AsSingle();
        }
    }
}