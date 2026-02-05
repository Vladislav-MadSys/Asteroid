using System;
using _Project.Scripts.Low;
using _Project.Scripts.Low.SceneController;
using _Project.Scripts.Services;
using UnityEngine.VFX;
using Zenject;

namespace _Project.Scripts.Bootstrap
{
    public class BootstrapScene : IInitializable, IDisposable
    {
        private readonly ISceneController _sceneController;
        private AccessChecker _accessChecker;
        
        public BootstrapScene(ISceneController sceneController, AccessChecker accessChecker)
        {
            _sceneController = sceneController;
            _accessChecker = accessChecker;
        }
        public void Initialize()
        {
            _accessChecker.OnSeccessConnection += LoadGame;
        }

        public void Dispose()
        {
            _accessChecker.OnSeccessConnection -= LoadGame;
        }

        private void LoadGame()
        {
            _sceneController.LoadMainMenuScene();
        }
    }
}
