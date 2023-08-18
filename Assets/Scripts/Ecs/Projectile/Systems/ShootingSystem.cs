using System;
using Entitas;
using Scripts.Behaviours;
using Scripts.Databases.Projectile;
using Scripts.Extensions;
using Scripts.ObjectPooling.Objects.Impls;
using Scripts.ObjectPooling.Pools;
using UnityEngine;

namespace Scripts.Ecs.Projectile.Systems
{
    public class ShootingSystem : IExecuteSystem
    {
        private readonly IProjectileSettingsDatabase _projectileSettingsDatabase;
        private readonly IProjectilePool _projectilePool;
        private readonly IMainSceneView _mainSceneView;

        private ProjectileBehaviour _currentProjectile;
        private bool _isDragging;

        public ShootingSystem
        (
            IProjectileSettingsDatabase projectileSettingsDatabase,
            IProjectilePool projectilePool,
            IMainSceneView mainSceneView
        )
        {
            _projectileSettingsDatabase = projectileSettingsDatabase;
            _projectilePool = projectilePool;
            _mainSceneView = mainSceneView;
        }

        public void Execute()
        {
            if (Camera.main == null)
                throw new Exception($"[{nameof(ShootingSystem)}] Main camera is null");

            if (Input.GetMouseButtonDown(0))
                HandleMouseButtonDown();
            else if (Input.GetMouseButton(0) && _isDragging)
                HandleMouseDrag();
            else if (Input.GetMouseButtonUp(0) && _isDragging)
                HandleMouseButtonUp();
        }

        private void HandleMouseButtonDown()
        {
            if (_currentProjectile == null)
            {
                _currentProjectile = _projectilePool.SpawnAndActivate(_mainSceneView.ProjectileSpawnPoint);
                ShowLine();
            }

            _isDragging = true;
            ShowLine();
        }

        private void HandleMouseDrag()
        {
            var currentTouchPosition = GetPlaneIntersectionPoint();
            UpdateLine(currentTouchPosition);
            GrowBall();
        }

        private void HandleMouseButtonUp()
        {
            _isDragging = false;
            var releasePosition = GetPlaneIntersectionPoint();
            LaunchBall(releasePosition);
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

        private void GrowBall()
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
            _currentProjectile.AddForce(direction * _projectileSettingsDatabase.Settings.LaunchForce);
        }
    }
}