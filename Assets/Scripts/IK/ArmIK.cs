using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmIK : MonoBehaviour
{
    [Header("Transforms")]
    public Transform player;
    

    [Header("Floats")]
    public float rayDistance;

    [Header("General Variables")]
    public LayerMask layerMask;
    public bool isGrab;

    [Header("Swing Variables")]
    public float lerpSpeed;

    void Start()
    {
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, layerMask)) // set to raycat points
        {
            // set it to an grab pos until it hits a box

            if(isGrab)
            {
                // set target pos to raycast point
                // paret box
                //decrease speed
                //rotate player to face box
                // decrease step distance
                
            }
            else
            {
                resetPos();
            }
        }
        else
        {
        }

        swingArm();
    }

    void resetPos()
    {

    }

    void Inputs()
    {
        if(Input.GetButtonDown(" Fire1"))
        {
            isGrab = true;
        }

        if (Input.GetButtonUp(" Fire1"))
        {
            isGrab = false;
        }
    }

    void swingArm()
    {
    }
}
