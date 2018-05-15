using UnityEngine;
using System.Collections;

public class CameraMovement2 : MonoBehaviour
{
    public float distance = 2f;
    // the height we want the camera to be above the target
    public float height = 1f;

    public float rotationDamping = 2f;

    public float heightDamping = 3f;

    public Transform Target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Calculate the current rotation angles
        var wantedRotationAngle = Target.eulerAngles.y;
        var wantedHeight = Target.position.y + height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = Target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        //transform.rotation = currentRotation;

        // Always look at the target
        transform.LookAt(Target);
    }
}
