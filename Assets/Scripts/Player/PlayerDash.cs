using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer.Mechanics
{
    public class PlayerDash : MonoBehaviour
    {
        [SerializeField] private float dashForce = 10f;
        [SerializeField] private float dashDuration = 0.2f;

        public bool IsDashing { get; private set; }

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.Actions.Player.Dash.started += OnDash;
            }
        }

        private void OnDestroy()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.Actions.Player.Dash.started -= OnDash;
            }
        }

        private void FixedUpdate()
        {
            if (!IsDashing) return;

            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        }

        private void OnDash(InputAction.CallbackContext ctx)
        {
            StartCoroutine(StartDash());

            Vector2 dashDirection = InputManager.Instance.InputVector.x > 0f ? Vector2.right : Vector2.left;
            _rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        }

        private IEnumerator StartDash()
        {
            IsDashing = true;

            yield return new WaitForSeconds(dashDuration);

            IsDashing = false;
        }
    }
}
