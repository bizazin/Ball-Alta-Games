using System;
using Scripts.Ecs.Utils.Impls;
using Scripts.ObjectPooling.Objects;
using UnityEngine;

namespace Scripts.Behaviours.Impls
{
    public class PlayerBehaviour : LinkableBehaviour<PlayerEntity>, IPlayerScaleListener
    {
        [SerializeField] private LineRenderer playerPath;
        
        public void SetPathActive(bool isActive) => playerPath.enabled = isActive;

        public void OnPlayerScale(PlayerEntity entity, Vector3 value)
        {
            var scaleDiff = value - transform.localScale;
            var positionChange = new Vector3(0, scaleDiff.y / 2, 0);

            transform.localScale = value;
            transform.position += positionChange;
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
    }
}