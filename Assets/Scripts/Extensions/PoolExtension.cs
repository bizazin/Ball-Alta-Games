using Scripts.ObjectPooling.Core;
using UnityEngine;

namespace Scripts.Extensions
{
    public static class PoolExtension
    {
        public static T SpawnAndActivate<T>(this IPool<T> pool, Transform spawnPoint) where T : MonoBehaviour
        {
            var instance = pool.Spawn(spawnPoint);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public static void DespawnAndDeactivate<T>(this IPool<T> pool, T item) where T : MonoBehaviour
        {
            pool.Despawn(item);
            item.gameObject.SetActive(false);
        }
    }
}