using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Unity.VisualScripting;
using UnityEngine;

public class PlayOnCollision : MonoBehaviour
{
    [Tooltip("Der bei einer Kollision abzuspielende Clip")] [SerializeField]
    private AudioClip clip;

    [Tooltip("Spielt nur einen Sound, wenn das kollidierende GameObject den angegeben Tag hat")] [SerializeField]
    private string filterTag = String.Empty;

    private void OnCollisionEnter(Collision collision)
    {
        if (filterTag == String.Empty || collision.gameObject.CompareTag(this.filterTag))
        {
            SoundManager.Instance.PlayEffect(clip);
        }
    }
}