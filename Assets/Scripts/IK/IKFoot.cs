using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFoot : MonoBehaviour
{
    public IKFoot ikFoot;
    public PlayerController controller;

    [Header("Feet")]
    public bool rightFoot;
    public bool leftFoot;
    
    [Header("Transforms")]
    public Transform raycastPoint;
    public Transform targetPoint;
    public Transform jumpingPos;
    public Transform idlePos;
    public Transform body;
    public Transform player;

    [Header("Floats")]
    public float stepDistance = 1f;
    public float stepHeight;
    public float rayDistance;
    public float lerpSpeed;
    public float bodyLerpSpeed;

    public AnimationCurve yCurve;

    Vector3 newPosition;
    Vector3 lastPosition;
    Vector3 normal;

    float footSpeed;
    float timer;
    float originalDistance;

    bool moving;
    bool isGrounded;

    private void Start()
    {
        originalDistance = stepDistance;
    }

    void Update()
    {
        footSpeedCheck();
        targetPointMove();

    }

    private void targetPointMove()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastPoint.position, raycastPoint.forward, out hit, rayDistance))
        {
            targetPoint.position = hit.point;

            if (Vector3.Distance(transform.position, targetPoint.position) > stepDistance)
            {
                if (!ikFoot.moving)
                {
                    stepDistance = originalDistance;
                    timer = 0;

                    newPosition = targetPoint.position;
                    normal = hit.normal;
                }
            }

            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            newPosition = jumpingPos.position;
            stepDistance = 0;
        }

        timer += Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(newPosition.x, newPosition.y + yCurve.Evaluate(timer), newPosition.z), lerpSpeed * Time.deltaTime);
        transform.up = normal;

        transform.rotation = player.rotation;
        transform.rotation *= Quaternion.Euler(new Vector3(0, 180, 0));
    }

    void footSpeedCheck()
    {
        footSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        RaycastHit hit;
        if (Physics.Raycast(idlePos.position, Vector3.down, out hit, rayDistance))
        {
            if (ikFoot.moving && moving)
            {
                if (rightFoot)
                {
                    newPosition = hit.point;
                }
            }
        }

        if (footSpeed > 0 && isGrounded)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }
}
