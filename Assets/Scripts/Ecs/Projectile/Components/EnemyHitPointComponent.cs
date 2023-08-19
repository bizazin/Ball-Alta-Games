using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Scripts.Ecs.Projectile.Components
{
    [Projectile, Unique]
    public class EnemyHitPointComponent : IComponent
    {
        public Vector3 Value;
    }
}