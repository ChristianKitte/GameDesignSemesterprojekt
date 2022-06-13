using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMessageEnabled : MonoBehaviour
{
    [Tooltip("Steuert die Geschwindigkeit des Ausblendens, wenn Scale Out auf True gesetzt ist")] [SerializeField]
    private float fadeOutFactor = 0.00001f;

    [Tooltip("Nutzt den Fade Out Factor um sich langsam auszublenden")] [SerializeField]
    private bool scaleOut = true;

    private Vector3 scaleVector;

    private void OnDestroy()
    {
        EventManager.Instance().DestroyProvider -= destroyThis;
    }

    private void OnEnable()
    {
        scaleVector = this.transform.localScale;
        EventManager.Instance().DestroyProvider += destroyThis;
    }

    private void destroyThis()
    {
        EventManager.Instance().DestroyProvider -= destroyThis;

        if (scaleOut)
        {
            while (scaleVector.x > 0)
            {
                scaleVector = new Vector3(
                    scaleVector.x - fadeOutFactor,
                    scaleVector.y - fadeOutFactor,
                    scaleVector.z - fadeOutFactor);
            }
        }

        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
    }
}