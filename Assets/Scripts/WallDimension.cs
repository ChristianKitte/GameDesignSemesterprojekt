using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ein Record, der die Min und Max Werte definiert, zwischen denen ein Wall angelegt werden soll.
/// </summary>
public record WallDimension()
{
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
    /// Die minimale Länge
    /// </summary>
    public float MinLength { get; set; }

    /// <summary>
    /// Die maximale Länge
    /// </summary>
    public float MaxLength { get; set; }
}