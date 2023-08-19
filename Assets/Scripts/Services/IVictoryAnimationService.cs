using Scripts.Behaviours.Impls;
using UnityEngine;

namespace Scripts.Services
{
    public interface IVictoryAnimationService
    {
        void PlayVictoryAnimations(PlayerBehaviour player, DoorBehaviour door);
    }
}