//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ContextMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class CommonMatcher {

    public static Entitas.IAllOfMatcher<CommonEntity> AllOf(params int[] indices) {
        return Entitas.Matcher<CommonEntity>.AllOf(indices);
    }

    public static Entitas.IAllOfMatcher<CommonEntity> AllOf(params Entitas.IMatcher<CommonEntity>[] matchers) {
          return Entitas.Matcher<CommonEntity>.AllOf(matchers);
    }

    public static Entitas.IAnyOfMatcher<CommonEntity> AnyOf(params int[] indices) {
          return Entitas.Matcher<CommonEntity>.AnyOf(indices);
    }

    public static Entitas.IAnyOfMatcher<CommonEntity> AnyOf(params Entitas.IMatcher<CommonEntity>[] matchers) {
          return Entitas.Matcher<CommonEntity>.AnyOf(matchers);
    }
}
