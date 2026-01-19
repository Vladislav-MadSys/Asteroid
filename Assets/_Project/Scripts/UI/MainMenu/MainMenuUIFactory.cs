using _Project.Scripts.Addressables;
using _Project.Scripts.Factories;
using _Project.Scripts.Low;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.MainMenu
{
    public class MainMenuUIFactory : IInitializable
    {
        private SceneController _sceneController;
        private IResourcesService _resourcesService;

        private MainMenuUIModel _model;
        private MainMenuUIView _view;
        private MainMenuUIPresenter _presenter;
    
        [Inject]
        private void Inject(SceneController sceneController, IResourcesService resourcesService)
        {
            _sceneController = sceneController;
            _resourcesService = resourcesService;
        }

        public async void Initialize()
        {
            _model = new MainMenuUIModel();
            _presenter = new MainMenuUIPresenter();
            GameObject uiPrefab = await _resourcesService.Load<GameObject>(AddressablesKeys.MAIN_MENU_CANVAS);
            GameObjectFactory gameObjectFactory = new GameObjectFactory(uiPrefab);
            GameObject ui = gameObjectFactory.Create();
            _view = ui.GetComponent<MainMenuUIView>();

            _model.Initialize();
            _view.Initialize(_presenter, _resourcesService);
            _presenter.Initialize(_view, _model, _sceneController, _resourcesService);
        }
    }
}
