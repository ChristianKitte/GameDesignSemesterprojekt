using UnityEngine;

/// <summary>
/// Sobalt das unterliegenede Objekt gestartet wird, wird der Menü Player gestartet
/// </summary>
public class PlayOnStart : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayMenueMusic();
    }
}