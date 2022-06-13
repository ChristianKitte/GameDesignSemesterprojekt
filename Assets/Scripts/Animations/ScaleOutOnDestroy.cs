using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOutOnDestroy : MonoBehaviour
{
    [Tooltip("Steuert die Geschwindigkeit des Ausblendens")] [SerializeField]
    private float fadeOutFactor = 0.1f;

    private Vector3 scaleVector;

    private void OnDestroy()
    {
        EventManager.Instance().DestroyProvider -= scaleOut;
    }

    private void OnEnable()
    {
        scaleVector = this.transform.localScale;
        EventManager.Instance().DestroyProvider += scaleOut;
    }

    private void scaleOut()
    {
        EventManager.Instance().DestroyProvider -= scaleOut;

        while (scaleVector.x > 0)
        {
            scaleVector = new Vector3(
                scaleVector.x - fadeOutFactor,
                scaleVector.y - fadeOutFactor,
                scaleVector.z - fadeOutFactor);
        }
    }
}