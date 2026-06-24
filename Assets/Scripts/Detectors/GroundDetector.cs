using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D collider;

    [SerializeField] private float angle = 0f;
    [SerializeField] private float height = 0.2f;

    public bool IsGrounded
    {
        get
        {
            var point = new Vector2(collider.bounds.center.x, collider.bounds.min.y);
            var size = new Vector2(collider.bounds.extents.x, height);
            Collider2D hit = Physics2D.OverlapBox(point, size, angle, groundLayer);

            return hit != null;
        }
    }

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        var point = new Vector2(collider.bounds.center.x, collider.bounds.min.y);
        var size = new Vector2(collider.bounds.extents.x, height);

        Gizmos.DrawCube(point, size);
    }
}
