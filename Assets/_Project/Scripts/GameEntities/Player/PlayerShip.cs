using System;
using System.Threading;
using _Project.Scripts.Addressables;
using _Project.Scripts.GameEntities.Player.Weapon;
using _Project.Scripts.Low;
using _Project.Scripts.Saves;
using _Project.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEntities.Player
{
    public class PlayerShip : MonoBehaviour
    {
        private const int INVENCIBLE_TIME = 5000;
        [SerializeField] private PlayerMovementController _playerMovementController;
        [SerializeField] private MachinegunWeapon _machinegunWeapon;
        [SerializeField] private Laser _laser;
        [SerializeField] private GameObject _body;
        [SerializeField] private SpriteRenderer _bodySprite;
        
        private GameSessionData _gameSessionData;
        private IAdvertisement _advertisement;
        private ConfigData _configData;
        private CancellationTokenSource _cancellationToken;
        private bool _isInvencible = false;
        private Color _baseColor;
        private Color _invencibleColor = new Color(1, 1, 1, 0.2f);
        
        public bool IsDead { get; private set; } = false;
        
        
        public void Initialize(
            GameSessionData gameSessionData, 
            IResourcesService resourcesService,
            PlayerInputHandler playerInputHandler,
            PlayerStates playerStates,
            SceneSaveController sceneSaveController,
            ConfigData configData)
        {
            _gameSessionData = gameSessionData;
            _configData = configData;
            
            _playerMovementController.Initialize(playerInputHandler, playerStates, sceneSaveController, configData);
            _machinegunWeapon.Initialize(playerInputHandler, gameSessionData, resourcesService, configData);
            _laser.Initialize(playerInputHandler, playerStates, gameSessionData, configData);

            _baseColor = _bodySprite.color;
        }

        private void OnDestroy()
        {
            if (_cancellationToken != null)
            {
                _cancellationToken.Cancel();
            }
        }

        public void SetPlayerPosition(Vector2 position)
        {
            _playerMovementController.SetPlayerPosition(position);
        }
        public void SetPlayerRotation(float rotation)
        {
            _playerMovementController.SetPlayerRotation(rotation);
        }
        
        public async void RespawnPlayer()
        {
            _body.SetActive(true);
            _playerMovementController.ControlEnabled = true;
            _machinegunWeapon.ControlEnabled = true;
            _laser.ControlEnabled = true;
            IsDead = false;
            
            _cancellationToken = new CancellationTokenSource();
            await UniTask.Delay(INVENCIBLE_TIME, cancellationToken: _cancellationToken.Token);
            SwitchOffInvencible();
        }
        
        public void KillPlayer()
        {
            if(_isInvencible) return;
            
            _gameSessionData.KillPlayer();
            _body.SetActive(false);
            _playerMovementController.ControlEnabled = false;
            _machinegunWeapon.ControlEnabled = false;
            _laser.ControlEnabled = false;
            _isInvencible = true;
            _bodySprite.color = _invencibleColor;
            IsDead = true;
            
        }

        private void SwitchOffInvencible()
        {
            _isInvencible = false;
            _bodySprite.color = _baseColor;
        }
    }
}
