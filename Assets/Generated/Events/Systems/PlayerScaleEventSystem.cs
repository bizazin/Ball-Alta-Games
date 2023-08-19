//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class PlayerScaleEventSystem : Entitas.ReactiveSystem<PlayerEntity> {

    readonly System.Collections.Generic.List<IPlayerScaleListener> _listenerBuffer;

    public PlayerScaleEventSystem(Contexts contexts) : base(contexts.player) {
        _listenerBuffer = new System.Collections.Generic.List<IPlayerScaleListener>();
    }

    protected override Entitas.ICollector<PlayerEntity> GetTrigger(Entitas.IContext<PlayerEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(PlayerMatcher.PlayerScale)
        );
    }

    protected override bool Filter(PlayerEntity entity) {
        return entity.hasPlayerScale && entity.hasPlayerScaleListener;
    }

    protected override void Execute(System.Collections.Generic.List<PlayerEntity> entities) {
        foreach (var e in entities) {
            var component = e.playerScale;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.playerScaleListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnPlayerScale(e, component.Value);
            }
        }
    }
}
