using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private GroundDetector groundDetector;
    [SerializeField] private WallDetector wallDetector;

    public bool IsGrounded => groundDetector.IsGrounded;
    public bool IsOnWall => wallDetector.IsOnWall;

    private void Awake()
    {
        Instance = this;

        groundDetector = GetComponent<GroundDetector>();
        wallDetector = GetComponent<WallDetector>();
    }
}
