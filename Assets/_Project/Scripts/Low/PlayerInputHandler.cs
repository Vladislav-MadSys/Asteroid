using System;
using UnityEngine;
using Zenject;

namespace AsteroidGame
{
    public class PlayerInputHandler : ITickable, IInitializable, IDisposable
    {
        public Vector2 JoyInput { get; private set; }
        public bool isFireButtonPressed { get; private set; }
        public bool isFire2ButtonPressed { get; private set; }

        private PlayerInput _playerInput;

        [Inject]
        void Inject(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public void Initialize()
        {
            _playerInput.Enable();
        }

        public void Tick()
        {
            JoyInput = _playerInput.Player.Move.ReadValue<Vector2>();
            isFireButtonPressed = _playerInput.Player.Attack.IsPressed();
            isFire2ButtonPressed = _playerInput.Player.Attack_2.IsPressed();
        }

        public void Dispose()
        {
            _playerInput.Disable();
        }
    }
}