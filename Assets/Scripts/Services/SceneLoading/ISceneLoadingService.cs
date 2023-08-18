using System;

namespace Scripts.Services.SceneLoading
{
    public interface ISceneLoadingService
    {
        void Load(string name, Action onLoaded = null);
    }
}