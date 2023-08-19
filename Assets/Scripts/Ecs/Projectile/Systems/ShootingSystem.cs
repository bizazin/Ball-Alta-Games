using System;
using System.Collections;
using Entitas;
using Scripts.Behaviours;
using Scripts.Controllers;
using Scripts.Databases;
using Scripts.Ecs.Projectile.Utils;
using Scripts.Enums;
using Scripts.Extensions;
using Scripts.ObjectPooling.Objects;
using Scripts.ObjectPooling.Pools;
using Scripts.Services;
using UnityEngine;
using Zenject;

namespace Scripts.Ecs.Projectile.Systems
{
    public class ShootingSystem : IExecuteSystem, IInitializable
    {
        private readonly IProjectileSettingsDatabase _projectileSettingsDatabase;
        private readonly IPlayerSettingsDatabase _playerSettingsDatabase;
        private readonly IProjectilePool _projectilePool;
        private readonly IMainSceneController _mainSceneController;
        private readonly ProjectileContext _projectileContext;
        private readonly PlayerContext _playerContext;
        private readonly IProjectileTimeoutService _projectileTimeoutService;

        private ProjectileBehaviour _currentProjectile;
        private bool _isDragging;
        private ProjectileEntity _projectileEntity;

        public ShootingSystem
        (
            IProjectileSettingsDatabase projectileSettingsDatabase,
            IPlayerSettingsDatabase playerSettingsDatabase,
            IProjectilePool projectilePool,
            IMainSceneController mainSceneController,
            ProjectileContext projectileContext,
            PlayerContext playerContext,
            IProjectileTimeoutService projectileTimeoutService
        )
        {
            _projectileSettingsDatabase = projectileSettingsDatabase;
            _playerSettingsDatabase = playerSettingsDatabase;
            _projectilePool = projectilePool;
            _mainSceneController = mainSceneController;
            _projectileContext = projectileContext;
            _playerContext = playerContext;
            _projectileTimeoutService = projectileTimeoutService;
        }

        public void Initialize()
        {
            if (Camera.main == null)
                throw new Exception($"[{nameof(ShootingSystem)}] Main camera is null");
        }

        public void Execute()
        {
            if (_playerContext.state.Value == EPlayerState.ProjectileFlight)
                return;
            if (Input.GetMouseButtonDown(0))
                HandleMouseButtonDown();
            else if (Input.GetMouseButton(0) && _isDragging)
                HandleMouseDrag();
            else if (Input.GetMouseButtonUp(0) && _isDragging)
                HandleMouseButtonUp();
        }

        private void HandleMouseButtonDown()
        {
            if (_playerContext.state.Value == EPlayerState.ReadyForLoad)
            {
                _playerContext.ReplaceState(EPlayerState.IncreaseProjectile);
                _currentProjectile = _mainSceneController.SpawnProjectile();

                var projectileLocalScale = _currentProjectile.transform.localScale;
                _projectileEntity = _projectileContext.CreateProjectile(
                    projectileLocalScale.x *
                    _projectileSettingsDatabase.Settings.InfectRadiusMultiplier, projectileLocalScale);
                _currentProjectile.Link(_projectileEntity, _projectileContext);
                _currentProjectile.ShowLine(GetPlaneIntersectionPoint());
            }

            _isDragging = true;
            _currentProjectile.ShowLine(GetPlaneIntersectionPoint());
        }

        private void HandleMouseDrag()
        {
            var currentTouchPosition = GetPlaneIntersectionPoint();
            _currentProjectile.UpdateLine(currentTouchPosition);
            GrowProjectile();
        }

        private void HandleMouseButtonUp()
        {
            _isDragging = false;
            _projectileContext.ReplaceInfectRadius(_currentProjectile.transform.localScale.x *
                                                   _projectileSettingsDatabase.Settings.InfectRadiusMultiplier);
            var releasePosition = GetPlaneIntersectionPoint();
            LaunchBall(releasePosition);
            _playerContext.ReplaceState(EPlayerState.ProjectileFlight);
            _currentProjectile.SetShootLineActive(false);
        }

        private Vector3 GetPlaneIntersectionPoint()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, _currentProjectile.transform.position);
            if (plane.Raycast(ray, out float enter))
                return ray.GetPoint(enter);

            return Vector3.zero;
        }

        private void GrowProjectile()
        {
            _projectileEntity.ReplaceProjectileScale(_projectileEntity.projectileScale.Value +
                                                     Vector3.one * _projectileSettingsDatabase.Settings.GrowRate *
                                                     Time.deltaTime);
            _playerContext.ReplacePlayerScale(_playerContext.playerScale.Value -
                                              Vector3.one * _playerSettingsDatabase.Settings.ReductionRate *
                                              Time.deltaTime);
        }

        private void LaunchBall(Vector3 releasePosition)
        {
            var position = _currentProjectile.transform.position;
            var direction = (releasePosition - position).normalized;
            _currentProjectile.Launch(direction * _projectileSettingsDatabase.Settings.LaunchForce);
            _projectileTimeoutService.StartCoroutine(ProjectileTimeout());
        }

        private IEnumerator ProjectileTimeout()
        {
            var tempProjectile = _currentProjectile;
            yield return new WaitForSeconds(_projectileSettingsDatabase.Settings.ProjectileTimeoutDurationS);

            if (tempProjectile == _currentProjectile)
            {
                _currentProjectile.Unlink();
                _projectilePool.DespawnAndDeactivate();
                _playerContext.ReplaceState(EPlayerState.ReadyForLoad);
                _projectileEntity.Destroy();
            }
        }
    }
}