using System.Collections.Generic;
using Entitas;
using Zenject;

namespace Scripts.Extensions
{
    public static class ContextExtension
    {
        public static void BindDestroyedCleanup<TContext, UEntity>(this DiContainer container,
            IMatcher<UEntity> matcher)
            where UEntity : class, IEntity
            where TContext : class, IContext<UEntity>
        {
            container.Bind<IMatcher<UEntity>>().FromInstance(matcher)
                .WhenInjectedInto<DestroyedCleaner<TContext, UEntity>>();
            container.BindInterfacesAndSelfTo<DestroyedCleaner<TContext, UEntity>>().AsSingle().NonLazy();
        }
    }
    public sealed class DestroyedCleaner<TContext, UEntity> : ICleanupSystem
        where UEntity : class, IEntity
        where TContext : class, IContext<UEntity>
    {
        private IGroup<UEntity> _group;
        private List<UEntity> _list = new List<UEntity>();

        public DestroyedCleaner(TContext context, IMatcher<UEntity> matcher)
        {
            _group = context.GetGroup(matcher);
        }

        public void Cleanup()
        {
            _list = _group.GetEntities(_list);
            for (var i = 0; i < _list.Count; i++)
                _list[i].Destroy();
        }
    }
}