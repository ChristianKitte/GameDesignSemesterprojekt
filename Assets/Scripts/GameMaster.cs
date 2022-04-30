using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

/// <summary>
/// Der GameMaster steuert den Spielverlauf und hält die aktuellen Zustände. Es handelt sich
/// um ein Singleton Objekt.
/// </summary>
public class GameMaster : MonoBehaviour
{
    #region Einstellungen und Variablen

    [Tooltip("Rundenzeit in Sekunden")] [SerializeField]
    private int TimePerRoundInSeconds;

    [Tooltip("Die zu verwendende WallFactory")] [SerializeField]
    private GameObject WallFactory;

    [Tooltip("Die zu verwendende ProviderFactory")] [SerializeField]
    private GameObject ProviderFactory;

    /// <summary>
    /// Aktuell verwendeter Intervall für das Generieren einer Wand
    /// </summary>
    private int curWallIntervallInSeconds;

    private int playedSecondsSinceStart;
    private int remainingSecondsSinceStart;
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

    // Start is called before the first frame update
    void Start()
    {
        remainingSecondsSinceStart = TimePerRoundInSeconds + 1;
        EventManager.Instance().SecondTick += HandleSecondEvent;

        curWallIntervallInSeconds = getNewWallIntervall(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Wird von Unity aufgerufen, wenn die Komponente disabled wird
    /// </summary>
    private void OnDisable()
    {
        EventManager.Instance().SecondTick -= HandleSecondEvent;
    }

    /// <summary>
    /// Wird einmal je Spielsekunde durch das Event SecondTick des EventManagers aufgerufen
    /// </summary>
    private void HandleSecondEvent()
    {
        playedSecondsSinceStart++;
        remainingSecondsSinceStart--;

        ProviderFactory.GetComponent<ProviderMaker>().CreateProvider(getNewProviderDimension(currentLevel));

        int curWallIntervall = getNewWallIntervall(currentLevel);
        for (int i = 0; i < curWallIntervall; i++)
        {
            WallFactory.GetComponent<WallMaker>().createWall(getNewWallDimension(currentLevel));
        }

        // Wird betreten, wenn die aktuelle Zeit remainingSecondsSinceStart den Wert 0 erreicht hat. Im
        // Spielkontext ist dies das Ende der zur Verfügung stehenden Zeit einer Runde
        if (remainingSecondsSinceStart < 0)
        {
            EventManager.Instance().SendResetTimer();
            remainingSecondsSinceStart = TimePerRoundInSeconds;
            EventManager.Instance().SendStartTimer();
        }

        TimeSpan restzeit = TimeSpan.FromSeconds(remainingSecondsSinceStart);
        currentTimeText = $"{restzeit.Minutes.ToString()}:{restzeit.Seconds.ToString()}";
    }

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