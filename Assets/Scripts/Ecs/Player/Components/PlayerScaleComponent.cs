using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Scripts.Ecs.Player.Components
{
    [Player, Unique, Event(EventTarget.Self)]
    public class PlayerScaleComponent : IComponent
    {
        public Vector3 Value;
    }
}