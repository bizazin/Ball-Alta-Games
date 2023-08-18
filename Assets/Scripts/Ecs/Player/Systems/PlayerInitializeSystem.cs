using Entitas;
using Scripts.Ecs.Player.Utils;

namespace Scripts.Ecs.Player.Systems
{
    public class PlayerInitializeSystem : IInitializeSystem
    {
        private readonly PlayerContext _playerContext;

        public PlayerInitializeSystem
        (
            PlayerContext playerContext
        )
        {
            _playerContext = playerContext;
        }
        
        public void Initialize() => _playerContext.CreatePlayer();
    }
}