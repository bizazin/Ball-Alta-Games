using Scripts.Models;
using UnityEngine;

namespace Scripts.Databases.Impls
{
    [CreateAssetMenu(menuName = "Databases/PlayerSettingsDatabase", fileName = "PlayerSettingsDatabase")]
    public class PlayerSettingsDatabase : ScriptableObject, IPlayerSettingsDatabase
    {
        [SerializeField] private PlayerSettingsVo playerSettings;

        public PlayerSettingsVo Settings => playerSettings;
    }
}