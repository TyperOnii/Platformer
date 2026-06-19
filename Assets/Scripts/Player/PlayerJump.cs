using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 14f;
        [SerializeField] private float fallMultiplier = 3f;
        [SerializeField] private float lowJumpMultiplier = 4f;

        private bool jumpIsInProgress;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            InputManager.Instance.Actions.Player.Jump.started += OnJump;
        }

        private void OnDestroy()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.Actions.Player.Jump.started -= OnJump;
            }
        }

        private void Update()
        {
            jumpIsInProgress = InputManager.Instance.Actions.Player.Jump.IsInProgress();
        }

        private void FixedUpdate()
        {
            if (Player.Instance.IsGrounded) return;

            if (_rb.linearVelocity.y < 0)
            {
                _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }

            if (!jumpIsInProgress && _rb.linearVelocity.y > 0)
            {
                _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            if (!Player.Instance.IsGrounded) return;

            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
        }
    }
}
