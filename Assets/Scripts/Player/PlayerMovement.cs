using UnityEngine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 7f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float moveVectorX = InputManager.Instance.InputVector.x;

            _rb.linearVelocity = new Vector2(moveVectorX * moveSpeed, _rb.linearVelocity.y);
        }
    }
}


