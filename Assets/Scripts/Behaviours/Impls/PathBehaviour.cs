using Scripts.Ecs.Utils.Impls;
using Scripts.ObjectPooling.Objects;
using UnityEngine;

namespace Scripts.Behaviours.Impls
{
    [RequireComponent(typeof(Collider))]
    public class PathBehaviour : LinkableBehaviour<PlayerEntity>, IPlayerScaleListener
    {
        [SerializeField] private BoxCollider enemyDetectorCollider;
        public bool IsCollidingWithEnemy()
        {
            var boxCenter = transform.TransformPoint(enemyDetectorCollider.center);
            var hitColliders = Physics.OverlapBox(boxCenter, enemyDetectorCollider.size / 2, transform.rotation);

            foreach (Collider hitCollider in hitColliders)
                if (hitCollider.GetComponent<EnemyBehaviour>() != null)
                    return true;

            return false;        
        }
        
        protected override void Listen(PlayerEntity entity)
        {
            entity.AddPlayerScaleListener(this);
            
            if (entity.hasPlayerScale)
                OnPlayerScale(entity, entity.playerScale.Value);
        }

        protected override void Unlisten(PlayerEntity entity)
        {
            entity.RemovePlayerScaleListener();
        }

        protected override void InternalClear()
        {
        }

        public void OnPlayerScale(PlayerEntity entity, Vector3 value)
        {
            var newColliderSize = enemyDetectorCollider.size;
            newColliderSize.y = value.y;
            enemyDetectorCollider.size = newColliderSize;
        }
    }
}