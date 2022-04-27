using System;
using UnityEngine;


/// <summary>
/// Bewegt ein Objekt stetig mit der eingestellten Geschwindigkeit nach vorne (space.self)
/// </summary>
public class MoveForward : MonoBehaviour
{
    [Tooltip("Die Geschwindigkeit der Vorwärtsbewegung")] [SerializeField]
    private float MoveSpeed = 0f;

    [Tooltip("Die linke Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float leftDestroyXPosition = 0f;

    [Tooltip("Die obere (forward) Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float topDestroyZPosition = 0f;

    [Tooltip("Die rechte Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float rightDestroyXPosition = 0f;

    [Tooltip("Die untere (backward) Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float bottomDestroyZPosition = 0f;

    /// <summary>
    /// Setzt die Geschwindigkeit der Vorwärtsbewegung und überschreibt zuvor gemachte
    /// Einstellungen
    /// </summary>
    /// <param name="speed">Die Geschwindigkeit</param>
    public void SetMoveSpeed(float speed)
    {
        MoveSpeed = speed;
    }

    /// <summary>
    /// Legt die Begrenzung fest, innerhalb derer sich das Objekt bewegen darf. Verlässt es eine
    /// der Grenzen, so löscht sich das Objekt selbstständig. 
    /// </summary>
    /// <param name="leftX">Die linke Position, ab der ein WallObject sich zerstört</param>
    /// <param name="topX">Die obere (forward) Position, ab der ein WallObject sich zerstört</param>
    /// <param name="rightX">Die rechte Position, ab der ein WallObject sich zerstört</param>
    /// <param name="bottomX">Die untere (backward) Position, ab der ein WallObject sich zerstört</param>
    public void SetRestrictionArea(float leftX, float topX, float rightX, float bottomX)
    {
        leftDestroyXPosition = leftX;
        topDestroyZPosition = topX;
        rightDestroyXPosition = rightX;
        bottomDestroyZPosition = bottomX;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
    }

    // Update is called once per frame 
    private void LateUpdate()
    {
        var curPosition = transform.position;

        if (curPosition.x < leftDestroyXPosition || curPosition.x > rightDestroyXPosition)
        {
            Destroy(this.gameObject);
        }

        if (curPosition.z < bottomDestroyZPosition || curPosition.z > topDestroyZPosition)
        {
            Destroy(this.gameObject);
        }
    }
}