using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputActions _input;

    private void OnEnable()
    {
        _input = new InputActions();
        _input.Game.Enable();

        _input.Game.Quit.performed += QuitGame;
    }

    private void OnDisable()
    {
        _input.Game.Quit.performed -= QuitGame;

        _input.Game.Disable();
    }

    private void QuitGame(InputAction.CallbackContext ctx)
    {
        if (GameState.Instance().GameIsPlaying && !GameState.Instance().GameIsPaused)
        {
            GameState.Instance().GameIsPaused = true;
            EventManager.Instance().ShowMainMenue();
        }
    }
}