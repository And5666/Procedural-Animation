using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject character;
    public GameObject playerGraphic;

    [Header("General Settings")]
    public float speed;
    public float gravity;
    public float jumpHeight;
    public bool isMoving;
    public float playerSpeed;

    [Header("Crouch Settings")]
    public float crouchSpeed;
    public float crouchHeight;
    public float crouchJumpHeight;
    public Vector3 crouchScale;
    public GameObject rayPoint;

    private bool isCrouching;
    private float originalHeight;
    private float originalSpeed;
    private float originalJumpHeight;
    private Vector3 moveDirection;

    private Vector3 velocity;

    private void Start()
    {
        originalHeight = controller.height;
        originalSpeed = speed;
        originalJumpHeight = jumpHeight;
    }

    void Update()
    {
        walk();
        jump();
        crouch();
        playerPhysics();
        playerRotate();
    }

    void walk()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0f, z);

        controller.Move(move * speed * Time.deltaTime + velocity * Time.deltaTime);

    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (controller.collisionFlags == CollisionFlags.Above)
        {
            velocity.y = -2f;
        }
    }

    void playerPhysics()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        playerSpeed = controller.velocity.magnitude;
    }

    void crouch()
    {
        if (!isCrouching && Input.GetButton("Crouch"))
        {
            if (controller.isGrounded)
            {
                speed = crouchSpeed;
                jumpHeight = crouchJumpHeight;
                controller.height /= 1.5f;

                isCrouching = true;
            }
        }

        if (isCrouching && !Input.GetButton("Crouch"))
        {
            var cantStandUp = Physics.Raycast(rayPoint.transform.position, Vector3.up, 1.5f);

            if (!cantStandUp)
            {
                speed = originalSpeed;
                jumpHeight = originalJumpHeight;
                controller.height = originalHeight;

                isCrouching = false;
            }
        }
    }

    void playerRotate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(x, 0f, z);
        movementDirection.Normalize();

        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 700 * Time.deltaTime);
        }
    }
}
