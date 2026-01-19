using _Project.Scripts.Low;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Scripts.Bootstrap
{
    public class BootstrapScene : MonoBehaviour
    {
        private SceneController _sceneController;
        
        [Inject]
        private void Inject(SceneController sceneController)
        {
            _sceneController = sceneController;
        }
        private void Start()
        {
            _sceneController.LoadSceneWithKey("MainMenu");
        }
    }
}
