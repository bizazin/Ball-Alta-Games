

using Entitas;

namespace Scripts.Ecs.Utils
{
    public interface ILinkable : IEntityHashHolder
    {
        bool IsLinked { get; }
        void Link(IEntity entity, IContext context);
        void Destroy();
    }
}