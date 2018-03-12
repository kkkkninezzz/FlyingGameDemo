using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyingController : MonoBehaviour {
    private Vector3 forcePoint;

    [SerializeField]
    // 向前的力
    public float forwardForce;

    [SerializeField]
    public Transform forceTail;

    [SerializeField]
    public Transform forceLeftHead;

    [SerializeField]
    public Transform forceRightHead;

    [SerializeField]
    public Transform forceLeftTail;

    [SerializeField]
    public Transform forceRightTail;

    // 向上的力，初始值与刚体的重力相等
    private float upwardForce;

    // 物体所受的重力
    private float gravity;

    private Rigidbody thisRigibody;

    // 垂直物体向上的力
    private float baseTransUpForce;

    private float transUpForce;

    // 垂直方向额外的力
    private float verticalExtraForce;

    // 水平方向额外的力
    private float horizontalExtraForce;

    // Use this for initialization
    void Start () {
        thisRigibody = GetComponent<Rigidbody>();
        forcePoint = thisRigibody.centerOfMass;

        Debug.Log(forcePoint);
        gravity = thisRigibody.mass * 9.81f;

        transUpForce = baseTransUpForce = gravity / 4;

	}
	
	void FixedUpdate () {
        //Debug.Log(thisRigibody.centerOfMass);
        thisRigibody.AddForceAtPosition(transform.up * upwardForce, forcePoint);

        thisRigibody.AddForceAtPosition(transform.up * transUpForce, forceLeftHead.position);
        thisRigibody.AddForceAtPosition(transform.up * transUpForce, forceRightHead.position);
        thisRigibody.AddForceAtPosition(transform.up * transUpForce, forceLeftTail.position);
        thisRigibody.AddForceAtPosition(transform.up * transUpForce, forceRightTail.position);
        
        //verticalExtraForce = 

        if (Input.GetKey(KeyCode.W))
            thisRigibody.AddForceAtPosition(transform.forward * forwardForce, forceTail.position);
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            /*
            thisRigibody.AddForceAtPosition(-transform.up * transUpForce / 32, forceLeftTail.position);
            thisRigibody.AddForceAtPosition(-transform.up * transUpForce / 32, forceRightTail.position);
            */
            //Debug.Log(this.transform.rotation.eulerAngles.x);
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x - 2f, 0, 0);
        }else if (Input.GetKey(KeyCode.DownArrow))
        {
            /*
            thisRigibody.AddForceAtPosition(transform.up * transUpForce / 32, forceLeftTail.position);
            thisRigibody.AddForceAtPosition(transform.up * transUpForce / 32, forceRightTail.position);
            */
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x + 2f, 0, 0);
        }
    }
}
