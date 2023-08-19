using System.Collections;
using Scripts.Behaviours;
using Scripts.Behaviours.Impls;
using Scripts.Databases;
using UnityEngine;

namespace Scripts.Services.Impls
{
    public class VictoryAnimationService : IVictoryAnimationService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPlayerAnimationSettingsDatabase _playerAnimationSettingsDatabase;
        private readonly IDoorAnimationSettingsDatabase _doorAnimationSettingsDatabase;

        public VictoryAnimationService
        (
            ICoroutineRunner coroutineRunner,
            IPlayerAnimationSettingsDatabase playerAnimationSettingsDatabase,
            IDoorAnimationSettingsDatabase doorAnimationSettingsDatabase
        )
        {
            _coroutineRunner = coroutineRunner;
            _playerAnimationSettingsDatabase = playerAnimationSettingsDatabase;
            _doorAnimationSettingsDatabase = doorAnimationSettingsDatabase;
        }

        public void PlayVictoryAnimations(PlayerBehaviour player, DoorBehaviour door)
        {
            _coroutineRunner.StartCoroutine(JumpRoutine(player, door,
                _playerAnimationSettingsDatabase.Settings.PlayerJumpsCount));
        }

        private IEnumerator JumpRoutine(PlayerBehaviour player, DoorBehaviour door, int numberOfJumps)
        {
            var start = player.transform.position;
            var targetPoint = door.transform.position;
            var distanceBetweenJumps = Vector3.Distance(start, targetPoint) / numberOfJumps;

            for (var i = 0; i < numberOfJumps; i++)
            {
                var nextJumpTarget = Vector3.MoveTowards(start, targetPoint, distanceBetweenJumps * (i + 1));
                var peakHeight = Mathf.Max(start.y, nextJumpTarget.y) +
                                 _playerAnimationSettingsDatabase.Settings.JumpHeight;

                while (player.transform.position.y < peakHeight)
                {
                    player.SetVelocity(new Vector3(0, _playerAnimationSettingsDatabase.Settings.JumpForce, 0));
                    yield return null;
                }

                while (player.transform.position.y > nextJumpTarget.y)
                {
                    player.SetVelocity(new Vector3(0, _playerAnimationSettingsDatabase.Settings.Gravity, 0));
                    yield return null;
                }

                player.transform.position = new Vector3(nextJumpTarget.x, nextJumpTarget.y, nextJumpTarget.z);
            }

            player.SetVelocity(Vector3.zero);

            float distanceToDoor = Vector3.Distance(player.transform.position, targetPoint);
            if (distanceToDoor < _doorAnimationSettingsDatabase.Settings.DoorOpenDistanceM)
                door.Open(_doorAnimationSettingsDatabase.Settings.DoorOpenDurationS,
                    _doorAnimationSettingsDatabase.Settings.DoorOpenAngleDeg);
        }
    }
}