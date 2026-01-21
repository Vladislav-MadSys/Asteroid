using System;
using _Project.Scripts.Addressables;
using _Project.Scripts.Factories;
using _Project.Scripts.Low;
using _Project.Scripts.Saves;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEntities.Player
{
    public class PlayerFactory : IInitializable, IDisposable
    {
        public event Action<PlayerShip> OnPlayerShipCreated;
        public PlayerShip PlayerShip { get; private set; }
        
        private IResourcesService _resourcesService;
        private GameSessionData _gameSessionData;
        private PlayerInputHandler _playerInputHandler;
        private PlayerStates _playerStates;
        private SceneSaveController _sceneSaveController;
        
        
        private Vector2 _playerTargetPosition;
        private float _playerTargetRotation;

        [Inject]
        private void Inject(
            GameSessionData gameSessionData, 
            IResourcesService resourcesService,
            PlayerInputHandler playerInputHandler,
            PlayerStates playerStates,
            SceneSaveController sceneSaveController,
            IAdvertisement advertisement)
        {
            _gameSessionData = gameSessionData;
            _resourcesService = resourcesService;
            _playerInputHandler = playerInputHandler;
            _playerStates = playerStates;
            _sceneSaveController = sceneSaveController;
        }

        public async void Initialize()
        {
            _sceneSaveController.OnSaveLoaded += OnSaveDataLoaded;
            
            var playerPrefab = await _resourcesService.Load<GameObject>(AddressablesKeys.PLAYER);
            GameObjectFactory gameObjectFactory = new GameObjectFactory(playerPrefab);
            PlayerShip = gameObjectFactory.Create().GetComponent<PlayerShip>();
            PlayerShip.Initialize(_gameSessionData, _resourcesService, _playerInputHandler, _playerStates, _sceneSaveController);
            PlayerShip.SetPlayerPosition(_playerTargetPosition);
            PlayerShip.SetPlayerRotation(_playerTargetRotation);
            OnPlayerShipCreated.Invoke(PlayerShip);
        }

        public void Dispose()
        {
            _sceneSaveController.OnSaveLoaded -= OnSaveDataLoaded;
        }

        private void OnSaveDataLoaded(SaveData saveData)
        {
            _playerTargetPosition = saveData.playerPosition;
            _playerTargetRotation = saveData.playerRotation;
        }
    }
}
