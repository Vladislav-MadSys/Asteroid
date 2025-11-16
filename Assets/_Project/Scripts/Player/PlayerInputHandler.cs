using UnityEngine;
using Zenject;

public class PlayerInputHandler : ITickable, IInitializable
{
    public Vector2 JoyInput { get; private set; }

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
    }
}
