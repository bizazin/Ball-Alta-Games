using Scripts.Services;
using Zenject;

namespace Scripts.Core
{
    public class MainBootstrap : IInitializable
    {
        private readonly ISceneLoadingService _sceneLoadingService;

        public MainBootstrap
        (               
            ISceneLoadingService sceneLoadingService
        )
        {
            _sceneLoadingService = sceneLoadingService;
        }

        public void Initialize() => _sceneLoadingService.Load("Main");
    }
}