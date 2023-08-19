using System.Collections;
using DG.Tweening;
using Scripts.Behaviours;
using Scripts.Behaviours.Impls;
using Scripts.Controllers;
using Scripts.Databases;
using Scripts.Enums;
using UnityEngine;

namespace Scripts.Services.Impls
{
    public class VictoryAnimationService : IVictoryAnimationService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPlayerAnimationSettingsDatabase _playerAnimationSettingsDatabase;
        private readonly IDoorAnimationSettingsDatabase _doorAnimationSettingsDatabase;
        private readonly IGameResultController _gameResultController;

        public VictoryAnimationService
        (
            ICoroutineRunner coroutineRunner,
            IPlayerAnimationSettingsDatabase playerAnimationSettingsDatabase,
            IDoorAnimationSettingsDatabase doorAnimationSettingsDatabase,
            IGameResultController gameResultController
        )
        {
            _coroutineRunner = coroutineRunner;
            _playerAnimationSettingsDatabase = playerAnimationSettingsDatabase;
            _doorAnimationSettingsDatabase = doorAnimationSettingsDatabase;
            _gameResultController = gameResultController;
        }
        
            public void PlayVictoryAnimations(PlayerBehaviour player, DoorBehaviour door)
            {
                var doorPos = door.transform.position;
                var playerPos = player.transform.position;
                var directionToDoor = (doorPos - playerPos).normalized;
                var distance = Vector3.Distance(playerPos, doorPos);
                var jumpDistance = distance / _playerAnimationSettingsDatabase.Settings.PlayerJumpsCount;
                var initialY = playerPos.y;
                var jumpSequence = DOTween.Sequence();
                var isDoorOpened = false;

                player.SetPathActive(false);
                
                for (var i = 0; i < _playerAnimationSettingsDatabase.Settings.PlayerJumpsCount; i++)
                {
                    var midPoint = player.transform.position + directionToDoor * jumpDistance * (i + 1);
                    midPoint.y += _playerAnimationSettingsDatabase.Settings.JumpHeight;

                    jumpSequence
                        .Append(player.transform.DOJump(midPoint, _playerAnimationSettingsDatabase.Settings.JumpHeight, 1, _playerAnimationSettingsDatabase.Settings.JumpDurationS).SetEase(Ease.OutQuad)
                        .OnStepComplete(() =>
                        {
                            var currentDistance = Vector3.Distance(player.transform.position, door.transform.position);
                            if (currentDistance <= _doorAnimationSettingsDatabase.Settings.DoorOpenDistanceM && !isDoorOpened)
                            {
                                door.Open(_doorAnimationSettingsDatabase.Settings.DoorOpenDurationS);
                                isDoorOpened = true;
                            }
                        }));
                }

                jumpSequence.OnComplete(() =>
                {
                    var finalPosition = door.transform.position;
                    finalPosition.y = initialY; 
                    player.transform.position = finalPosition;
                    _gameResultController.SetResult(EGameResultType.Win);
                });
            
        }
    }
}