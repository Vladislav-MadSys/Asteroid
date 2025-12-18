using System; 
using System.Linq; 
using System.Collections.Generic;
using _Project.Scripts.Low;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI
{
    public class UIPresenter
    {
        protected SceneController _sceneController;
        protected UIModel _model;

        private Action onRestart;

        [Inject]
        private void Inject(SceneController sceneController, UIModel model)
        {
            _sceneController = sceneController;
            _model = model;
        }

        public void OnRestartButtonClicked()
        {
            _sceneController.ReloadCurrentScene();
        }
    }
}