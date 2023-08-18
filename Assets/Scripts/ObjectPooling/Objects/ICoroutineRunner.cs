using System.Collections;
using UnityEngine;

namespace Scripts.ObjectPooling.Objects
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}