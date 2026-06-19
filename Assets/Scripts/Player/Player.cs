using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private GroundDetector groundDetector;

    public bool IsGrounded => groundDetector.IsGrounded;

    private void Awake()
    {
        Instance = this;

        groundDetector = GetComponent<GroundDetector>();
    }
}
