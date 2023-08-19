using Scripts.ObjectPooling.Objects;

namespace Scripts.Controllers
{
    public interface IMainSceneController
    {
        ProjectileBehaviour SpawnProjectile();
        void DetectEnemiesOnPath();
    }
}