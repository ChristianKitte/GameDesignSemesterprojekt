using System;
using UnityEngine;

/// <summary>
/// Erkennt und Verfolgt das hinterlegte Objekt ab einer einstellbaren Annäherung des Objektes. Das hinterlegte
/// GameObject wird nicht verfolgt, sofern die Schutzzeit größer 0 ist. Die Zeit wird in Update berücksichtigt.
/// </summary>
public class DetectAndTarget : MonoBehaviour
{
    /// <summary>
    /// True, wenn ein Player gefunden und eingebunden werden konnte, ansonsten false
    /// </summary>
    private bool targetNotNull;

    /// <summary>
    /// Hält eine Instanz von GameState (Singleton)
    /// </summary>
    private GameState gameState;

    /// <summary>
    /// Wird von Unity aufgerufen, wenn die Komponente enabled wird
    /// </summary>
    private void OnEnable()
    {
        gameState = GameState.Instance();
        targetNotNull = gameState.currentTarget != null;
    }

    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1*Time.deltaTime);
        if (gameState.currentTarget != null)
        {
            // attackieren, sobalt der Beobachtete in Reichweite ist 
            transform.LookAt(gameState.currentTarget.transform);
        }
    }
}