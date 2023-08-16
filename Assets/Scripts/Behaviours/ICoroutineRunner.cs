using System.Collections;
using UnityEngine;

namespace Behaviours
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}