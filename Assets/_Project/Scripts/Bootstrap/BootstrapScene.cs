using _Project.Scripts.Low;
using _Project.Scripts.Low.SceneController;
using Zenject;

namespace _Project.Scripts.Bootstrap
{
    public class BootstrapScene : IInitializable
    {
        private ISceneController _sceneController;
        
        public BootstrapScene(ISceneController sceneController)
        {
            _sceneController = sceneController;
        }
        public void Initialize()
        {
            _sceneController.LoadMainMenuScene();
        }
    }
}
