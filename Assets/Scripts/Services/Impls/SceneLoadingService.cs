﻿using System;
using System.Collections;
using Scripts.Behaviours;
using UnityEngine.SceneManagement;

namespace Scripts.Services.Impls
{
    public class SceneLoadingService : ISceneLoadingService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoadingService
        (
            ICoroutineRunner coroutineRunner
        )
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            var waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}