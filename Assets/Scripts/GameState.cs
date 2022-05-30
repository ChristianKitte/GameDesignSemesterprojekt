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
    /// Setzt die Singleton Intanz zurück auf ihren Anfangswert
    /// </summary>
    /// <returns>Die Instanz von GameState</returns>
    public GameState reset()
    {
        _gameState = new GameState();
        return _gameState;
    }

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
    /// Der aktuelle Punktestand des Spielers
    /// </summary>
    //public int playerPoints { get; set; } = 0;

    /// <summary>
    /// Restliche Sekunden für GhostProtection
    /// </summary>
    public int remainingSecondsGhostProtectionProvider { get; set; } = 0;

    /// <summary>
    /// Restliche Sekunden für GoThrough
    /// </summary>
    public int remainingSecondsGoThroughProvider { get; set; } = 0;

    /// <summary>
    /// Alle bisher gesammelten Punkte des LiveProviders 
    /// </summary>
    public int collectedLiveProviderPoints { get; set; } = 0;

    /// <summary>
    /// Alle bisher gesammelten Sekunden des GhostProtectionProviders
    /// </summary>
    public int collectedGhostProtectionProviderSeconds { get; set; } = 0;

    /// <summary>
    /// Alle bisher gesammelten Sekunden des GoThroughProviders
    /// </summary>
    public int collectedGoThroughProviderSeconds { get; set; } = 0;
}