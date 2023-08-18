using Entitas;
using Scripts.Ecs.Core;
using Scripts.Ecs.Projectile.Systems;
using Scripts.Extensions;
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
            BindContextFromInstance(contexts.projectile);
            Container.BindDestroyedCleanup<ProjectileContext, ProjectileEntity>(ProjectileMatcher.Destroyed);
        }

        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<ShootingSystem>().AsSingle();
        }

        private void BindContextFromInstance<TContext>(TContext context)
            where TContext : IContext =>
            Container.BindInterfacesAndSelfTo<TContext>().FromInstance(context).AsSingle();
    }
}