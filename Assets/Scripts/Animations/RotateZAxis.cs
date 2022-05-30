using UnityEngine;

/// <summary>
/// Lässt das unterliegende GameObject um die Z-Achse rotieren
/// </summary>
public class RotateZAxis : MonoBehaviour
{
    [Tooltip("Die Geschwindigkeit und Richtung der Rotation")] [SerializeField]
    private float rotationSpeed = 1.0f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1 * rotationSpeed));
    }
}