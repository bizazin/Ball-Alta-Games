using Scripts.Models;
using UnityEngine;

namespace Scripts.Databases.Impls
{
    [CreateAssetMenu(menuName = "Databases/PlayerAnimationSettingsDatabase", fileName = "PlayerAnimationSettingsDatabase")]
    public class PlayerAnimationSettingsDatabase : ScriptableObject, IPlayerAnimationSettingsDatabase
    {
        [SerializeField] private PlayerAnimationSettingsVo playerAnimationSettings;

        public PlayerAnimationSettingsVo Settings => playerAnimationSettings;
    }
}