using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Scripts.Ecs.Projectile.Components
{
    [Projectile, Unique, Event(EventTarget.Self)]
    public class InfectRadiusComponent : IComponent
    {
        public float Value;
    }
}