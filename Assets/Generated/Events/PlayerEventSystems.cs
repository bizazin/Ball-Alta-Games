//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class PlayerEventSystems : Feature {

    public PlayerEventSystems(Contexts contexts) {
        Add(new PlayerScaleEventSystem(contexts)); // priority: 0
    }
}
