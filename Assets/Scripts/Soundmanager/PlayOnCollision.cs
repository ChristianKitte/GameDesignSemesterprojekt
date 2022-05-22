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
    private string TargetTag = "";

    private void OnCollisionEnter(Collision collision)
    {
        if (TargetTag != "" && collision.gameObject.CompareTag(TargetTag))
        {
            SoundManager.Instance.PlayEffect(clip);
        }
    }
}