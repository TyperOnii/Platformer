using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Vector2 InputVector { get; private set; }

    public PlayerInputActions Actions { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Actions = new PlayerInputActions();
        Actions.Player.Enable();
    }

    private void Update()
    {
        ReadInputVector();
    }

    private void ReadInputVector()
    {
        InputVector = Actions.Player.Move.ReadValue<Vector2>();
    }
    
}
