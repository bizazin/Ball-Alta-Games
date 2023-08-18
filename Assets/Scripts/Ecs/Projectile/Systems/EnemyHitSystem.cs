using System.Collections.Generic;
using Entitas;
using Scripts.Behaviours;
using Scripts.Enums;
using Scripts.Extensions;
using Scripts.ObjectPooling.Objects;
using Scripts.ObjectPooling.Pools;
using Scripts.Services.ProjectileTimeout;
using UnityEngine;

namespace Scripts.Ecs.Projectile.Systems
{
    public class EnemyHitSystem : ReactiveSystem<ProjectileEntity>
    {
        private readonly ProjectileContext _projectileContext;
        private readonly IProjectilePool _projectilePool;
        private readonly IEnemyPool _enemyPool;
        private readonly PlayerContext _playerContext;
        private readonly IProjectileTimeoutService _projectileTimeoutService;

        public EnemyHitSystem
        (
            ProjectileContext projectileContext,
            IProjectilePool projectilePool,
            IEnemyPool enemyPool,
            PlayerContext playerContext,
            IProjectileTimeoutService projectileTimeoutService
        ) : base(projectileContext)
        {
            _projectileContext = projectileContext;
            _projectilePool = projectilePool;
            _enemyPool = enemyPool;
            _playerContext = playerContext;
            _projectileTimeoutService = projectileTimeoutService;
        }

        protected override ICollector<ProjectileEntity> GetTrigger(IContext<ProjectileEntity> context) =>
            context.CreateCollector(ProjectileMatcher.EnemyHitPoint);

        protected override bool Filter(ProjectileEntity entity) => entity.hasEnemyHitPoint && !entity.isDestroyed;

        protected override void Execute(List<ProjectileEntity> entities)
        {
            _playerContext.ReplaceState(EPlayerState.ReadyForLoad);
            foreach (var projectileEntity in entities)
            {
                var hitColliders = Physics.OverlapSphere(projectileEntity.enemyHitPoint.Value,
                    _projectileContext.infectRadius.Value);
                foreach (var hitCollider in hitColliders)
                {
                    var hitEnemy = hitCollider.GetComponent<EnemyBehaviour>();
                    if (hitEnemy != null)
                        _enemyPool.DespawnAndDeactivate(hitEnemy);
                }

                _projectileTimeoutService.StopCoroutine();
                _projectilePool.DespawnAndDeactivate();
                projectileEntity.Destroy();
            }
        }
    }
}