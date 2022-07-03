using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller für die Deviceeingaben, die das Spiel, nicht den Spieler betreffen
/// </summary>
public class GameController : MonoBehaviour
{
    private PlayerControls playerControls;

    /// <summary>
    /// Hält global alle Stände des Spiels (Singleton)
    /// </summary>
    private GameState gameState;

    private void OnEnable()
    {
        playerControls = new PlayerControls();
        playerControls.Game.Enable();

        gameState = GameState.Instance();

        playerControls.Game.Quit.performed += QuitGame;
    }

    private void OnDisable()
    {
        playerControls.Game.Quit.performed -= QuitGame;

        playerControls.Game.Disable();
    }

    /// <summary>
    /// Handler für die ESC und ENDE Taste
    /// </summary>
    /// <param name="ctx">Der Kontext des Events</param>
    private void QuitGame(InputAction.CallbackContext ctx)
    {
        if (!gameState.MainMenuVisible)
        {
            EventManager.Instance().ShowMainMenue();
        }
    }
}