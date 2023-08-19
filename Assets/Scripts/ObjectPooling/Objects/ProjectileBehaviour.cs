using Scripts.Ecs.Utils.Impls;
using UnityEngine;

namespace Scripts.ObjectPooling.Objects
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ProjectileBehaviour : LinkableBehaviour<ProjectileEntity>, IProjectileScaleListener
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private LineRenderer shootLine;

        public void Launch(Vector3 direction) => 
            rigidbody.AddForce(direction, ForceMode.Impulse);

        public void Reset(Vector3 startPosition, Vector3 defaultLocalScale)
        {
            transform.position = startPosition;
            transform.localScale = defaultLocalScale;
            rigidbody.velocity = Vector3.zero;
        }
        
        public void ShowLine(Vector3 touchPosition)
        {
            var startPosition = shootLine.GetPosition(0);

            var projectileSpawnPos = gameObject.transform.position;
            shootLine.SetPosition(0, new Vector3(projectileSpawnPos.x, startPosition.y, projectileSpawnPos.z));
            shootLine.SetPosition(1, new Vector3(touchPosition.x, startPosition.y, touchPosition.z));

            SetShootLineActive(true);
        }
        
        public void UpdateLine(Vector3 intersectionPoint) => shootLine.SetPosition(1,
            new Vector3(intersectionPoint.x, shootLine.GetPosition(0).y, intersectionPoint.z));


        protected override void Listen(ProjectileEntity entity)
        {
            entity.AddProjectileScaleListener(this);
            if (entity.hasProjectileScale)
                OnProjectileScale(entity, entity.projectileScale.Value);
        }

        protected override void Unlisten(ProjectileEntity entity)
        {
            entity.RemoveProjectileScaleListener();
        }

        protected override void InternalClear()
        {
        }

        private void OnCollisionEnter(Collision enemyGameObject)
        {
            var enemy = enemyGameObject.gameObject.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                (_entity as ProjectileEntity)?.ReplaceEnemyHitPoint(enemyGameObject.transform.position);
                Unlink();
            }
        }

        public void SetShootLineActive(bool isActive) => shootLine.enabled = isActive;

        public void OnProjectileScale(ProjectileEntity entity, Vector3 value)
        {
            var scaleDiff = value - transform.localScale;
            var positionChange = new Vector3(0, scaleDiff.y / 2, 0);

            transform.localScale = value;
            transform.position += positionChange;
        }
    }
}