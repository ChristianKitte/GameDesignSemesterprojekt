using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ein Record, der die Min und Max Werte definiert, zwischen denen ein Player angelegt werden soll.
/// </summary>
public class PlayerDimension
{
    /// <summary>
    /// Die linke Position, ab der ein Player plaziert werden darf
    /// </summary>
    public float leftStartXPositionPlayer { get; set; }

    /// <summary>
    /// Die obere (forward) Position, ab der ein Player plaziert werden darf
    /// </summary>
    public float topStartZPositionPlayer { get; set; }

    /// <summary>
    /// Die rechte Position, ab der ein Player plaziert werden darf
    /// </summary>
    public float rightStartXPositionPlayer { get; set; }

    /// <summary>
    /// Die untere (backward) Position, ab der ein Player plaziert werden darf
    /// </summary>
    public float bottomStartZPositionPlayer { get; set; }
}