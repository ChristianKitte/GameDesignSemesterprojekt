using UnityEngine;


/// <summary>
/// Bewegt ein Objekt stetig mit der eingestellten Geschwindigkeit nach vorne (space.self)
/// </summary>
public class MoveForward : MonoBehaviour
{
    /// <summary>
    /// Die Geschwindigkeit der Vorwärtsbewegung
    /// </summary>
    [SerializeField] private float MoveSpeed = 0f;

    /// <summary>
    /// Setzt die Geschwindigkeit der Vorwärtsbewegung und überschreibt zuvor gemachte
    /// Einstellungen
    /// </summary>
    /// <param name="speed">Die Geschwindigkeit</param>
    public void SetMoveSpeed(float speed)
    {
        MoveSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
    }
}