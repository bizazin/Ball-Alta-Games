using Scripts.Enums;

namespace Scripts.Ecs.Player.Utils
{
    public static class PlayerExtensions
    {
        public static PlayerEntity CreatePlayer(this PlayerContext context)
        {
            var entity = context.CreateEntity();
            
            entity.AddState(EPlayerState.ReadyForLoad);
            
            return entity;
        }
    }
}