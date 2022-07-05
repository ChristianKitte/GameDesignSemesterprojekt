using UnityEngine;

/// <summary>
/// Eine zentrale Containerklasse als Singleton um den Zustand des aktuellen Spiels zu halten 
/// </summary>
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

    #region Sichtbarkeit Main und Level Menü

    /// <summary>
    /// True, wenn das Hauptmenü sichtbar ist, ansonsten false
    /// </summary>
    public bool MainMenuVisible { get; set; }

    /// <summary>
    /// True, wenn das LevelMenü sichtbar ist, ansonsten false
    /// </summary>
    public bool LevelMenuVisible { get; set; }

    #endregion

    #region Andere Einstellungen

    /// <summary>
    /// True, wenn ein neues Spiel gestartet wurde, ansonsten false
    /// </summary>
    public bool GameIsPlaying { get; set; }

    /// <summary>
    /// True, wenn ein Level beendet ist, ansonsten False
    /// </summary>
    public bool LevelBreak { get; set; }

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

    #endregion

    #region Punkte und Sekunden

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

    public GameObject currentTarget { get; set; }

    #endregion
}