using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera camera;

    private float xRotation = 0f;

    private float xSensitivity = 30f;
    private float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Berechnen Up Down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // steuern der Kamera Transformation hoch und runter schauen
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        
        // steuert den Spieler nach links und rechts (Skript ist einem Player zugeordnet !)
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}