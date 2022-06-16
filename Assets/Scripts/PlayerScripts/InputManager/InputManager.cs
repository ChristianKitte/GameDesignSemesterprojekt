using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerControls.PlayerActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    public PlayerControls.PlayerActions OnFoot => playerControls.Player;

    private void Awake()
    {
        playerControls = new PlayerControls();

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot = playerControls.Player;
        onFoot.Jump.performed += ctx => motor.ProcessJump();
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }

    // Update rate is fixed
    void FixedUpdate()
    {
        // Prozess Bewegen abarbeiten auf Basis der Eingaben
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        // Prozess Look abarbeiten auf Basis der Eingaben
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
}