using UnityEngine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 7f;

        [SerializeField] private PlayerDash playerDashScript;

        private const float ROTATION_DEAD_ZONE = 0.01f;

        private Rigidbody2D _rb;
        private SpriteRenderer _sr;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            if (playerDashScript.IsDashing) return;

            Move();
        }

        private void Move()
        {
            float moveDirection = InputManager.Instance.InputVector.x;

            _rb.linearVelocity = new Vector2(moveDirection * moveSpeed, _rb.linearVelocity.y);

            RotateByMovement(moveDirection);
        }

        private void RotateByMovement(float moveDirection)
        {
            //if (Player.Instance.IsOnWall) return;

            if (moveDirection > ROTATION_DEAD_ZONE)
            {
                _sr.flipX = false;
            }
            else if (moveDirection < -ROTATION_DEAD_ZONE)
            {
                _sr.flipX = true;
            }
        }
    }
}


