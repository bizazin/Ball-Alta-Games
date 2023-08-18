using Scripts.Models;
using UnityEngine;

namespace Scripts.Databases.Projectile.Impls
{
    [CreateAssetMenu(menuName = "Databases/ProjectileSettingsDatabase", fileName = "ProjectileSettingsDatabase")]
    public class ProjectileSettingsDatabase : ScriptableObject, IProjectileSettingsDatabase
    {
        [SerializeField] private ProjectileSettingsVo projectileSettings;

        public ProjectileSettingsVo Settings => projectileSettings;
    }
}