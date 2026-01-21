using _Project.Scripts.Addressables;
using _Project.Scripts.Factories;
using _Project.Scripts.GameEntities.Player;
using _Project.Scripts.Low;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.Gameplay
{
    public class PlayerHUDFactory : IInitializable
    {
        protected SceneController _sceneController;
        private PlayerStates _playerStates;
        private GameSessionData _gameSessionData;
        private IResourcesService _resourcesService;
        private IAdvertisement _advertisement;
        private PlayerFactory _playerFactory;
    
        protected PlayerStatsHudModel _model;
        protected PlayerStatsHudView _view;
        protected PlayerStatsHudPresenter _presenter;
    
        [Inject]
        private void Inject(
            SceneController sceneController, 
            PlayerStates playerStates, 
            GameSessionData gameSessionData, 
            IResourcesService resourcesService,
            IAdvertisement advertisement,
            PlayerFactory playerFactory)
        {
            _sceneController = sceneController;
            _playerStates = playerStates;
            _gameSessionData = gameSessionData;
            _resourcesService = resourcesService;
            _advertisement = advertisement;
            _playerFactory = playerFactory;
        }
    
        public async void Initialize()
        {
            _model = new PlayerStatsHudModel();
            _presenter = new PlayerStatsHudPresenter();
            var hudPrefab = await _resourcesService.Load<GameObject>(AddressablesKeys.PLAYER_HUD_CANVAS);
            GameObjectFactory gameObjectFactory = new GameObjectFactory(hudPrefab);
            GameObject hud = gameObjectFactory.Create();
            _view = hud.GetComponent<PlayerStatsHudView>();
        
            _model.Initialize(_playerStates, _gameSessionData);
            _presenter.Initialize(_sceneController, _advertisement, _playerFactory, _model, _view);
            _view.Initialize(_presenter);
        }

    }
}
