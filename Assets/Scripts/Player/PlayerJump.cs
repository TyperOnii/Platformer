using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 14f;
        [SerializeField] private float fallMultiplier = 4f;
        [SerializeField] private float lowJumpMultiplier = 5f;

        [Header("Coyote time")]
        [SerializeField] private float coyoteTime = 0.15f;
        private float coyoteTimeState = 0f;

        [Header("Jump buffer time")]
        [SerializeField] private float jumpBufferTime = 0.15f;
        private float jumpBufferState = 0f;

        private bool isJumpPressed;

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
            isJumpPressed = JumpIsPressed();

            if (jumpBufferState > -jumpBufferTime)
            {
                jumpBufferState -= Time.deltaTime;
            }

            if (Player.Instance.IsGrounded)
            {
                coyoteTimeState = coyoteTime;
            }
            else
            {
                coyoteTimeState -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            if (Player.Instance.IsGrounded) return;

            if (_rb.linearVelocity.y < 0)
            {
                _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }

            if (!isJumpPressed && _rb.linearVelocity.y > 0)
            {
                _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            jumpBufferState = jumpBufferTime;

            if (coyoteTimeState > 0f || (Player.Instance.IsGrounded && jumpBufferState > 0f))
            {
                Jump();
                coyoteTimeState = 0f;
                jumpBufferState = 0f;
            }
        }

        private void Jump()
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
        }

        private bool JumpIsPressed()
        {
            return InputManager.Instance.Actions.Player.Jump.IsInProgress();
        }
    }
}
