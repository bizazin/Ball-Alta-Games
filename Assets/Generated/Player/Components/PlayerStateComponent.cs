//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class PlayerContext {

    public PlayerEntity stateEntity { get { return GetGroup(PlayerMatcher.State).GetSingleEntity(); } }
    public Scripts.Ecs.Player.Components.StateComponent state { get { return stateEntity.state; } }
    public bool hasState { get { return stateEntity != null; } }

    public PlayerEntity SetState(Scripts.Enums.EPlayerState newValue) {
        if (hasState) {
            throw new Entitas.EntitasException("Could not set State!\n" + this + " already has an entity with Scripts.Ecs.Player.Components.StateComponent!",
                "You should check if the context already has a stateEntity before setting it or use context.ReplaceState().");
        }
        var entity = CreateEntity();
        entity.AddState(newValue);
        return entity;
    }

    public void ReplaceState(Scripts.Enums.EPlayerState newValue) {
        var entity = stateEntity;
        if (entity == null) {
            entity = SetState(newValue);
        } else {
            entity.ReplaceState(newValue);
        }
    }

    public void RemoveState() {
        stateEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class PlayerEntity {

    public Scripts.Ecs.Player.Components.StateComponent state { get { return (Scripts.Ecs.Player.Components.StateComponent)GetComponent(PlayerComponentsLookup.State); } }
    public bool hasState { get { return HasComponent(PlayerComponentsLookup.State); } }

    public void AddState(Scripts.Enums.EPlayerState newValue) {
        var index = PlayerComponentsLookup.State;
        var component = (Scripts.Ecs.Player.Components.StateComponent)CreateComponent(index, typeof(Scripts.Ecs.Player.Components.StateComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceState(Scripts.Enums.EPlayerState newValue) {
        var index = PlayerComponentsLookup.State;
        var component = (Scripts.Ecs.Player.Components.StateComponent)CreateComponent(index, typeof(Scripts.Ecs.Player.Components.StateComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveState() {
        RemoveComponent(PlayerComponentsLookup.State);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class PlayerMatcher {

    static Entitas.IMatcher<PlayerEntity> _matcherState;

    public static Entitas.IMatcher<PlayerEntity> State {
        get {
            if (_matcherState == null) {
                var matcher = (Entitas.Matcher<PlayerEntity>)Entitas.Matcher<PlayerEntity>.AllOf(PlayerComponentsLookup.State);
                matcher.componentNames = PlayerComponentsLookup.componentNames;
                _matcherState = matcher;
            }

            return _matcherState;
        }
    }
}
