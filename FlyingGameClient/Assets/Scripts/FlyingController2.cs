using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyingController2 : MonoBehaviour
{
    private Vector3 forcePoint;

    [SerializeField]
    // 向前的力
    public float forwardForce;

    // 向上的力，初始值与刚体的重力相等
    private float upwardForce;

    // 物体所受的重力
    private float gravity;

    private Rigidbody thisRigibody;

    // Use this for initialization
    void Start()
    {
        thisRigibody = GetComponent<Rigidbody>();
        forcePoint = thisRigibody.worldCenterOfMass;

        Debug.Log(forcePoint);
        upwardForce = gravity = thisRigibody.mass * 9.81f;

    }

    void FixedUpdate()
    {

        thisRigibody.AddForceAtPosition(transform.up * upwardForce, forcePoint);

        
        if (Input.GetKey(KeyCode.W))
            thisRigibody.AddForceAtPosition(transform.forward * forwardForce, forcePoint);
        /*
        if (Input.GetKey(KeyCode.UpArrow))
        {
            thisRigibody.AddForceAtPosition(-transform.up * upwardForce / 8, forceLeftTail.position);
            thisRigibody.AddForceAtPosition(-transform.up * upwardForce / 8, forceRightTail.position);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            thisRigibody.AddForceAtPosition(transform.up * upwardForce / 8, forceLeftTail.position);
            thisRigibody.AddForceAtPosition(transform.up * upwardForce / 8, forceRightTail.position);
        }

        */
    }
}
