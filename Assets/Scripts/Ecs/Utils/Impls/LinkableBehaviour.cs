using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Scripts.Ecs.Utils.Impls
{
    public abstract class LinkableBehaviour<TEntity> : LinkableBehaviour where TEntity : IEntity
    {
        private protected TEntity Entity => (TEntity) _entity;

        public sealed override void Listen(IEntity entity) => Listen((TEntity) entity);

        public sealed override void Unlisten(IEntity entity)
        {
            Unlisten((TEntity) entity);
            InternalClear();
        }

        public sealed override void Clear() => InternalClear();

        protected abstract void Listen(TEntity entity);

        protected abstract void Unlisten(TEntity entity);

        protected abstract void InternalClear();
    }

    [RequireComponent(typeof(EntityLink))]
    public abstract class LinkableBehaviour : MonoBehaviour, ILinkedObject
    {
        [SerializeField] private EntityLink entityLink;
        private protected IEntity _entity { get; private set; }
        private bool _destroyed;

        public int Hash => transform.GetHashCode();

        public bool IsLinked => _entity != default;

        public void Link(IEntity entity, IContext context)
        {
            entityLink.Link(entity);
            _entity = entity;
            _entity.OnDestroyEntity += OnDestroyEntity;
            Listen(_entity);
        }

        public virtual void Destroy() => Destroy(gameObject);

        private void OnDestroyEntity(IEntity entity)
        {
            entityLink.Unlink();
            if (!_destroyed)
                Clear();

            if (entity.isEnabled)
            {
                Unlisten(_entity);
                _entity.OnDestroyEntity -= OnDestroyEntity;
            }

            _entity = default;
        }

        public abstract void Listen(IEntity entity);

        public abstract void Unlisten(IEntity entity);

        public void Unlink()
        {
            if (_entity == null)
                return;
            OnDestroyEntity(_entity);
        }

        public abstract void Clear();

        private void OnDestroy()
        {
            _destroyed = true;
            if (_entity != null)
                OnDestroyEntity(_entity);
            OnDestroyed();
        }

        protected virtual void OnDestroyed()
        {
        }
    }
}