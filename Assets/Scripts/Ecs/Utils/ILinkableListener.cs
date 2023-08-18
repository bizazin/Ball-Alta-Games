

using Entitas;

namespace Scripts.Ecs.Utils
{
    public interface ILinkableListener
    {
        void Listen(IEntity entity);
        void Unlisten(IEntity entity);
        void Unlink();
        void Clear();
    }
}