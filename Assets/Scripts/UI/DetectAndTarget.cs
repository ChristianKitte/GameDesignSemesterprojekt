using System;
using UnityEngine;

/// <summary>
/// Erkennt und Verfolgt das hinterlegte Objekt ab einer einstellbaren Annäherung des Objektes. Das hinterlegte
/// GameObject wird nicht verfolgt, sofern die Schutzzeit größer 0 ist. Die Zeit wird in Update berücksichtigt.
/// </summary>
public class DetectAndTarget : MonoBehaviour
{
    void Update()
    {
        if (GameState.Instance().currentTarget != null)
        {
            // attackieren, sobalt der Beobachtete in Reichweite ist 
            transform.LookAt(GameState.Instance().currentTarget.transform);
        }
    }
}