using System;
using _Project.Scripts.Addressables;
using _Project.Scripts.Factories;
using _Project.Scripts.Low;
using _Project.Scripts.Low.SceneController;
using _Project.Scripts.Saves;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.MainMenu
{
    public class MainMenuUIFactory : IInitializable, IDisposable
    {
        private ISceneController _sceneController;
        private IResourcesService _resourcesService;
        private ISaveSystem _saveSystem;
        
        private MainMenuUIModel _model;
        private MainMenuUIView _view;
        private MainMenuUIPresenter _presenter;
    
        public MainMenuUIFactory(ISceneController sceneController, IResourcesService resourcesService, ISaveSystem saveSystem)
        {
            _sceneController = sceneController;
            _resourcesService = resourcesService;
            _saveSystem = saveSystem;
        }

        public async void Initialize()
        {
            _model = new MainMenuUIModel();
            _presenter = new MainMenuUIPresenter();
            GameObject uiPrefab = await _resourcesService.Load<GameObject>(AddressablesKeys.MAIN_MENU_CANVAS);
            GameObjectFactory gameObjectFactory = new GameObjectFactory(uiPrefab);
            GameObject ui = gameObjectFactory.Create();
            _view = ui.GetComponent<MainMenuUIView>();

            _model.Initialize(_saveSystem);
            _view.Initialize(_presenter, _resourcesService);
            _presenter.Initialize(_view, _model, _sceneController, _resourcesService);
        }

        public void Dispose()
        {
            _model.Dispose();
            _presenter.Dispose();
        }
    }
}
