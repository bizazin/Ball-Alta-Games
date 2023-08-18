using System.Collections;

namespace Scripts.Services.ProjectileTimeout
{
    public interface IProjectileTimeoutService
    {
        void StartCoroutine(IEnumerator projectileTimeout);
        void StopCoroutine();
    }
}