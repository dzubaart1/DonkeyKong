using System;
using UnityEngine;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        public event Action JumpEvent;
        public event Action<Vector2> RunEvent;
    
        private PlayerInput _playerInput;
        private PlayerInput.PlayerActions _playerActions;
    
        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerActions = _playerInput.Player;
        }

        private void Update()
        {
            if (_playerActions.Jump.triggered)
            {
                JumpEvent?.Invoke();
            }
        
            RunEvent?.Invoke(_playerActions.Run.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }
    }
}
