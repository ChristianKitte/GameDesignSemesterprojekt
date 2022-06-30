using UnityEngine;

/// <summary>
/// Erkennt und Verfolgt das hinterlegte Objekt ab einer einstellbaren Annäherung des Objektes. Das hinterlegte
/// GameObject wird nicht verfolgt, sofern die Schutzzeit größer 0 ist. Die Zeit wird in Update berücksichtigt.
/// </summary>
public class DetectAndFollow : MonoBehaviour
{
    [Tooltip("Der Verfolgte")] [SerializeField]
    private GameObject player;

    [Tooltip("Die Geschwindigkeit, mit der sich das Objekt im Ruhezustand dreht")] [SerializeField]
    private float rotationSpeed = 1f;

    [Tooltip("Der Radius, in dem ein detektierbares Objekt wahrgenommen werden soll")] [SerializeField]
    private float detectionRadius = 5.0f;

    [Tooltip("Die Schnelligkeit, mit der sich das Objekt dem detektierten Objekt annähert")] [SerializeField]
    private float detectionApproximate = 1f;

    [Tooltip("Der minimale Abstand, mit der sich das Objekt dem detektierten Objekt annähert")] [SerializeField]
    private float detectionStopApproximate = 1f;

    /// <summary>
    /// True, wenn ein Player gefunden und eingebunden werden konnte, ansonsten false
    /// </summary>
    private bool playerNotNull;

    /// <summary>
    /// Hält global alle Stände des Spiels (Singleton)
    /// </summary>
    private GameState gameState;

    private Swing swingScript;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerNotNull = player != null;

        gameState = GameState.Instance();

        swingScript = GetComponent<Swing>();
    }

    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1*Time.deltaTime);
        if (playerNotNull && gameState.collectedGhostProtectionProviderSeconds <= 0)
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= detectionRadius)
            {
                // Für die Annäherung müssen wir Swing ausschalten
                swingScript.enabled = false;

                // attackieren, sobalt der Beobachtete in Reichweite ist 
                transform.LookAt(player.transform);

                if (distance > detectionStopApproximate)
                {
                    // annähern, solange die minimale Distanz nicht erreicht wurde
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position,
                        detectionApproximate * Time.deltaTime);
                }
                else
                {
                    // weggehen, solange die minimale Distanz erreicht wurde
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position,
                        detectionApproximate * Time.deltaTime * -1.0f);
                }
            }
            else
            {
                // slow down
                swingScript.enabled = true;
                transform.Rotate(0, rotationSpeed, 0);
            }
        }
        else
        {
            // slow down
            transform.Rotate(0, rotationSpeed, 0);
        }
    }
}