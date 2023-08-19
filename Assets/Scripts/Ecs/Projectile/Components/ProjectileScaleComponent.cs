using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Scripts.Ecs.Projectile.Components
{
    [Projectile, Unique, Event(EventTarget.Self)]
    public class ProjectileScaleComponent : IComponent
    {
        public Vector3 Value;
    }
}