using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ein Record, der die Min und Max Werte definiert, zwischen denen ein Wall angelegt werden soll.
/// </summary>
public record WallDimension()
{
    /// <summary>
    /// Die linke Position, ab der ein WallObject startet
    /// </summary>
    public float leftStartXPosition { get; set; }

    /// <summary>
    /// Die obere (forward) Position, ab der ein WallObject startet
    /// </summary>
    public float topStartZPosition { get; set; }

    /// <summary>
    /// Die rechte Position, ab der ein WallObject startet
    /// </summary>
    public float rightStartXPosition { get; set; }

    /// <summary>
    /// Die untere (backward) Position, ab der ein WallObject startet
    /// </summary>
    public float bottomStartZPosition { get; set; }

    /// <summary>
    /// Die Höhe, ab der ein WallObject startet
    /// </summary>
    public float HeightStartYPosition { get; set; }

    /// <summary>
    /// Die linke Position, ab der ein WallObject sich zerstört
    /// </summary>
    public float leftDestroyXPosition { get; set; }

    /// <summary>
    /// Die obere (forward) Position, ab der ein WallObject sich zerstört
    /// </summary>
    public float topDestroyZPosition { get; set; }

    /// <summary>
    /// Die rechte Position, ab der ein WallObject sich zerstört
    /// </summary>
    public float rightDestroyXPosition { get; set; }

    /// <summary>
    /// Die untere (backward) Position, ab der ein WallObject sich zerstört
    /// </summary>
    public float bottomDestroyZPosition { get; set; }

    /// <summary>
    /// Die minimale Geschwindigkeit
    /// </summary>
    public float MinMoveSpeed { get; set; }

    /// <summary>
    /// Die maximale Geschwindigkeit
    /// </summary>
    public float MaxMoveSpeed { get; set; }

    /// <summary>
    /// Die minimale Höhe
    /// </summary>
    public float MinHeight { get; set; }

    /// <summary>
    /// Die maximale Höhe
    /// </summary>
    public float MaxHeight { get; set; }

    /// <summary>
    /// Die minmale Dickte
    /// </summary>
    public float MinThickness { get; set; }

    /// <summary>
    /// Die maximale Dickte
    /// </summary>
    public float MaxThickness { get; set; }

    /// <summary>
    /// Die minimale Länge in Unit (1 Unit entspricht 0.5 Meter)
    /// </summary>
    public int MinLengthUnit { get; set; }

    /// <summary>
    /// Die maximale Länge in Unit (1 Unit entspricht 0.5 Meter)
    /// </summary>
    public int MaxLengthUnit { get; set; }
}