using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterFinalTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance().currentTarget = gameObject;
    }

    private void LateUpdate()
    {
        if (GameState.Instance().currentTarget == null)
        {
            GameState.Instance().currentTarget = gameObject;
        }
    }
}