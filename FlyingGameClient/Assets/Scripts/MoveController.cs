using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    // 主体
    GameObject mainBody;

    public Transform forceHead;

    public Transform forceLeftAirfoil;
    public Transform forceRightAirfoil;

    public Transform forceLeftTailAirfoil;
    public Transform forceRightTailAirfoil;

    private Transform thisTransform;
    private Rigidbody thisRigidbody;

    public float speed = 250.0f;

    public float airfoilForce = 11.0f;

    public float tailAirfoilForce = 6.0f;

    // Use this for initialization
    void Start()
    {
        mainBody = this.gameObject;
        thisTransform = transform;

        /*
        forceHead = thisTransform.Find("FlyingVehicle/ForcePoints/ForceHead");

        forceLeftAirfoil = thisTransform.Find("FlyingVehicle/ForcePoints/ForceLeftAirfoil");
        forceRightAirfoil = thisTransform.Find("FlyingVehicle/ForcePoints/ForceRightAirfoil");

        forceLeftTailAirfoil = thisTransform.Find("FlyingVehicle/ForcePoints/ForceLeftTailAirfoil");
        forceRightTailAirfoil = thisTransform.Find("FlyingVehicle/ForcePoints/ForceRightTailAirfoil");
        */
        thisRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        thisRigidbody.AddForceAtPosition(thisTransform.forward * speed, forceHead.position);

        thisRigidbody.AddForceAtPosition(thisTransform.up * airfoilForce, forceLeftAirfoil.position);
        thisRigidbody.AddForceAtPosition(thisTransform.up * airfoilForce, forceRightAirfoil.position);

        thisRigidbody.AddForceAtPosition(thisTransform.up * tailAirfoilForce, forceLeftTailAirfoil.position);
        thisRigidbody.AddForceAtPosition(thisTransform.up * tailAirfoilForce, forceRightTailAirfoil.position);

        if (Input.GetKey(KeyCode.W))
        {
            // 俯冲
            thisRigidbody.AddForceAtPosition(thisTransform.up * tailAirfoilForce, forceLeftTailAirfoil.position);
            thisRigidbody.AddForceAtPosition(thisTransform.up * tailAirfoilForce, forceRightTailAirfoil.position);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // 爬升
            thisRigidbody.AddForceAtPosition(thisTransform.up * -tailAirfoilForce, forceLeftTailAirfoil.position);
            thisRigidbody.AddForceAtPosition(thisTransform.up * -tailAirfoilForce, forceRightTailAirfoil.position);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // 左翻滚
            thisRigidbody.AddForceAtPosition(thisTransform.up * -tailAirfoilForce, forceLeftTailAirfoil.position);
            thisRigidbody.AddForceAtPosition(thisTransform.up * tailAirfoilForce, forceRightTailAirfoil.position);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // 右翻滚
            thisRigidbody.AddForceAtPosition(thisTransform.up * tailAirfoilForce, forceLeftTailAirfoil.position);
            thisRigidbody.AddForceAtPosition(thisTransform.up * -tailAirfoilForce, forceRightTailAirfoil.position);
        }
    } 
}
