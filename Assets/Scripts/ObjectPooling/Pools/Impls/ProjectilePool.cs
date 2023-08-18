
using Scripts.Extensions;
using Scripts.ObjectPooling.Core;
using Scripts.ObjectPooling.Objects;
using UnityEngine;

namespace Scripts.ObjectPooling.Pools.Impls
{
    public class ProjectilePool : Pool<ProjectileBehaviour>, IProjectilePool
    {
        private ProjectileBehaviour _currentProjectile;
        private Vector3 _startPosition;
        private Vector3 _defaultLocalScale;

        protected override void OnSpawned(ProjectileBehaviour item)
        {
            base.OnSpawned(item);
            _currentProjectile = item;
            var itemTransform = item.transform;
            _startPosition = itemTransform.position;
            _defaultLocalScale = itemTransform.localScale;
        }

        public void DespawnAndDeactivate()
        {
            _currentProjectile.Reset(_startPosition, _defaultLocalScale);
            this.DespawnAndDeactivate(_currentProjectile);
        }
    }
}