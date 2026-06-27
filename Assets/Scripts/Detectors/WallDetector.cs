using UnityEngine;

[RequireComponent (typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class WallDetector : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Collider2D collider;

    [SerializeField] private float angle = 0f;
    [SerializeField] private float weight = 0.2f;

    private SpriteRenderer _sr;

    public bool IsOnWall
    {
        get
        {
            float pointX = _sr.flipX ? collider.bounds.min.x : collider.bounds.max.x;
            float pointY = collider.bounds.center.y;

            var point = new Vector2(pointX, pointY);
            var size = new Vector2(weight, collider.bounds.extents.y);
            Collider2D hit = Physics2D.OverlapBox(point, size, angle, wallLayer);

            return hit != null;
        }
    }

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnDrawGizmosSelected()
    {
        if (_sr == null || collider == null) return; 

        Gizmos.color = Color.red;

        float pointX = _sr.flipX ? collider.bounds.min.x : collider.bounds.max.x;
        float pointY = collider.bounds.center.y;

        var point = new Vector2(pointX, pointY);
        var size = new Vector2(weight, collider.bounds.extents.y);

        Gizmos.DrawCube(point, size);
    }
}
