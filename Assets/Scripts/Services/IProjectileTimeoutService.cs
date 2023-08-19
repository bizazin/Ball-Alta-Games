using System.Collections;

namespace Scripts.Services
{
    public interface IProjectileTimeoutService
    {
        void StartCoroutine(IEnumerator projectileTimeout);
        void StopCoroutine();
    }
}