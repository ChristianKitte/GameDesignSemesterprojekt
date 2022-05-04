using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

/// <summary>
/// Setzt zufällig einen Integerwert zwischen den angegebenen Wertebereich und
/// zeigt diese in der übergebenen Textkomponente an
/// </summary>
public class SetRandomValue : MonoBehaviour
{
    [Tooltip("Das übergeordnete GameObjekt")] [SerializeField]
    private GameObject Provider;

    [Tooltip("Die Textkomponente, auf welcher die Zahl angezeigt werden soll")] [SerializeField]
    private TMP_Text TextComponent;

    [Tooltip("Der kleinste Wert (inkl.) des Zufallsbereichs (Default = 0)")] [SerializeField]
    private int MinValue = 0;

    [Tooltip("Der größte Wert (inkl.) des Zufallsbereichs (Default = 0)")] [SerializeField]
    private int MaxValue = 0;

    /// <summary>
    /// Der aktuelle Wert
    /// </summary>
    public int CurrentValue { get; private set; } = 0;

    void Start()
    {
        var livePoint = RandomNumberGenerator.GetInt32(MinValue, MaxValue + 1);
        TextComponent.SetText(livePoint.ToString());

        CurrentValue = livePoint;
    }
}