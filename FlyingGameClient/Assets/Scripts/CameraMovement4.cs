using UnityEngine;
using System.Collections;

public class CameraMovement4 : MonoBehaviour
{
    public Transform Target;

    public float MoveSpeed = 3f;

    public float RotateSpeed = 2f;
    

    // Update is called once per frame
    void LateUpdate()
    {
        
        transform.position = Target.position; // Vector3.Lerp(transform.position, Target.position, MoveSpeed * Time.deltaTime);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, Target.rotation, RotateSpeed * Time.deltaTime);
        //rotation = new Quaternion(rotation.x, rotation.y, 0, rotation.w);
        transform.rotation = rotation;
        //transform.LookAt(Target);
        
        /*
        var wantedRotationAngle = Target.eulerAngles.y;
        var currentRotationAngle = transform.eulerAngles.y;
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, 2f * Time.deltaTime);
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = Target.position;
        transform.position -= currentRotation * Vector3.forward * 1;

        transform.LookAt(Target);
        */
    }
}
