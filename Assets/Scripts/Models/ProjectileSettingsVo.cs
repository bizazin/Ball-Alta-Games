using System;

namespace Scripts.Models
{
    [Serializable]
    public class ProjectileSettingsVo
    {
        public float GrowRate;
        public float LaunchForce;
        public float InfectRadiusMultiplier;
        public int ProjectileTimeoutDurationS;
    }
}