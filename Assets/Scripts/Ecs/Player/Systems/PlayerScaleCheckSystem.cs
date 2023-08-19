using System.Collections.Generic;
using Entitas;
using Scripts.Controllers;
using Scripts.Controllers.Impls;
using Scripts.Databases;
using Scripts.Enums;

namespace Scripts.Ecs.Player.Systems
{
    public class PlayerScaleCheckSystem : ReactiveSystem<PlayerEntity>
    {
        private readonly IPlayerSettingsDatabase _playerSettingsDatabase;
        private readonly IGameResultController _gameResultController;

        public PlayerScaleCheckSystem
        (
            PlayerContext playerContext,
            IPlayerSettingsDatabase playerSettingsDatabase,
            IGameResultController gameResultController
        ) : base(playerContext)
        {
            _playerSettingsDatabase = playerSettingsDatabase;
            _gameResultController = gameResultController;
        }

        protected override ICollector<PlayerEntity> GetTrigger(IContext<PlayerEntity> context) =>
            context.CreateCollector(PlayerMatcher.PlayerScale);

        protected override bool Filter(PlayerEntity entity) =>
            entity.hasPlayerScale && !entity.isDestroyed &&
            entity.playerScale.Value.x / entity.playerMaxScale.Value.x <
            _playerSettingsDatabase.Settings.ThresholdPercentage;

        protected override void Execute(List<PlayerEntity> entities) =>
            _gameResultController.SetResult(EGameResultType.Lose);
    }
}