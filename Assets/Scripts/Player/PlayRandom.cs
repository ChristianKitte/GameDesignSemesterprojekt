using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandom : MonoBehaviour
{
    [Tooltip("Der zufällig zu spielende Clip")] [SerializeField]
    private AudioClip clip;

    private bool play = false;
    private float salt;

    // Start is called before the first frame update
    void Start()
    {
        salt = Random.value;

        var waitFor = Random.Range(4, 16);

        StartCoroutine(wait(waitFor));
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            SoundManager.Instance.PlayEffect(clip);
            play = false;

            var waitFor = Random.Range(4, 16);
            StartCoroutine(wait(waitFor));
        }
    }

    private IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        play = true;
    }
}