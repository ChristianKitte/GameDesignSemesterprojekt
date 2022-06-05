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
    public int MinCountBananaProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an LiveProvider
    /// </summary>
    public int MaxCountBananaProvider { get; set; }

    /// <summary>
    /// Die minimale Anzahl an GhostProtectionProvider
    /// </summary>
    public int MinCountGhostProtectionProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an GhostProtectionProvider
    /// </summary>
    public int MaxCountGhostProtectionProvider { get; set; }

    /// <summary>
    /// Die minimale Anzahl an WallProtectionProvider
    /// </summary>
    public int MinCountWallProtectionProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an WallProtectionProvider
    /// </summary>
    public int MaxCountWallProtectionProvider { get; set; }

    /// <summary>
    /// Die minimale Anzahl an WallProtectionProvider
    /// </summary>
    public int MinCountNPCGhostProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an WallProtectionProvider
    /// </summary>
    public int MaxCountNPCGhostProvider { get; set; }

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
    /// Die minimale Anzahl an Bananen für BananaProvider
    /// </summary>
    public int MinValueBananaProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an Bananen für BananaProvider
    /// </summary>
    public int MaxValueBananaProvider { get; set; }

    /// <summary>
    /// Die minimale Anzahl an Punkten für GhostProtectionProvider
    /// </summary>
    public int MinValueGhostProtectionProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an Punkten für GhostProtectionProvider
    /// </summary>
    public int MaxValueGhostProtectionProvider { get; set; }

    /// <summary>
    /// Die minimale Anzahl an Punkten für WallProtectionProvider
    /// </summary>
    public int MinValueWallProtectionProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an Punkten für WallProtectionProvider
    /// </summary>
    public int MaxValueWallProtectionProvider { get; set; }

    /// <summary>
    /// Die minimale Anzahl an Bananen für NPCGhost
    /// </summary>
    public int MinValueNPCGhostProvider { get; set; }

    /// <summary>
    /// Die maximale Anzahl an Bananen für NPCGhost
    /// </summary>
    public int MaxValueNPCGhostProvider { get; set; }
}