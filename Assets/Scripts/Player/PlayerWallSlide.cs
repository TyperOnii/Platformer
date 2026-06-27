using UnityEngine;

namespace Platformer.Mechanics
{
    public class PlayerWallSlide : MonoBehaviour
    {
        [SerializeField] private float slideSpeed = -1f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            SlideOnWall();
        }

        private void SlideOnWall()
        {
            if (_rb.linearVelocity.y > 0f || Player.Instance.IsGrounded) return;

            float moveVectorX = InputManager.Instance.InputVector.x;

            if (Player.Instance.IsOnWall && moveVectorX != 0)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, slideSpeed);
            }
        }
    }
}
