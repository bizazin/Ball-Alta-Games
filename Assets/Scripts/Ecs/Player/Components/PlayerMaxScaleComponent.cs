using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Scripts.Ecs.Player.Components
{
    [Player, Unique]
    public class PlayerMaxScaleComponent : IComponent
    {
        public Vector3 Value;
    }
}