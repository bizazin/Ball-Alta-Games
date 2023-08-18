using UnityEngine;
using Zenject;

namespace Scripts.ObjectPooling.Core
{
    public interface IPool<T> : IMemoryPool<Transform, T> where T : MonoBehaviour
    {
    }
}