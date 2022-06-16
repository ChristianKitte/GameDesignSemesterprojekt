using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private PlayerControls playerControls;

    private void OnEnable()
    {
        playerControls = new PlayerControls();
        playerControls.Game.Enable();

        playerControls.Game.Quit.performed += QuitGame;
    }

    private void OnDisable()
    {
        playerControls.Game.Quit.performed -= QuitGame;

        playerControls.Game.Disable();
    }

    /// <summary>
    /// Handler für die ESC Taste
    /// </summary>
    /// <param name="ctx">Der Kontext des Events</param>
    private void QuitGame(InputAction.CallbackContext ctx)
    {
        if (GameState.Instance().GameIsPlaying && !GameState.Instance().GameIsPaused)
        {
            GameState.Instance().GameIsPaused = true;
            EventManager.Instance().ShowMainMenue();
        }
    }
}