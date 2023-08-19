using Scripts.Enums;
using UnityEngine;

namespace Scripts.Ecs.Player.Utils
{
    public static class PlayerExtensions
    {
        public static PlayerEntity CreatePlayer(this PlayerContext context, Vector3 scale)
        {
            var entity = context.CreateEntity();
            
            entity.AddState(EPlayerState.ReadyForLoad);
            entity.AddPlayerScale(scale);
            entity.AddPlayerMaxScale(scale);
            
            return entity;
        }
    }
}