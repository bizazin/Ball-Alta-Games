using System;
using DesperateDevs.Utils;

namespace Scripts.Models
{
    [Serializable]
    public class PlayerAnimationSettingsVo
    {
        public int PlayerJumpsCount;
        public float JumpForce;
        public float Gravity;
        public float JumpHeight;
    }
}