using Scripts.Models;
using UnityEngine;

namespace Scripts.Databases.Impls
{
    [CreateAssetMenu(menuName = "Databases/DoorAnimationSettingsDatabase", fileName = "DoorAnimationSettingsDatabase")]
    public class DoorAnimationSettingsDatabase : ScriptableObject, IDoorAnimationSettingsDatabase
    {
        [SerializeField] private DoorAnimationSettingsVo playerAnimationSettings;

        public DoorAnimationSettingsVo Settings => playerAnimationSettings;
    }
}