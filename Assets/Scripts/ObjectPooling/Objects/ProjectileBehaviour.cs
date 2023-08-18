using Scripts.Ecs.Utils.Impls;
using UnityEngine;

namespace Scripts.ObjectPooling.Objects
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ProjectileBehaviour : LinkableBehaviour<ProjectileEntity>
    {
        [SerializeField] private Rigidbody rigidbody;

        public void Launch(Vector3 direction) => 
            rigidbody.AddForce(direction, ForceMode.Impulse);

        public void Reset(Vector3 startPosition, Vector3 defaultLocalScale)
        {
            transform.position = startPosition;
            transform.localScale = defaultLocalScale;
            rigidbody.velocity = Vector3.zero;
        }

        protected override void Listen(ProjectileEntity entity)
        {
        }

        protected override void Unlisten(ProjectileEntity entity)
        {
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
    }
}