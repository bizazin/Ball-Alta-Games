using System.Collections;
using Scripts.Behaviours;
using UnityEngine;

namespace Scripts.Services.ProjectileTimeout.Impls
{
    public class ProjectileTimeoutService : IProjectileTimeoutService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _currentCoroutine;

        public ProjectileTimeoutService
        (
            ICoroutineRunner coroutineRunner
        )
        {
            _coroutineRunner = coroutineRunner;
        }

        public void StartCoroutine(IEnumerator projectileTimeout)
        {
            _currentCoroutine = _coroutineRunner.StartCoroutine(projectileTimeout);
        }

        public void StopCoroutine() => _coroutineRunner.StopCoroutine(_currentCoroutine);
    }
}