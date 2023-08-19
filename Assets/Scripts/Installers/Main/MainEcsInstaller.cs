using Entitas;
using Scripts.Ecs.Core;
using Scripts.Ecs.Player.Systems;
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
            BindContextFromInstance(contexts.player);
            Container.BindDestroyedCleanup<PlayerContext, PlayerEntity>(PlayerMatcher.Destroyed);
        }

        private void BindSystems()
        {
            var contexts = Contexts.sharedInstance;
            
            Container.BindInterfacesAndSelfTo<ShootingSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectileEnemyHitSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerScaleCheckSystem>().AsSingle();
            
            
            Container.BindInstance(contexts).WhenInjectedInto<ProjectileEventSystems>();
            Container.BindInterfacesAndSelfTo<ProjectileEventSystems>().AsSingle().NonLazy();
            
            Container.BindInstance(contexts).WhenInjectedInto<PlayerEventSystems>();
            Container.BindInterfacesAndSelfTo<PlayerEventSystems>().AsSingle().NonLazy();

        }

        private void BindContextFromInstance<TContext>(TContext context)
            where TContext : IContext =>
            Container.BindInterfacesAndSelfTo<TContext>().FromInstance(context).AsSingle();
    }
}