namespace Game.Enumerations
{
    /// <summary>
    /// Definiert eine Bewegungsrichtung
    /// </summary>
    public enum CollisionObjektTyp
    {
        /// <summary>
        /// Default
        /// </summary>
        DefaultValue = 0,

        /// <summary>
        /// Objekt ist ein LiveProvider
        /// </summary>
        LiveProvider = 1,

        /// <summary>
        /// Objekt ist ein GoThroughProvider
        /// </summary>
        GoThroughProvider = 2,

        /// <summary>
        /// Objekt ist ein GhostProtectionProvider
        /// </summary>
        GhostProtectionProvider = 3,

        /// <summary>
        /// Objekt ist eine sich bewegende Wand
        /// </summary>
        MovingWall = 4,

        /// <summary>
        /// Objekt ist ein Geist
        /// </summary>
        Ghost = 5,

        /// <summary>
        /// Objekt ist das Hauptziel
        /// </summary>
        MainTarget = 6
    };
}