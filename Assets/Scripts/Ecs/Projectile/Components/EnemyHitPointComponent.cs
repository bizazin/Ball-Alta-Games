using Entitas;
using UnityEngine;

namespace Scripts.Ecs.Projectile.Components
{
    [Projectile]
    public class EnemyHitPointComponent : IComponent
    {
        public Vector3 Value;
    }
}