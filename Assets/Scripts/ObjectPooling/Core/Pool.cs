using UnityEngine;
using Zenject;

namespace Scripts.ObjectPooling.Core
{
    public class Pool<T> : MemoryPool<Transform, T>, IPool<T> where T : MonoBehaviour
    {
    }
}