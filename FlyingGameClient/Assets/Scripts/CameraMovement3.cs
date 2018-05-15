using UnityEngine;
using System.Collections;

public class CameraMovement3 : MonoBehaviour
{
    //这个脚本使用Unity秘密教程中引入的一些概念。  

    public float smooth = 1.5f;         //照相机能赶上的相对速度。  


    public Transform ball;           // The ball's position.  
    private Vector3 relativeCameraPosition;       // 摄像机相对于球得点  
    private float relativeCameraDistance;      // 摄像机现对于球 的距离  
    private Vector3 newCameraPosition;             // 新位置点是否能到达摄像机  


    void Start()
    {
        // Set up the values that remain unchanged during the entire level  

        //设置在整个级别中保持不变的值。   

        relativeCameraPosition = transform.position - ball.position;//摄像机的相对位置  
        relativeCameraDistance = relativeCameraPosition.magnitude - 0.5f; // to look (roughly) at the center of the ball (1m ball size)摄像机看向求得距离  
    }


    void LateUpdate()
    {
        //摄影机在FixedUpdate（因为游戏物理将在FixedUpdate，球移动相机应该做的）  
        Vector3 standardPosition = ball.position + relativeCameraPosition; // 获得当前摄像机的目标位置  
        Vector3 topPosition = ball.position + Vector3.up * relativeCameraDistance; //直接在球顶上取得位置  

        //  在标准位置和顶部位置之间放置一组可能的摄像机位置。  
        //    
        //如果相机不能从标准位置看到玩家，它会尝试所有其他位置，直到它可以看到球员againn。  

        Vector3[] cameraPoints = new Vector3[5];

        cameraPoints[0] = standardPosition;
        cameraPoints[1] = Vector3.Lerp(standardPosition, topPosition, 0.25f); // Lerp is used here to interpolate  
        cameraPoints[2] = Vector3.Lerp(standardPosition, topPosition, 0.5f);  // 0, 25, 50, 75, 100% the line between standard and top position  
        cameraPoints[3] = Vector3.Lerp(standardPosition, topPosition, 0.75f);
        cameraPoints[4] = topPosition;

        //检查所有相机点，直到摄像机看到球。  
        for (int i = 0; i < cameraPoints.Length; i++)
        {
            if (CameraSeesBall(cameraPoints[i]))
                break;
        }

        // Smoothly transition to the new camera point over time平稳过渡到新相机点随着时间的推移  
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, smooth * Time.deltaTime);

        // Reposition camera to look at the ball  
        CameraLookAtBall();
    }


    bool CameraSeesBall(Vector3 checkPosition)
    {
        RaycastHit hit;

        //Raycast from Camera to the ball and see if it finds it  

        if (Physics.Raycast(checkPosition, ball.position - checkPosition, out hit, relativeCameraDistance))
            if (hit.transform != ball)
                return false;

        //this is the new position for the camera  
        newCameraPosition = checkPosition;
        return true;
    }


    void CameraLookAtBall()
    {
        // 创建一个向量和旋转从相机向玩家。  
        Vector3 relativeBallPosition = ball.position - transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(relativeBallPosition, Vector3.up);

        //平滑调整相机  
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }
}
