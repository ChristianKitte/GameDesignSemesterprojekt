using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnCollision : MonoBehaviour
{
    [SerializeField] private string TargetTag = "Player";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == TargetTag)
        {
            Debug.Log("Bang...");
            Destroy(this.gameObject);
            Destroy(this);
        }
    }
}