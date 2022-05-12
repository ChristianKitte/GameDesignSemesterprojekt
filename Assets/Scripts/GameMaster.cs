using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Game.Enumerations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;
using TMPro;

/// <summary>
/// Der GameMaster steuert den Spielverlauf und hält die aktuellen Zustände. Es handelt sich
/// um ein Singleton Objekt.
/// </summary>
public class GameMaster : MonoBehaviour
{
    [SerializeField] private TMP_Text TextComponent;

    #region Einstellungen und Variablen

    [Tooltip("Rundenzeit in Sekunden")] [SerializeField]
    private int TimePerRoundInSeconds;

    [Tooltip("Die zu verwendende WallFactory")] [SerializeField]
    private GameObject WallFactory;

    [Tooltip("Die zu verwendende ProviderFactory")] [SerializeField]
    private GameObject ProviderFactory;

    //public AudioSource audioSource;
    //public AudioClip colliderClip;

    private int playedSecondsSinceStart;

    private string currentTimeText;

    private int currentLevel = 1;

    #endregion

    #region Einstellungen für WallDimension

    [Tooltip("Die linke Position, ab der ein WallObject startet")] [SerializeField]
    private float leftStartXPosition = 203f;

    [Tooltip("Die obere (forward) Position, ab der ein WallObject startet")] [SerializeField]
    private float topStartZPosition = 191f;

    [Tooltip("Die rechte Position, ab der ein WallObject startet")] [SerializeField]
    private float rightStartXPosition = 203f;

    [Tooltip("Die untere (backward) Position, ab der ein WallObject startet")] [SerializeField]
    private float bottomStartZPosition = 191f;

    [Tooltip("Die Höhe, ab der ein WallObject startet")] [SerializeField]
    private float HeightStartYPosition = 0.5f;

