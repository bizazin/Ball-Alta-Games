//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ProjectileEntity {

    public ProjectileScaleListenerComponent projectileScaleListener { get { return (ProjectileScaleListenerComponent)GetComponent(ProjectileComponentsLookup.ProjectileScaleListener); } }
    public bool hasProjectileScaleListener { get { return HasComponent(ProjectileComponentsLookup.ProjectileScaleListener); } }

    public void AddProjectileScaleListener(System.Collections.Generic.List<IProjectileScaleListener> newValue) {
        var index = ProjectileComponentsLookup.ProjectileScaleListener;
        var component = (ProjectileScaleListenerComponent)CreateComponent(index, typeof(ProjectileScaleListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceProjectileScaleListener(System.Collections.Generic.List<IProjectileScaleListener> newValue) {
        var index = ProjectileComponentsLookup.ProjectileScaleListener;
        var component = (ProjectileScaleListenerComponent)CreateComponent(index, typeof(ProjectileScaleListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveProjectileScaleListener() {
        RemoveComponent(ProjectileComponentsLookup.ProjectileScaleListener);
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
public sealed partial class ProjectileMatcher {

    static Entitas.IMatcher<ProjectileEntity> _matcherProjectileScaleListener;

    public static Entitas.IMatcher<ProjectileEntity> ProjectileScaleListener {
        get {
            if (_matcherProjectileScaleListener == null) {
                var matcher = (Entitas.Matcher<ProjectileEntity>)Entitas.Matcher<ProjectileEntity>.AllOf(ProjectileComponentsLookup.ProjectileScaleListener);
                matcher.componentNames = ProjectileComponentsLookup.componentNames;
                _matcherProjectileScaleListener = matcher;
            }

            return _matcherProjectileScaleListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ProjectileEntity {

    public void AddProjectileScaleListener(IProjectileScaleListener value) {
        var listeners = hasProjectileScaleListener
            ? projectileScaleListener.value
            : new System.Collections.Generic.List<IProjectileScaleListener>();
        listeners.Add(value);
        ReplaceProjectileScaleListener(listeners);
    }

    public void RemoveProjectileScaleListener(IProjectileScaleListener value, bool removeComponentWhenEmpty = true) {
        var listeners = projectileScaleListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveProjectileScaleListener();
        } else {
            ReplaceProjectileScaleListener(listeners);
        }
    }
}
