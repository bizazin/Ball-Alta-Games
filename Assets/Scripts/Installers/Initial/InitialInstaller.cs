using Scripts.Core;
using Zenject;

namespace Installers.Initial
{
    public class InitialInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.BindInterfacesTo<MainBootstrap>().AsSingle().NonLazy();
    }
}