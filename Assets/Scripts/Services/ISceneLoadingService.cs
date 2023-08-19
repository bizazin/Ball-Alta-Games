using System;

namespace Scripts.Services
{
    public interface ISceneLoadingService
    {
        void Load(string name, Action onLoaded = null);
    }
}