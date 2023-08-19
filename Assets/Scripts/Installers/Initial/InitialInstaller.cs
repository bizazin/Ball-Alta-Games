using Scripts.Core;
using Zenject;

namespace Scripts.Installers.Initial
{
    public class InitialInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.BindInterfacesTo<MainBootstrap>().AsSingle().NonLazy();
    }
}