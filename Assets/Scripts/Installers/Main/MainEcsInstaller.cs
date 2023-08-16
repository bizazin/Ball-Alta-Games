using Scripts.Ecs.Core;
using Zenject;

namespace Scripts.Installers.Main
{
    public class MainEcsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindContexts(Contexts.sharedInstance); 
            BindSystems();

            Container.BindInstance(Contexts.sharedInstance).WhenInjectedInto<EcsSystemsBootstrap>();
            Container.BindInterfacesTo<EcsSystemsBootstrap>().AsSingle().WithArguments(gameObject.name).NonLazy();

        }

        private void BindContexts(Contexts contexts)
        {
        }

        private void BindSystems()
        {
        }
    }
}   