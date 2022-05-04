using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMessageEnabled : MonoBehaviour
{
    private void OnDestroy()
    {
        EventManager.Instance().DestroyProvider -= destroyThis;
    }

    private void OnEnable()
    {
        EventManager.Instance().DestroyProvider += destroyThis;
    }

    private void destroyThis()
    {
        EventManager.Instance().DestroyProvider -= destroyThis;

        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
    }
}