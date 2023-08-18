using Entitas;
using Entitas.CodeGeneration.Attributes;
using Scripts.Enums;

namespace Scripts.Ecs.Player.Components
{
    [Player, Unique]
    public class StateComponent : IComponent
    {
        public EPlayerState Value;
    }
}