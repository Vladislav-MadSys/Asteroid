using _Project.Scripts.Low;
using Zenject;

namespace _Project.Scripts.Bootstrap
{
    public class BootstrapScene : IInitializable
    {
        private SceneController _sceneController;
        
        public BootstrapScene(SceneController sceneController)
        {
            _sceneController = sceneController;
        }
        public void Initialize()
        {
            _sceneController.LoadSceneWithKey("MainMenu");
        }
    }
}
