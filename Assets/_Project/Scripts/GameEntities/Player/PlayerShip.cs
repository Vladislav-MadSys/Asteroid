using _Project.Scripts.Addressables;
using _Project.Scripts.GameEntities.Player.Weapon;
using _Project.Scripts.Low;
using _Project.Scripts.Saves;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEntities.Player
{
    public class PlayerShip : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController _playerMovementController;
        [SerializeField] private MachinegunWeapon _machinegunWeapon;
        [SerializeField] private Laser _laser;
        
        private GameSessionData _gameSessionData;

        public void Initialize(
            GameSessionData gameSessionData, 
            IResourcesService resourcesService,
            PlayerInputHandler playerInputHandler,
            PlayerStates playerStates,
            SceneSaveController sceneSaveController)
        {
            _gameSessionData = gameSessionData;
            
            _playerMovementController.Initialize(playerInputHandler, playerStates, sceneSaveController);
            _machinegunWeapon.Initialize(playerInputHandler, gameSessionData, resourcesService);
            _laser.Initialize(playerInputHandler, playerStates, gameSessionData);
        }

        public void SetPlayerPosition(Vector2 position)
        {
            _playerMovementController.SetPlayerPosition(position);
        }
        public void SetPlayerRotation(float rotation)
        {
            _playerMovementController.SetPlayerRotation(rotation);
        }
        
        public void KillPlayer()
        {
            _gameSessionData.KillPlayer();
            Destroy(gameObject);
        }
        
    }
}
