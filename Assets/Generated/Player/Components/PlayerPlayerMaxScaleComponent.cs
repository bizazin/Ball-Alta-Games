//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class PlayerContext {

    public PlayerEntity playerMaxScaleEntity { get { return GetGroup(PlayerMatcher.PlayerMaxScale).GetSingleEntity(); } }
    public Scripts.Ecs.Player.Components.PlayerMaxScaleComponent playerMaxScale { get { return playerMaxScaleEntity.playerMaxScale; } }
    public bool hasPlayerMaxScale { get { return playerMaxScaleEntity != null; } }

    public PlayerEntity SetPlayerMaxScale(UnityEngine.Vector3 newValue) {
        if (hasPlayerMaxScale) {
            throw new Entitas.EntitasException("Could not set PlayerMaxScale!\n" + this + " already has an entity with Scripts.Ecs.Player.Components.PlayerMaxScaleComponent!",
                "You should check if the context already has a playerMaxScaleEntity before setting it or use context.ReplacePlayerMaxScale().");
        }
        var entity = CreateEntity();
        entity.AddPlayerMaxScale(newValue);
        return entity;
    }

    public void ReplacePlayerMaxScale(UnityEngine.Vector3 newValue) {
        var entity = playerMaxScaleEntity;
        if (entity == null) {
            entity = SetPlayerMaxScale(newValue);
        } else {
            entity.ReplacePlayerMaxScale(newValue);
        }
    }

    public void RemovePlayerMaxScale() {
        playerMaxScaleEntity.Destroy();
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

    public Scripts.Ecs.Player.Components.PlayerMaxScaleComponent playerMaxScale { get { return (Scripts.Ecs.Player.Components.PlayerMaxScaleComponent)GetComponent(PlayerComponentsLookup.PlayerMaxScale); } }
    public bool hasPlayerMaxScale { get { return HasComponent(PlayerComponentsLookup.PlayerMaxScale); } }

    public void AddPlayerMaxScale(UnityEngine.Vector3 newValue) {
        var index = PlayerComponentsLookup.PlayerMaxScale;
        var component = (Scripts.Ecs.Player.Components.PlayerMaxScaleComponent)CreateComponent(index, typeof(Scripts.Ecs.Player.Components.PlayerMaxScaleComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePlayerMaxScale(UnityEngine.Vector3 newValue) {
        var index = PlayerComponentsLookup.PlayerMaxScale;
        var component = (Scripts.Ecs.Player.Components.PlayerMaxScaleComponent)CreateComponent(index, typeof(Scripts.Ecs.Player.Components.PlayerMaxScaleComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePlayerMaxScale() {
        RemoveComponent(PlayerComponentsLookup.PlayerMaxScale);
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

    static Entitas.IMatcher<PlayerEntity> _matcherPlayerMaxScale;

    public static Entitas.IMatcher<PlayerEntity> PlayerMaxScale {
        get {
            if (_matcherPlayerMaxScale == null) {
                var matcher = (Entitas.Matcher<PlayerEntity>)Entitas.Matcher<PlayerEntity>.AllOf(PlayerComponentsLookup.PlayerMaxScale);
                matcher.componentNames = PlayerComponentsLookup.componentNames;
                _matcherPlayerMaxScale = matcher;
            }

            return _matcherPlayerMaxScale;
        }
    }
}