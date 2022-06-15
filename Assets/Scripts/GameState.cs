using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Game.Enumerations;
using Unity.VisualScripting;
using UnityEngine;

// Zentrale Containerklasse um den Zustand des aktuellen Spiels zu halten
public class GameState
{
    /// <summary>
    /// Einzige Instanz der Klasse GameState (Singleton)
    /// </summary>
    private static GameState _gameState;


    /// <summary>
    /// Liefert eine Instanz der Klasse GameState zurück
    /// </summary>
    /// <returns>Die Instanz von GameState</returns>
    public static GameState Instance()
    {
        if (_gameState == null)
        {
            _gameState = new GameState();
        }

        return _gameState;
    }

    /// <summary>
    /// Setzt die Singleton Instanz zurück auf ihren Anfangswert
    /// </summary>
    /// <returns>Die Instanz von GameState</returns>
    public GameState reset()
    {
        _gameState = new GameState();
        return _gameState;
    }

    /// <summary>
    /// True, wenn das Spiel gestartet wurde, ansonsten false
    /// </summary>
    public bool GameIsPlaying { get; set; }

    /// <summary>
    /// True, wenn die Spiel pausiert, ansonsten false
    /// </summary>
    public bool GameIsPaused { get; set; }

    /// <summary>
    /// True, wenn das Hauptmenü angezeigt wird, ansonsten false 
    /// </summary>
    public bool GameMainMenuIsShowing { get; set; }

    /// <summary>
    /// True, wenn der Leveldialog aktiv ist, ansonsten false
    /// </summary>
    public bool GameLevelDlgIsShowing { get; set; }

    /// <summary>
    /// Die Zeit in Sekunden eines Levels
    /// </summary>
    public int defaultSecondsToPlayPerLevel { get; set; } = 0;

    /// <summary>
    /// Restliche Sekunden bis zum Levelende
    /// </summary>
    public int remainingSecondsToPlayLevel { get; set; } = 0;

    /// <summary>
    /// Das aktuelle Spiellevel
    /// </summary>
    public int currentLevel { get; set; } = 0;

    /// <summary>
    /// Restliche Sekunden für GhostProtection
    /// </summary>
    public int remainingSecondsGhostProtectionProvider { get; set; } = 0;

    /// <summary>
    /// Restliche Sekunden für WallProtection
    /// </summary>
    public int remainingSecondsWallProtectionProvider { get; set; } = 0;

    /// <summary>
    /// Alle bisher gesammelte Bananen des BananaProviders 
    /// </summary>
    public int collectedBananaProviderBanana { get; set; } = 0;

    /// <summary>
    /// Alle bisher gesammelten Sekunden des GhostProtectionProviders
    /// </summary>
    public int collectedGhostProtectionProviderSeconds { get; set; } = 0;

    /// <summary>
    /// Alle bisher gesammelten Sekunden des GoThroughProviders
    /// </summary>
    public int collectedWallProtectionProviderSeconds { get; set; } = 0;
}