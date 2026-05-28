using UnityEngine;

namespace Yandere.Player
{
    using Input;

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private Animator _animator;
        private CharacterController _characterController;

        private float _turnVelocity;

        [Header("Rotation")]
        [SerializeField]
        [Range(0f, 1f)]
        private float _turnSpeed = 0.2f;

        [Header("Movement")]
        [SerializeField]
        [Range(0f, 10f)]
        private float _moveSpeed = 2f;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Vector2 moveInput = InputHandler.Move;
            Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

            if (moveDirection.magnitude >= 0.1f)
            {
                // Rotation
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnVelocity, _turnSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Movement
                moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _characterController.Move(moveDirection.normalized * (_moveSpeed * Time.deltaTime));
            }
        }
    }
}
