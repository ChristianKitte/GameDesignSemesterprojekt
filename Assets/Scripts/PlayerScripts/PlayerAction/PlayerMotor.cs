using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    public float speed = 5f;
    public float jumpHeight = 3f;

    private bool isGrounded;
    public float gravity = -9.8f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update
    void Update()
    {
        // Update jedes Frame. isGrounded ist eine Unity Eigenschaft
        isGrounded = controller.isGrounded;
    }

    // Eingaben des InputManagers entgegen nehmen und auf den Character anwenden
    // ==> Bewegung im 3D immer mit einem Vektor 3, Eingabe ist aber Vektor 2
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        if (isGrounded && playerVelocity.y < 0)
        {
            // Es soll ein wenig unter 0 liegen
            playerVelocity.y = -2f;
        }

        // Es wirkt aber immer ncoh ein Kraft auf den Player
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void ProcessJump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}