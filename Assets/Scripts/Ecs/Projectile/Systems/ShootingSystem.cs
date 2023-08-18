using System;
using System.Collections;
using Entitas;
using Scripts.Behaviours;
using Scripts.Databases.Projectile;
using Scripts.Ecs.Projectile.Utils;
using Scripts.Enums;
using Scripts.Extensions;
using Scripts.ObjectPooling.Objects;
using Scripts.ObjectPooling.Pools;
using Scripts.Services.ProjectileTimeout;
using UnityEngine;
using Zenject;

namespace Scripts.Ecs.Projectile.Systems
{
    public class ShootingSystem : IExecuteSystem, IInitializable
    {
        private readonly IProjectileSettingsDatabase _projectileSettingsDatabase;
        private readonly IProjectilePool _projectilePool;
        private readonly IMainSceneView _mainSceneView;
        private readonly ProjectileContext _projectileContext;
        private readonly PlayerContext _playerContext;
        private readonly IProjectileTimeoutService _projectileTimeoutService;

        private ProjectileBehaviour _currentProjectile;
        private bool _isDragging;
        private ProjectileEntity _projectileEntity;

        public ShootingSystem
        (
            IProjectileSettingsDatabase projectileSettingsDatabase,
            IProjectilePool projectilePool,
            IMainSceneView mainSceneView,
            ProjectileContext projectileContext,
            PlayerContext playerContext,
            IProjectileTimeoutService projectileTimeoutService
        )
        {
            _projectileSettingsDatabase = projectileSettingsDatabase;
            _projectilePool = projectilePool;
            _mainSceneView = mainSceneView;
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
                _currentProjectile = _projectilePool.SpawnAndActivate(_mainSceneView.ProjectileSpawnPoint);
                var currentProjectileTransform = _currentProjectile.transform;
                currentProjectileTransform.position = _mainSceneView.ProjectileSpawnPoint.position;
                _projectileEntity = _projectileContext.CreateProjectile(currentProjectileTransform.localScale.x *
                                                                           _projectileSettingsDatabase.Settings
                                                                               .InfectRadiusMultiplier);
                _currentProjectile.Link(_projectileEntity, _projectileContext);
                ShowLine();
            }

            _isDragging = true;
            ShowLine();
        }

        private void HandleMouseDrag()
        {
            var currentTouchPosition = GetPlaneIntersectionPoint();
            UpdateLine(currentTouchPosition);
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
            _mainSceneView.SetShootLineActive(false);
        }

        private void ShowLine()
        {
            var touchPosition = GetPlaneIntersectionPoint();
            var startPosition = _mainSceneView.ShootLine.GetPosition(0);

            var position = _currentProjectile.transform.position;
            _mainSceneView.ShootLine.SetPosition(0, new Vector3(position.x, startPosition.y, position.z));
            _mainSceneView.ShootLine.SetPosition(1, new Vector3(touchPosition.x, startPosition.y, touchPosition.z));

            _mainSceneView.SetShootLineActive(true);
        }

        private Vector3 GetPlaneIntersectionPoint()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, _currentProjectile.transform.position);
            if (plane.Raycast(ray, out float enter))
                return ray.GetPoint(enter);

            return Vector3.zero;
        }

        private void UpdateLine(Vector3 intersectionPoint) => _mainSceneView.ShootLine.SetPosition(1,
            new Vector3(intersectionPoint.x, _mainSceneView.ShootLine.GetPosition(0).y, intersectionPoint.z));

        private void GrowProjectile()
        {
            var transform = _currentProjectile.transform;
            var localScale = transform.localScale;
            localScale += Vector3.one * _projectileSettingsDatabase.Settings.GrowRate * Time.deltaTime;
            transform.localScale = localScale;
            
            _mainSceneView.ShootLine.startWidth = localScale.x;
            _mainSceneView.ShootLine.endWidth = localScale.x;
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