    [Tooltip("Die linke Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float leftDestroyXPosition = 130f;

    [Tooltip("Die obere (forward) Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float topDestroyZPosition = 330f;

    [Tooltip("Die rechte Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float rightDestroyXPosition = 280f;

    [Tooltip("Die untere (backward) Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float bottomDestroyZPosition = 101f;

    [Tooltip("Referenzwert in Level1 - Minimale Wartezeit zwischen neuen Mauern in Sekunden")] [SerializeField]
    private int MinWallIntervallInSeconds;

    [Tooltip("Referenzwert in Level1 - Maximale Wartezeit zwischen neuen Mauern in Sekunden")] [SerializeField]
    private int MaxWallIntervallInSeconds;

    [Tooltip("Referenzwert in Level1 - Minimaler Anfangswert der Geschwindigkeit der Wälle")] [SerializeField]
    private float StartMinMoveSpeed = 5.0f;

    [Tooltip("Referenzwert in Level1 - Maximaler Endwert der Geschwindigkeit der Wälle")] [SerializeField]
    private float StartMaxMoveSpeed = 10.0f;

    [Tooltip("Referenzwert in Level1 - Minimaler Anfangswert der Höhe der Wälle")] [SerializeField]
    private float StartMinHeight = 0.5f;

    [Tooltip("Referenzwert in Level1 - Maximaler Endwert der Höhe der Wälle")] [SerializeField]
    private float StartMaxHeight = 4.0f;

    [Tooltip("Referenzwert in Level1 - Minimaler Anfangswert der Dicke der Wälle")] [SerializeField]
    private float StartMinThickness = 0.25f;

    [Tooltip("Referenzwert in Level1 - Maximaler Endwert der Dicke der Wälle")] [SerializeField]
    private float StartMaxThickness = 1.5f;

    [Tooltip(
        "Referenzwert in Level1 - Minimaler Anfangswert der Länge der Wälle in Units. ein UnNit entspricht 0.5 Meter")]
    [SerializeField]
    private int StartMinLengthUnits = 5;

    [Tooltip("Referenzwert in Level1 - Maximaler Endwert der Länge der Wälle in Units. ein Unit entspricht 0.5 Meter")]
    [SerializeField]
    private int StartMaxLengthUnit = 40;

    #endregion

    #region Einstellungen für ProviderDimension

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an LiveProvider")] [SerializeField]
    private int MinCountLiveProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an LiveProvider")] [SerializeField]
    private int MaxCountLiveProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an GhostProtectionProvider")] [SerializeField]
    private int MinCountGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an GhostProtectionProvider")] [SerializeField]
    private int MaxCountGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an GoThroughProvider")] [SerializeField]
    private int MinCountGoThroughProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an GoThroughProvider")] [SerializeField]
    private int MaxCountGoThroughProvider;

    [Tooltip("Die linke Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float leftStartXPositionProvider;

    [Tooltip("Die obere (forward) Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float topStartZPositionProvider;

    [Tooltip("Die rechte Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float rightStartXPositionProvider;

    [Tooltip("Die untere (backward) Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float bottomStartZPositionProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an Punkten für LiveProvider")] [SerializeField]
    private int MinValueLiveProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an Punkten für LiveProvider")] [SerializeField]
    private int MaxValueLiveProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an Punkten für GhostProtectionProvider")] [SerializeField]
    private int MinValueGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an Punkten für GhostProtectionProvider")] [SerializeField]
    private int MaxValueGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an Punkten für GoThroughProvider")] [SerializeField]
    private int MinValueGoThroughProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an Punkten für GoThroughProvider")] [SerializeField]
    private int MaxValueGoThroughProvider;

    #endregion

    private EventManager eventManager;
    private GameState gameState;

    private void OnEnable()
    {
        eventManager = EventManager.Instance();
        gameState = GameState.Instance();

        gameState.currentLevel = 1;
        gameState.defaultSecondsToPlayPerLevel = TimePerRoundInSeconds;
    }

    /// <summary>
    /// Wird von Unity aufgerufen, wenn die Komponente disabled wird
    /// </summary>
    private void OnDisable()
    {
        eventManager = null;
        gameState = null;

        EventManager.Instance().SecondTick -= HandleSecondEvent;
        EventManager.Instance().CollisionDetected -= HandleCollisionDetectedEvent;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance().SecondTick += HandleSecondEvent;
        EventManager.Instance().CollisionDetected += HandleCollisionDetectedEvent;

        startGameLevel();
    }

    private void startGame()
    {
    }

    private void endGame()
    {
    }

    /// <summary>
    /// Startet das aktuell gesetzte Level 
    /// </summary>
    private void startGameLevel()
    {
        // Shortcut - während der Entwicklung werden Einstellungen nicht auf Basis des Levels angepasst

        gameState.remainingSecondsToPlayLevel = gameState.defaultSecondsToPlayPerLevel;

        EventManager.Instance().SendResetTimer(gameState.defaultSecondsToPlayPerLevel);
        EventManager.Instance().SendStartTimer();

        ProviderFactory.GetComponent<ProviderMaker>()?.CreateProvider(getNewProviderDimension(currentLevel));

        // Debugausgabe
        string playerPointString = $"Aktueller Punktestand: {gameState.playerPoints.ToString()} Punkte";
        string playerGetPointString =
            $"Der Spieler hat {gameState.collectedLiveProviderPoints.ToString()} Punkte gesammelt";
        string playerGoThroughString =
            $"Der Spieler hat {gameState.collectedGoThroughProviderSeconds} Sekunden zur Verfügung, um durch Wände zu gehen";
        string playerGhostProtectionString =
            $"Der Spieler hat {gameState.collectedGhostProtectionProviderSeconds} Sekunden zur Verfügung, in denen er vor Geister geschützt ist";

        string ausgabe = playerPointString
                         + System.Environment.NewLine
                         + playerGetPointString
                         + System.Environment.NewLine
                         + playerGoThroughString
                         + System.Environment.NewLine
                         + playerGhostProtectionString;

        TextComponent.SetText(ausgabe);
    }

    /// <summary>
    /// Beendet das aktuell gesetzte Level
    /// </summary>
    private void finishCurrentGameLevel()
    {
        EventManager.Instance().SendKillSignalForProvider();
    }

    /// <summary>
    /// Setzt den Levelzähler um eins hoch und startet das neue Level
    /// </summary>
    private void runNextLevel()
    {
        gameState.currentLevel++;
        startGameLevel();
    }

    /// <summary>
    /// Setzt den Levelzähler um eins herunter und startet das neue Level.
    /// Ist das aktuelle Level das erste Level, so bleibt der Levelzähler unverändert.
    /// </summary>
    private void runPreviousLevel()
    {
        if (gameState.currentLevel == 1)
        {
            startGameLevel();
        }
        else
        {
            gameState.currentLevel--;
            startGameLevel();
        }
    }

    #region Game Events

    /// <summary>
    /// Wird aufgerufen, wenn ein GameObjekt mit der Komponente DeleteOnCollision oder SendMessageOnCollision
    /// eine Kollision mit einem in TargetTag der Komponente festgelegten GameObjekt hat
    /// </summary>
    /// <param name="collisionType">Der Typ der Kollision</param>
    /// <param name="value">Der dem Objekt zugeordnete Wert</param>
    private void HandleCollisionDetectedEvent(CollisionObjektTyp collisionType, int value)
    {
        switch (collisionType)
        {
            case CollisionObjektTyp.LiveProvider:
                gameState.playerPoints = gameState.playerPoints + value;
                gameState.collectedLiveProviderPoints = gameState.collectedLiveProviderPoints + value;
                break;
            case CollisionObjektTyp.GoThroughProvider:
                gameState.collectedGoThroughProviderSeconds = gameState.collectedLiveProviderPoints + value;
                break;
            case CollisionObjektTyp.GhostProtectionProvider:
                gameState.collectedGhostProtectionProviderSeconds = gameState.collectedLiveProviderPoints + value;
                break;
            case CollisionObjektTyp.MovingWall:
                gameState.playerPoints = gameState.playerPoints - value;
                break;
            case CollisionObjektTyp.Ghost:
                break;
            case CollisionObjektTyp.MainTarget:
                break;
        }

        // Debugausgabe
        string playerPointString = $"Aktueller Punktestand: {gameState.playerPoints.ToString()} Punkte";
        string playerGetPointString =
            $"Der Spieler hat {gameState.collectedLiveProviderPoints.ToString()} Punkte gesammelt";
        string playerGoThroughString =
            $"Der Spieler hat {gameState.collectedGoThroughProviderSeconds} Sekunden zur Verfügung, um durch Wände zu gehen";
        string playerGhostProtectionString =
            $"Der Spieler hat {gameState.collectedGhostProtectionProviderSeconds} Sekunden zur Verfügung, in denen er vor Geister geschützt ist";

        string ausgabe = playerPointString
                         + System.Environment.NewLine
                         + playerGetPointString
                         + System.Environment.NewLine
                         + playerGoThroughString
                         + System.Environment.NewLine
                         + playerGhostProtectionString;

        TextComponent.SetText(ausgabe);
    }

    /// <summary>
    /// Wird einmal je Spielsekunde durch das Event SecondTick des EventManagers aufgerufen
    /// </summary>
    private void HandleSecondEvent()
    {
        gameState.remainingSecondsToPlayLevel--;

        int curWallIntervall = getNewWallIntervall(gameState.currentLevel);
        for (int i = 0; i < curWallIntervall; i++)
        {
            WallFactory.GetComponent<WallMaker>().createWall(getNewWallDimension(currentLevel));
        }

        // Wird betreten, wenn die maximale Spielzeit des Levels erreicht ist. In diesen Fall endet
        // das Level und der Spieler wird um ein Level zurück gesetzt.
        //
        // Ist vielleicht eleganter über ein Timer Event zu lösen
        if (gameState.remainingSecondsToPlayLevel <= 0)
        {
            // Um einen Level zurück setzen
            //runPreviousLevel();

            // for debug purposes
            finishCurrentGameLevel();
            runNextLevel();
        }
        else
        {
            TimeSpan restzeit = TimeSpan.FromSeconds(
                gameState.defaultSecondsToPlayPerLevel - gameState.remainingSecondsToPlayLevel);
            currentTimeText = $"{restzeit.Minutes.ToString()}:{restzeit.Seconds.ToString()}";
        }
    }

    #endregion

    /// <summary>
    /// Gibt einen Zeitpunkt in Sekunden zurück. in der eine neue Wand erscheinen soll. Der Zeitpunkt befindet
    /// sich zwischen den auf Basis der Vorgaben und des aktuellen Levels festgelegten Minimalen und maximalen Wert.
    /// </summary>
    /// <param name="currentLevel">Der aktuelle Level</param>
    /// <returns>Die Zeit in Sekunden, wenn die nächste Wand erscheinen soll</returns>
    private int getNewWallIntervall(int currentLevel)
    {
        return RandomNumberGenerator.GetInt32(MinWallIntervallInSeconds, MaxWallIntervallInSeconds + 1);
    }

    /// <summary>
    /// Erzeugt auf Basis der anfänglichen Vorgaben und des aktuellen Levels einen neuen Record, der die minimalen
    /// und maximalen Abnessungen für die nächste zu generierende Wand enthält sowie deren räumliche Begrenzung.
    /// </summary>
    /// <param name="currentLevel">Das aktuelle Level</param>
    /// <returns>Die zur Generierung zu verwendenen Rahmenangaben für die Wandabmessungen als WallDimension</returns>
    private WallDimension getNewWallDimension(int currentLevel)
    {
        var newWallDimension = new WallDimension()
        {
            leftStartXPosition = leftStartXPosition,
            topStartZPosition = topStartZPosition,
            rightStartXPosition = rightStartXPosition,
            bottomStartZPosition = bottomStartZPosition,
            HeightStartYPosition = HeightStartYPosition,
            leftDestroyXPosition = leftDestroyXPosition,
            topDestroyZPosition = topDestroyZPosition,
            rightDestroyXPosition = rightDestroyXPosition,
            bottomDestroyZPosition = bottomDestroyZPosition,
            MinMoveSpeed = StartMinMoveSpeed,
            MaxMoveSpeed = StartMaxMoveSpeed,
            MinHeight = StartMinHeight,
            MaxHeight = StartMaxHeight,
            MinThickness = StartMinThickness,
            MaxThickness = StartMaxThickness,
            MinLengthUnit = StartMinLengthUnits,
            MaxLengthUnit = StartMaxLengthUnit
        };

        return newWallDimension;
    }

    /// <summary>
    /// Erzeugt auf Basis der anfänglichen Vorgaben und des aktuellen Levels einen neuen Record, der die minimalen
    /// und maximalen Plazierung für die im Level befindlichen Provider sowie deren Wertebereich enthält .
    /// </summary>
    /// <param name="currentLevel">Das aktuelle Level</param>
    /// <returns>Die zur Generierung zu verwendenen Rahmenangaben für die Provider als ProviderDimension</returns>
    private ProviderDimension getNewProviderDimension(int currentLevel)
    {
        var newProviderDimension = new ProviderDimension()
        {
            MinCountLiveProvider = MinCountLiveProvider,
            MaxCountLiveProvider = MaxCountLiveProvider,
            MinCountGhostProtectionProvider = MinCountGhostProtectionProvider,
            MaxCountGhostProtectionProvider = MaxCountGhostProtectionProvider,
            MinCountGoThroughProvider = MinCountGoThroughProvider,
            MaxCountGoThroughProvider = MaxCountGoThroughProvider,
            leftStartXPositionProvider = leftStartXPositionProvider,
            topStartZPositionProvider = topStartZPositionProvider,
            rightStartXPositionProvider = rightStartXPositionProvider,
            bottomStartZPositionProvider = bottomStartZPositionProvider,
            MinValueLiveProvider = MinValueLiveProvider,
            MaxValueLiveProvider = MaxValueLiveProvider,
            MinValueGhostProtectionProvider = MinValueGhostProtectionProvider,
            MaxValueGhostProtectionProvider = MaxValueGhostProtectionProvider,
            MinValueGoThroughProvider = MinValueGoThroughProvider,
            MaxValueGoThroughProvider = MaxValueGoThroughProvider
        };

        return newProviderDimension;
    }
}