using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ein Record, der die Min und Max Werte definiert, zwischen denen die betreffenden Attribute
/// angelegt werden sollen.
/// </summary>
public record ProviderDimension()
{
    /// <summary>
    /// Die minimale Anzahl an LiveProvider
    /// </summary>
    public int MinCountLiveProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an LiveProvider
    /// </summary>
    public int MaxCountLiveProvider { get; set; }

    /// <summary>
    /// Die minimale Anzahl an GhostProtectionProvider
    /// </summary>
    public int MinCountGhostProtectionProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an GhostProtectionProvider
    /// </summary>
    public int MaxCountGhostProtectionProvider { get; set; }

    /// <summary>
    /// Die minimale Anzahl an GoThroughProvider
    /// </summary>
    public int MinCountGoThroughProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an GoThroughProvider
    /// </summary>
    public int MaxCountGoThroughProvider { get; set; }

    /// <summary>
    /// Die linke Position, ab der ein Provider plaziert werden darf
    /// </summary>
    public float leftStartXPositionProvider { get; set; }

    /// <summary>
    /// Die obere (forward) Position, ab der ein Provider plaziert werden darf
    /// </summary>
    public float topStartZPositionProvider { get; set; }

    /// <summary>
    /// Die rechte Position, ab der ein Provider plaziert werden darf
    /// </summary>
    public float rightStartXPositionProvider { get; set; }

    /// <summary>
    /// Die untere (backward) Position, ab der ein Provider plaziert werden darf
    /// </summary>
    public float bottomStartZPositionProvider { get; set; }

    /// <summary>
    /// Die minimale Anzahl an Punkten für LiveProvider
    /// </summary>
    public int MinValueLiveProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an Punkten für LiveProvider
    /// </summary>
    public int MaxValueLiveProvider { get; set; }

    /// <summary>
    /// Die minimale Anzahl an Punkten für GhostProtectionProvider
    /// </summary>
    public int MinValueGhostProtectionProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an Punkten für GhostProtectionProvider
    /// </summary>
    public int MaxValueGhostProtectionProvider { get; set; }

    /// <summary>
    /// Die minimale Anzahl an Punkten für GoThroughProvider
    /// </summary>
    public int MinValueGoThroughProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an Punkten für GoThroughProvider
    /// </summary>
    public int MaxValueGoThroughProvider { get; set; }
}