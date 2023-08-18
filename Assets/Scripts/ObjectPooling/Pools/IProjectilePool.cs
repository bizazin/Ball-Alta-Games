using Scripts.ObjectPooling.Core;
using Scripts.ObjectPooling.Objects;

namespace Scripts.ObjectPooling.Pools
{
    public interface IProjectilePool : IPool<ProjectileBehaviour>
    {
        public void DespawnAndDeactivate();
    }
}