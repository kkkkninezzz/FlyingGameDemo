using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SGF.Utils;

using Kurisu.Game.Player;
using Kurisu.Game.Data;
using SGF;

namespace Kurisu.Game
{
    /// <summary>
    /// 游戏内的摄像机
    /// </summary>
    public class GameCamera : MonoBehaviour
    {
        public static GameCamera CurCameraScript;

        public static Camera MainCamera;

        public static uint FocusPlayerId = 0;

        public static void Create()
        {
            Camera ca = GameObject.FindObjectOfType<Camera>();
            if (ca != null)
            {
                GameObjectUtils.EnsureComponent<GameCamera>(ca.gameObject);
            }
            else
            {
                Debugger.LogError("GameCamera", "Create() Cannot Find Camera In Scene!");
            }
        }

        public static void Release()
        {
            if (CurCameraScript != null)
            {
                GameObject camera = CurCameraScript.gameObject;
                camera.transform.position = ConfigConstants.DEFAULT_CAMERA_POSITION;
                camera.transform.rotation = ConfigConstants.DEFAULT_CAMERA_ROTATION;

                GameObject.Destroy(CurCameraScript);
                CurCameraScript = null;
            }
        }

        private GameContext m_context;
        
        private float distance = 2f;
        // the height we want the camera to be above the target
        private float height = 1f;
        
        private float rotationDamping = 2f;

        private float heightDamping = 3f;

        public void Start()
        {
            CurCameraScript = this;
            MainCamera = this.GetComponent<Camera>();
            m_context = GameLogicManager.Instance.Context;
        }
        

        public void LateUpdate()
        {
            if (!GameLogicManager.Instance.IsRunning)
            {
                return;
            }

            //FollowTarget();
            FollowTarget2();
        }

        #region 实现1
        private void FollowTarget()
        {
            Transform target = FindTarget();
            if (target == null)
            {
                return;
            }


            // Calculate the current rotation angles
            var wantedRotationAngle = target.eulerAngles.y;
            var wantedHeight = target.position.y + height;

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
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;

            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            // Always look at the target
            transform.LookAt(target);
        }
        #endregion


        private Transform FindTarget()
        {
            FlyingPlayer flyingPlayer = GameLogicManager.Instance.GetPlayer(FocusPlayerId);
            if (flyingPlayer == null)
                return null;

            return flyingPlayer.FlyingVehicleView.transform;
        }

        #region 实现2
        private float RotateSpeed = 15f;
        private Transform m_lookPos; 

        private void FollowTarget2()
        {
            Transform target = FindTarget();
            if (target == null)
            {
                return;
            }
            
            if (this.m_lookPos == null)
            {
                this.m_lookPos = target.Find("LookPos");
            }

            transform.position = m_lookPos.position;
            Quaternion rotation = Quaternion.Lerp(transform.rotation, m_lookPos.rotation, RotateSpeed * Time.deltaTime);
            transform.rotation = rotation;
        }
        
        #endregion
        
    }

}
