using UnityEngine;

/// <summary>
/// Spielt einen Effekt bei der Kollision mit einem Objekt, das über den hinterlegten TAG verfügt
/// </summary>
public class PlayOnCollision : MonoBehaviour
{
    [Tooltip("Der bei einer Kollision abzuspielende Clip")] [SerializeField]
    private AudioClip clip;

    [Tooltip("Der bei einer Kollision abzuspielende Clip")] [SerializeField]
    private AudioClip afterClip;

    [Tooltip("Spielt nur einen Sound, wenn das kollidierende GameObject den angegeben Tag hat")] [SerializeField]
    private string TargetTag = "";

    /// <summary>
    /// Wird bei einer Kollision aufgerufen
    /// </summary>
    /// <param name="collision">Das Kontext Objekt</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (TargetTag != "" && collision.gameObject.CompareTag(TargetTag))
        {
            SoundManager.Instance.PlayEffect(clip);

            if (afterClip != null)
            {
                var shouldLaugh = Random.Range(4, 16) % 2 == 0;
                if (shouldLaugh)
                {
                    SoundManager.Instance.PlayEffect(afterClip);
                }
            }
        }
    }
}