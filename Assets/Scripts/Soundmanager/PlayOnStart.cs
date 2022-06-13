using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Unity.VisualScripting;
using UnityEngine;

public class PlayOnStart : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayMenueMusic();
    }
}