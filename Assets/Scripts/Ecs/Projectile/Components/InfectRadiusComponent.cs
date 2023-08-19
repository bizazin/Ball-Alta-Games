using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Scripts.Ecs.Projectile.Components
{
    [Projectile, Unique]
    public class InfectRadiusComponent : IComponent
    {
        public float Value;
    }
}