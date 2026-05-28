using UnityEngine;
using UnityEngine.InputSystem;

namespace Yandere.Input
{
    using Actions;

    public class InputHandler : MonoBehaviour
    {
        public static Vector2 Move { get; private set; } = Vector2.zero;

        private GameInputActions _actions;

        private void Awake() => _actions = new GameInputActions();

        private void OnEnable() => _actions.Enable();

        private void OnDisable() => _actions.Disable();

        private void OnDestroy()
        {
            _actions.Player.Move.performed -= OnPlayerMovePerformed;
            _actions.Player.Move.canceled -= OnPlayerMoveCanceled;
        }

        private void Start()
        {
            _actions.Player.Move.performed += OnPlayerMovePerformed;
            _actions.Player.Move.canceled += OnPlayerMoveCanceled;
        }

        private void OnPlayerMovePerformed(InputAction.CallbackContext context) => Move = context.ReadValue<Vector2>();

        private void OnPlayerMoveCanceled(InputAction.CallbackContext context) => Move = Vector2.zero;
    }
}
