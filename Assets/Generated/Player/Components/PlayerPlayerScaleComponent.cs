//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class PlayerContext {

    public PlayerEntity playerScaleEntity { get { return GetGroup(PlayerMatcher.PlayerScale).GetSingleEntity(); } }
    public Scripts.Ecs.Player.Components.PlayerScaleComponent playerScale { get { return playerScaleEntity.playerScale; } }
    public bool hasPlayerScale { get { return playerScaleEntity != null; } }

    public PlayerEntity SetPlayerScale(UnityEngine.Vector3 newValue) {
        if (hasPlayerScale) {
            throw new Entitas.EntitasException("Could not set PlayerScale!\n" + this + " already has an entity with Scripts.Ecs.Player.Components.PlayerScaleComponent!",
                "You should check if the context already has a playerScaleEntity before setting it or use context.ReplacePlayerScale().");
        }
        var entity = CreateEntity();
        entity.AddPlayerScale(newValue);
        return entity;
    }

    public void ReplacePlayerScale(UnityEngine.Vector3 newValue) {
        var entity = playerScaleEntity;
        if (entity == null) {
            entity = SetPlayerScale(newValue);
        } else {
            entity.ReplacePlayerScale(newValue);
        }
    }

    public void RemovePlayerScale() {
        playerScaleEntity.Destroy();
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

    public Scripts.Ecs.Player.Components.PlayerScaleComponent playerScale { get { return (Scripts.Ecs.Player.Components.PlayerScaleComponent)GetComponent(PlayerComponentsLookup.PlayerScale); } }
    public bool hasPlayerScale { get { return HasComponent(PlayerComponentsLookup.PlayerScale); } }

    public void AddPlayerScale(UnityEngine.Vector3 newValue) {
        var index = PlayerComponentsLookup.PlayerScale;
        var component = (Scripts.Ecs.Player.Components.PlayerScaleComponent)CreateComponent(index, typeof(Scripts.Ecs.Player.Components.PlayerScaleComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePlayerScale(UnityEngine.Vector3 newValue) {
        var index = PlayerComponentsLookup.PlayerScale;
        var component = (Scripts.Ecs.Player.Components.PlayerScaleComponent)CreateComponent(index, typeof(Scripts.Ecs.Player.Components.PlayerScaleComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePlayerScale() {
        RemoveComponent(PlayerComponentsLookup.PlayerScale);
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

    static Entitas.IMatcher<PlayerEntity> _matcherPlayerScale;

    public static Entitas.IMatcher<PlayerEntity> PlayerScale {
        get {
            if (_matcherPlayerScale == null) {
                var matcher = (Entitas.Matcher<PlayerEntity>)Entitas.Matcher<PlayerEntity>.AllOf(PlayerComponentsLookup.PlayerScale);
                matcher.componentNames = PlayerComponentsLookup.componentNames;
                _matcherPlayerScale = matcher;
            }

            return _matcherPlayerScale;
        }
    }
}