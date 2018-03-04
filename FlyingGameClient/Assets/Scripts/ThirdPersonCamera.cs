using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    // 摄像机与目标的水平距离
    public float horizontalDistance;

    // 摄像机与目标的垂直距离
    public float verticalDistance;

    // 摄像机移动到目标点的平滑度，也可以理解为速度
    public float smooth;

    // 跟随的目标
    public Transform target;
 
	
	void LateUpdate () {
        Vector3 targetPos = target.position + Vector3.up * verticalDistance + target.forward * horizontalDistance;

        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * smooth);

        this.transform.LookAt(target);
	}
}
